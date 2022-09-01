using App.Adapters;
using App.Domain.Contracts;
using App.Domain.Contracts.Adapter;
using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Domain.Entities.TransacaoPedido;
using App.Domain.Service;
using App.Repository.Context;
using App.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.ConsumerConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddTransient<ISchedulerService, SchedulerService>();
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddTransient<ILoggerRepository, LoggerRepository>();
            services.AddTransient<IPagamentoService, PagamentoService>();
            
            services.AddTransient<IPagamentoAdapter, PagamentoAdapter>();

            services.AddTransient<IPedidoRepository, PedidoRepository>();

            services.AddDbContext<DefaultContext>(options => options.UseSqlServer("Data Source=.\\;initial catalog=App;Trusted_Connection=True;MultipleActiveResultSets=true"));

            var serviceProvider = services.BuildServiceProvider();
            var _schedulerService = serviceProvider.GetService<ISchedulerService>();
            var _loggerService = serviceProvider.GetService<ILoggerService>();


            
            while (true)
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(queue: "Pedido",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += async (model, ea) =>
                    {
                        try
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            Console.WriteLine("Coletando mensagem..");
                            var FinalizarPedido = JsonConvert.DeserializeObject<FinalizarPedidoModel>(message);
                            if (FinalizarPedido == null)
                                throw new CustomException() { mensagemErro = "Erro ao coletar pedido na fila" };

                            var pedido = FinalizarPedido.pedido;

                            if (await Task.Run(() => _schedulerService.ProcessarPedidoFila(FinalizarPedido.Cartao, ref pedido)))
                                channel.BasicAck(ea.DeliveryTag, false);
                            else
                                throw new CustomException() { mensagemErro = $"Ocorreu um erro ao processar pedido: {pedido.CodigoPedido}" };
                        }
                        catch (AggregateException cex) when (cex.InnerException is CustomException)
                        {
                            _loggerService.InsertLog(cex);

                            channel.BasicNack(ea.DeliveryTag, false, false);
                        }
                        catch (Exception ex)
                        {
                            string erro = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
                            _loggerService.InsertLog(ex);

                            channel.BasicNack(ea.DeliveryTag, false, false);
                        }
                        Console.WriteLine("Coleta finalizada..");
                    };
                    
                    channel.BasicConsume(queue: "Pedido",
                                         autoAck: false,
                                         consumer: consumer);


                }
                
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }
}
