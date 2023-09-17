using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace WhatsApp.Pages
{
    public partial class Login
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        private async Task SignInAsync()
        {
            try
            {
                var result = await HttpClient.GetFromJsonAsync<string>($"/api/v1/user");
                Navigation.NavigateTo("/chat");
            }
            catch(HttpRequestException ex) 
            {
                Console.WriteLine($"Error message: {ex.Message}");
            }
        }

        private void SignUp()
        {
            try
            {
                Navigation.NavigateTo("/Register");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error message: {ex.Message}");
            }
        }
    }
}
