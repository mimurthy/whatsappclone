using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    public interface IConsumer
    {
        public void Initialize(string userId);
        public void Consume(EventHandler<BasicDeliverEventArgs> Received);
    }
}
