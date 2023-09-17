using System.Text.Json.Serialization;

namespace UserService.Models
{
    public class UsernameAndPwd
    {
        [JsonPropertyName("user_name")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }   
    }

    public class User : UsernameAndPwd 
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber {  get; set; }

        [JsonPropertyName("device_id")]
        public string DeviceId { get; set;}
    }
}
