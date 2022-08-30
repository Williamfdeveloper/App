using App.Domain.Contracts;
using App.Domain.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Service
{
    public class MessageQueueService : IMessageQueueService
    {
        public Task<bool> PostMessageQueue<T>(T Object, int fila)
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

                return Task.FromResult(true);
            }
        }

        public Task<bool> ProcessMessageQueue<T>(int fila)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                QueueDeclare(channel, ((EnumTipo.Queue)fila).ToString());

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        //Console.WriteLine(" [x] Received {0}", message);

                        switch (fila)
                        {
                            case (int)EnumTipo.Queue.Pedido:
                                var pedido = JsonConvert.DeserializeObject<T>(message);



                                break;
                            default:
                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        channel.BasicNack(ea.DeliveryTag, false, false);
                    }
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

            }

            return Task.FromResult(true);

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
