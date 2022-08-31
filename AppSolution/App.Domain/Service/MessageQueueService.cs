using App.Domain.Contracts;
using App.Domain.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace App.Domain.Service
{
    public class MessageQueueService : IMessageQueueService
    {
        //private readonly IPedidoService _pedidoService;
        //private readonly ILoggerService _loggerService;

        //public MessageQueueService(
        //    //IPedidoService pedidoService, 
        //    ILoggerService loggerService)
        //{
        //    _loggerService = loggerService;
        //    //_pedidoService = pedidoService;
        //}

        public bool PostMessageQueue<T>(T Object, int fila)
        {
            if (Object == null)
                throw new CustomException() { mensagemErro = "Pedido nao informado" };

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                try
                {
                    //Criar Fila
                    channel.QueueDeclare(queue: ((EnumTipo.Queue)fila).ToString(),
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                }
                catch{}


                //string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Object));

                channel.BasicPublish(exchange: "",
                                     routingKey: ((EnumTipo.Queue)fila).ToString(),
                                     basicProperties: null,
                                     body: body);
                //Console.WriteLine(" [x] Sent {0}", message);

                return true;
            }
        }

        //public bool ProcessMessageQueue(int fila)
        //{
        //    var factory = new ConnectionFactory() { HostName = "localhost" };
        //    using (var connection = factory.CreateConnection())
        //    using (var channel = connection.CreateModel())
        //    {
        //        QueueDeclare(channel, ((EnumTipo.Queue)fila).ToString());

        //        var consumer = new EventingBasicConsumer(channel);

        //        consumer.Received += (model, ea) =>
        //        {
        //            try
        //            {
        //                var body = ea.Body.ToArray();
        //                var message = Encoding.UTF8.GetString(body);

        //                switch (fila)
        //                {
        //                    case (int)EnumTipo.Queue.Pedido:
        //                        var FinalizarPedidoModel = JsonConvert.DeserializeObject<FinalizarPedidoModel>(message);
        //                        if (FinalizarPedidoModel == null)
        //                            throw new CustomException() { mensagemErro = "Erro ao coletar pedido na fila" };

        //                        var pedido = FinalizarPedidoModel.pedido;

        //                        //if (_pedidoService.ProcessarPedidoFila(FinalizarPedidoModel.Cartao, ref pedido))
        //                        //    channel.BasicAck(ea.DeliveryTag, false);

        //                        throw new CustomException() { mensagemErro = $"Ocorreu um erro ao processar pedido: {pedido.CodigoPedido}" };
        //                    default:
        //                        break;
        //                }

        //            }
        //            catch (AggregateException cex) when (cex.InnerException is CustomException)
        //            {
        //                _loggerService.InsertLog(cex);

        //                //return BadRequest(_loggerService.InsertLog(cex).mensagemErro);
        //                channel.BasicNack(ea.DeliveryTag, false, false);
        //            }
        //            catch (Exception ex)
        //            {
        //                string erro = $"Message:{ex.Message} - StackTrace: {ex.StackTrace}";
        //                _loggerService.InsertLog(ex);

        //                channel.BasicNack(ea.DeliveryTag, false, false);
        //            }

        //        };
        //        channel.BasicConsume(queue: ((EnumTipo.Queue)fila).ToString(),
        //                             autoAck: true,
        //                             consumer: consumer);

        //    }

        //    return true;

        //}

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
