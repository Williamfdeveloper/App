using App.Domain;
using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Entities.Cartao;
using App.Domain.Entities.TransacaoPedido;
using App.Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ConsoleApp
{
    internal class Program
    {
        private readonly IPagamentoService _pagamentoService;
        //private readonly IPedidoService _pedidoService;
        //public Program(IPagamentoService pagamentoService, IPedidoService pedidoService)
        //{
        //    _pagamentoService = pagamentoService;
        //    _pedidoService = pedidoService;
        //}


        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddTransient(typeof(IPedidoService), typeof(PedidoService));
            services.AddTransient(typeof(IPagamentoService), typeof(PagamentoService));

            var serviceProvider = services.BuildServiceProvider();

            //var pedidoService = serviceProvider.GetService<IPedidoService>();
            var _pagamentoService = serviceProvider.GetService<IPagamentoService>();


            while (true)
            {

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: EnumTipo.Queue.Pedido.ToString(),
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        try
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);


                            var FinalizarPedido = JsonConvert.DeserializeObject<FinalizarPedidoModel>(message);
                            if (FinalizarPedido == null)
                                throw new CustomException() { mensagemErro = "Erro ao coletar pedido na fila" };

                            var pedido = FinalizarPedido.pedido;

                            if (ProcessarPedidoFila(FinalizarPedido.Cartao, ref pedido))
                                channel.BasicAck(ea.DeliveryTag, false);

                            throw new CustomException() { mensagemErro = $"Ocorreu um erro ao processar pedido: {pedido.CodigoPedido}" };

                        }
                        catch (AggregateException cex) when (cex.InnerException is CustomException)
                        {
                            //_loggerService.InsertLog(cex);

                            //return BadRequest(_loggerService.InsertLog(cex).mensagemErro);
                            channel.BasicNack(ea.DeliveryTag, false, false);
                        }
                        catch (Exception ex)
                        {
                            string erro = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                            //_loggerService.InsertLog(ex);

                            channel.BasicNack(ea.DeliveryTag, false, false);
                        }

                    };
                    channel.BasicConsume(queue: EnumTipo.Queue.Pedido.ToString(),
                                         autoAck: true,
                                         consumer: consumer);

                }

                await Task.Delay(TimeSpan.FromSeconds(5));
            }

            //return true;

        }


        public static bool ProcessarPedidoFila(CartaoModel cartao, ref Pedido pedido)
        {
            if (cartao != null)
                throw new CustomException() { mensagemErro = "Objeto Pedido não informado." };

            if (pedido == null)
                throw new CustomException() { mensagemErro = "Objeto pedido não informado." };

            if (pedido.DataPedido == null)
                throw new CustomException() { mensagemErro = "Data pedido não informado." };

            if (pedido.ValorTotal == 0)
                throw new CustomException() { mensagemErro = "Valor do pedido não informado." };

            if (string.IsNullOrEmpty(pedido.CodigoUsuario))
                throw new CustomException() { mensagemErro = "Usuario não informdo no pedido." };

            if (pedido.FormaPagamento == null)
                throw new CustomException() { mensagemErro = "Codigo da forma de pagamento do pedido não informado." };

            if (pedido.SituacaoPedido == 0)
                throw new CustomException() { mensagemErro = "Status do pedido invalido." };

            if (pedido.PedidoItem == null || pedido.PedidoItem.Count() == 0)
                throw new CustomException() { mensagemErro = "Itens do pedido não informado." };

            if (pedido.PedidoHistorico == null || pedido.PedidoHistorico.Count() == 0)
                throw new CustomException() { mensagemErro = "Historico do pedido não informado." };

            if (pedido.FormaPagamento.CodigoFormaPagamento != (int)EnumTipo.FormaPagamento.Credito)
                throw new CustomException() { mensagemErro = "Forma de pagamenmto nao autorizada." };

            //var usuario = _userManager.FindByIdAsync(pedido.CodigoUsuario).Result;
            //if (usuario == null)
            //    throw new CustomException() { mensagemErro = "Usuario não identificado, por gentileza, efetue o Login." };

            if (pedido.SituacaoPedido == (int)EnumTipo.SituacaoPedido.EmAprovacao || pedido.SituacaoPedido == (int)EnumTipo.SituacaoPedido.Cancelado)
                return true;//Retorna true pra remover da fila

            var retornopagamento = _pagamentoService.ProcessarPagamento(cartao, pedido.ValorTotalComDesconto);
            var dataAtual = DateTime.Now;
            switch (retornopagamento)
            {
                case (int)EnumTipo.SituacaoPedidoPagamento.EmAprovacao:
                    pedido.SituacaoPedido = (int)EnumTipo.SituacaoPedido.EmAprovacao;
                    pedido.DataAtualizacaoPedido = dataAtual;

                    pedido.PedidoHistorico.Where(c => c.CodigoPedido.Equals(c.CodigoPedido) && c.DataAtualizacaoFim.Equals(null)).Select(c =>
                    {
                        c.DataAtualizacaoFim = dataAtual;
                        return c;
                    }).OrderByDescending(c => c.DataSituacao).ToList();

                    pedido.PedidoHistorico.Add(new PedidoHistorico()
                    {
                        CodigoPedido = pedido.CodigoPedido,
                        DataAtualizacaoInicio = dataAtual,
                        DataSituacao = dataAtual,
                        IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.EmAprovacao,
                    });

                    pedido.PedidoPagamento.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
                    {
                        CodigoPedidoPagamento = pedido.PedidoPagamento.CodigoPedidoPagamento,
                        DataAtualizacao = dataAtual,
                        IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.EmAprovacao
                    });

                    return _pedidoRepository.Atualizar(pedido);
                case (int)EnumTipo.SituacaoPedidoPagamento.Aprovado:
                    pedido.SituacaoPedido = (int)EnumTipo.SituacaoPedido.Finalizado;
                    pedido.DataAtualizacaoPedido = dataAtual;

                    pedido.PedidoHistorico.Where(c => c.CodigoPedido.Equals(c.CodigoPedido) && c.DataAtualizacaoFim.Equals(null)).Select(c =>
                    {
                        c.DataAtualizacaoFim = dataAtual;
                        return c;
                    }).OrderByDescending(c => c.DataSituacao).ToList();

                    pedido.PedidoHistorico.Add(new PedidoHistorico()
                    {
                        CodigoPedido = pedido.CodigoPedido,
                        DataAtualizacaoInicio = dataAtual,
                        DataSituacao = dataAtual,
                        IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.Finalizado,
                    });

                    pedido.PedidoPagamento.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
                    {
                        CodigoPedidoPagamento = pedido.PedidoPagamento.CodigoPedidoPagamento,
                        DataAtualizacao = dataAtual,
                        IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.Aprovado

                    });

                    return _pedidoRepository.Atualizar(pedido);
                case (int)EnumTipo.SituacaoPedidoPagamento.Cancelado:
                    pedido.SituacaoPedido = (int)EnumTipo.SituacaoPedido.Cancelado;
                    pedido.DataAtualizacaoPedido = dataAtual;

                    pedido.PedidoHistorico.Where(c => c.CodigoPedido.Equals(c.CodigoPedido) && c.DataAtualizacaoFim.Equals(null)).Select(c =>
                    {
                        c.DataAtualizacaoFim = dataAtual;
                        return c;
                    }).OrderByDescending(c => c.DataSituacao).ToList();

                    pedido.PedidoHistorico.Add(new PedidoHistorico()
                    {
                        CodigoPedido = pedido.CodigoPedido,
                        DataAtualizacaoInicio = dataAtual,
                        DataSituacao = dataAtual,
                        IdSituacaoPedido = (int)EnumTipo.SituacaoPedido.Cancelado,
                    });

                    pedido.PedidoPagamento.PedidoPagamentoHistorico.Add(new PedidoPagamentoHistorico()
                    {
                        CodigoPedidoPagamento = pedido.PedidoPagamento.CodigoPedidoPagamento,
                        DataAtualizacao = dataAtual,
                        IdSituacaoPedidoPagamento = (int)EnumTipo.SituacaoPedidoPagamento.Cancelado

                    });

                    return _pedidoRepository.Atualizar(pedido);
                default:
                    break;
            }




            throw new NotImplementedException();
        }
    }
}
