using sta_websocket_sharp_core;
using sta_websocket_sharp_core.Server;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    class WebsocketServer : IServer
    {
        public IProducer producer_user {  get; set; }
        public IProducer producer_db { get; set; }

        public IConsumer consumer { get; set; }

        public IDatabaseAdapter dbAdapter { get; set; } 

        public void Listen(int port)
        {
            var server = new WebSocketServer(port);
            server.AddWebSocketService<Chat>("/chat", (chatObj) => 
            {
                chatObj.producer_db = producer_db;
                chatObj.consumer = consumer;
                chatObj.dbAdapter = dbAdapter;
                chatObj.producer_user = producer_user;
            });

            server.Start();
        }
    }
}
