using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Diagnostics;
using UserService.Data;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Login([FromBody] UsernameAndPwd model)
        {
            IDatabaseAdapter adapter = new SQLServerDatabaseHandler();
            string retVal = adapter.Login(model.Username, model.Password);

            if ( retVal != string.Empty ) 
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "ChatService.exe",
                    Arguments = retVal + " " + model.Username,
                    UseShellExecute = true
                };

                using (Process process = new Process())
                {
                    process.StartInfo = psi;    
                    Process.Start(psi);
                }

                return Ok("ws://localhost:" + retVal + "/chat");
            }
            else
            {
                return BadRequest("Authentication failed");
            }
        }

        [HttpPost]
        public IActionResult SignUp([FromBody] User model)
        {
            IDatabaseAdapter adapter = new SQLServerDatabaseHandler();
            adapter.SignUp(model);

            CreateRMQ(model.Username);

            return Ok("Data updated successfully!");
        }

        private void CreateRMQ(string username) 
        {
            var _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            var _connection = _factory.CreateConnection();

            var _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: username,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(queue: username,
                exchange: "whatsapp_exchange",
                routingKey: username);
        }
    }
}
