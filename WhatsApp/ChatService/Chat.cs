using sta_websocket_sharp_core;
using sta_websocket_sharp_core.Server;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ChatService
{
    class Chat : WebSocketBehavior
    {
        public IProducer producer_user { get; set; }
        public IProducer producer_db { get; set; }

        public IConsumer consumer { get; set; }

        public IDatabaseAdapter dbAdapter { get; set; }

        public Chat()
        {

        }

        private void ReceivedEventHandler(object sender, BasicDeliverEventArgs e)
        {
            var messageBody = Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine($"Received message: {messageBody}");
            Send(messageBody);
        }

        protected override void OnOpen()
        {
            consumer.Consume(ReceivedEventHandler);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            ChatMessage message = JsonConvert.DeserializeObject<ChatMessage>(e.Data);

            Console.WriteLine($"Received message from client: {message.from}, to client: {message.to}, Content :- {message.message}, timestamp :- {message.timestamp}");

            producer_user.Produce(e.Data);
            producer_db.Produce(e.Data);
        }

        protected override void OnClose(CloseEventArgs e) 
        {
            base.OnClose(e);
            Console.WriteLine("WebSocket connection closed. Exiting..");
            Environment.Exit(0 );
        }
    }
}
