using Microsoft.AspNetCore.Components;

namespace WhatsApp.Pages
{
    public partial class Register
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        private async Task SignUpAsync()
        {
            try
            {
                Navigation.NavigateTo("/");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error message: {ex.Message}");
            }
        }
    }
}
