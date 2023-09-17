using RabbitMQ.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    class RMQProducer : IProducer, IDisposable
    {
        protected ConnectionFactory _factory;
        protected IConnection _connection;
        protected IModel _channel;

        public void Initialize()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            _connection = _factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("whatsapp_exchange", ExchangeType.Topic);            
        }

        public virtual void Produce(string msg)
        {
            ChatMessage message = JsonConvert.DeserializeObject<ChatMessage>(msg);

            _channel.BasicPublish(
                exchange: "whatsapp_exchange",
                routingKey: message.to,
                mandatory: true,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(msg) 
                );
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Close();
        }
    }

    class RMQProducerDB : RMQProducer
    {
        public override void Produce(string msg)
        {
            ChatMessage message = JsonConvert.DeserializeObject<ChatMessage>(msg);

            _channel.BasicPublish(
                exchange: "whatsapp_exchange",
                routingKey: "DBUpdate",
                mandatory: true,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(msg)
                );

        }
    }
}
