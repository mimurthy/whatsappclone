using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WhatsApp.Pages
{
    public partial class Chat
    {
        public Chat()
        {
            
        }

        public class Friend
        {
            public string name { get; set; } = string.Empty;

            public string message { get; set; } = string.Empty;

            public string profile { get; set; } = string.Empty;

        }

        public List<Friend> friends { get; set; } = new List<Friend>();

        public class ChatHistory
        {
            public string myName { get; set; } = string.Empty;

            public List<Message> messages { get; set; } = new List<Message>();
        }

        public ChatHistory chatHistory = new ChatHistory()
        {
            myName = "Mithun"
        };

        public class Message
        {
            public string from { get; set; } = string.Empty;

            public string to { get; set; } = string.Empty;

            public string message { get; set; } = string.Empty;

            public DateTime timeOfMessage { get; set; }
        }

        public void LoadChat(string myname, string friendname)
        {
            chatHistory.messages.Add(new Message()
            {
                from = "Deepa",
                to = "Mithun",
                message = "Hi, How are you",
                timeOfMessage = DateTime.Now,
            });

            chatHistory.messages.Add(new Message()
            {
                from = "Mithun",
                to = "Deepa",
                message = "I am fine and you",
                timeOfMessage = DateTime.Now,
            });
        }
    }
}
