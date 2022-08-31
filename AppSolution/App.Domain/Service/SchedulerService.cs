using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Entities.TransacaoPedido;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Service
{
    public class SchedulerService : BackgroundService
    {
        //private readonly MessageQueueService _messageQueueService;
        public IServiceScopeFactory _serviceScopeFactory;
        //private readonly ILoggerService _loggerService;

        public SchedulerService(/*MessageQueueService messageQueueService,*/ IServiceScopeFactory serviceScopeFactory) //, ILoggerService loggerService)
        {
            //_loggerService = loggerService;
            //_messageQueueService = messageQueueService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {

            //ImplementarRepositoryBase passando o dbcontext 
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                //IScoped scoped = scope.ServiceProvider.GetRequiredService();

                while (!cancellationToken.IsCancellationRequested)
                {
                    //var factory = new ConnectionFactory() { HostName = "localhost" };
                    //using (var connection = factory.CreateConnection())
                    //using (var channel = connection.CreateModel())
                    //{
                    //    QueueDeclare(channel, EnumTipo.Queue.Pedido.ToString()); //((EnumTipo.Queue)fila).ToString());

                    //    var consumer = new EventingBasicConsumer(channel);

                    //    consumer.Received += (model, ea) =>
                    //    {
                    //        try
                    //        {
                    //            var body = ea.Body.ToArray();
                    //            var message = Encoding.UTF8.GetString(body);

                    //            switch ((int)EnumTipo.Queue.Pedido)
                    //            {
                    //                case (int)EnumTipo.Queue.Pedido:
                    //                    var FinalizarPedido = JsonConvert.DeserializeObject<FinalizarPedidoModel>(message);
                    //                    if (FinalizarPedido == null)
                    //                        throw new CustomException() { mensagemErro = "Erro ao coletar pedido na fila" };

                    //                    var pedido = FinalizarPedido.pedido;

                    //                        //if (_pedidoService.ProcessarPedidoFila(FinalizarPedidoModel.Cartao, ref pedido))
                    //                        //    channel.BasicAck(ea.DeliveryTag, false);

                    //                    throw new CustomException() { mensagemErro = $"Ocorreu um erro ao processar pedido: {pedido.CodigoPedido}" };
                    //                default:
                    //                    break;
                    //            }

                    //        }
                    //        catch (AggregateException cex) when (cex.InnerException is CustomException)
                    //        {
                    //            //_loggerService.InsertLog(cex);

                    //                //return BadRequest(_loggerService.InsertLog(cex).mensagemErro);
                    //            channel.BasicNack(ea.DeliveryTag, false, false);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            string erro = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                    //            //_loggerService.InsertLog(ex);

                    //            channel.BasicNack(ea.DeliveryTag, false, false);
                    //        }

                    //    };
                    //    channel.BasicConsume(queue: EnumTipo.Queue.Pedido.ToString(),
                    //                         autoAck: true,
                    //                         consumer: consumer);

                    //}

                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
            }
        }

        private void QueueDeclare(IModel channel, string fila)
        {
            try
            {
                channel.QueueDeclare(queue: fila,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            }
            catch { }
        }

    }
}
