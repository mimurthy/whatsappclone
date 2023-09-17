using DatabaseService;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;

namespace DatabaseService
{
    class Program
    {
        static public IDatabaseAdapter dbAdapter { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Database service started");

            dbAdapter = new MongoDbDatabase();

            IConsumer consumer = new RMQConsumer();
            consumer.Initialize("DBUpdate");
            consumer.Consume(ReceivedEventHandler);

            while(true)
            {
                Thread.Sleep(1000);
            }
        }

        static private void ReceivedEventHandler(object sender, BasicDeliverEventArgs e) 
        {
            var messageBody = Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine($"Received event: {messageBody}");

            ChatMessage message = JsonConvert.DeserializeObject<ChatMessage>(messageBody);

            //Save the message to MongoDB
            dbAdapter.InsertChatMessage(message);
        }
    }
}