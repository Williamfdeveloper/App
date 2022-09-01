using App.Domain.Contracts;
using App.Domain.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace App.Domain.Service
{
    public class MessageQueueService : IMessageQueueService
    {

        public bool PostMessageQueue<T>(T Object, int fila)
        {
            if (Object == null)
                throw new CustomException() { mensagemErro = "Pedido nao informado" };

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Criar Fila
                channel.QueueDeclare(queue: ((EnumTipo.Queue)fila).ToString(),
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Object, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

                channel.BasicPublish(exchange: "",
                                     routingKey: ((EnumTipo.Queue)fila).ToString(),
                                     basicProperties: null,
                                     body: body);

                return true;
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
