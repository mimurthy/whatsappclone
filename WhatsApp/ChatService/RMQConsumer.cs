using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChatService
{
    public class RMQConsumer : IConsumer
    {
        ConnectionFactory _factory;
        IConnection _connection;
        IModel _channel;
        string _queueName;
        string _bindingKey;

        public void Initialize(string username)
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

            _bindingKey = username;

            _channel.QueueBind(queue: username,
                exchange: "whatsapp_exchange",
                routingKey: _bindingKey);
        }

        public void Consume(EventHandler<BasicDeliverEventArgs> Received) 
        { 
            Console.WriteLine($"Listening for messages with binding key: {_bindingKey}");

            // Create a consumer
            var _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += Received;

            // Start consuming messages
            _channel.BasicConsume(queue: _queueName,
                autoAck: true,
                consumer: _consumer);
        }
    }
}
