using System;

namespace ChatService
{
    class Program
    {
        static void Main(string[] args) 
        {
            if (args.Length < 2) 
            {
                Console.WriteLine("Less arguments");
            }

            int providedPort = int.Parse(args[0]);
            string userId = args[1];

            IProducer producer = new RMQProducer();
            producer.Initialize();

            IProducer producerDb = new RMQProducer();
            producerDb.Initialize();

            IConsumer consumer = new RMQConsumer();
            consumer.Initialize(userId);

            IServer server = new WebsocketServer()
            {
                producer_user = producer,
                producer_db = producerDb,
                consumer = consumer
            };
            server.Listen(providedPort);

            Console.WriteLine("WebSocket server is listening on port {0}", providedPort);
            Console.WriteLine("Press any key to stop the server");
            Console.ReadKey(true);
        }
    }
}