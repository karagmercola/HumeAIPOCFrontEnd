using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HumeAIPOCFrontEnd
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private string _humeApiKey = "your-hume-api-key";  // Replace with actual API key
        private string _humeEndpoint = "https://api.hume.ai/v1/chat/completions";

        public MainPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            string userMessage = UserInput.Text;
            if (!string.IsNullOrEmpty(userMessage))
            {
                ResponseLabel.Text = "Loading...";

                var requestBody = new
                {
                    messages = new[]
                    {
                        new { role = "user", content = userMessage }
                    }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("Authorization", $"Bearer {_humeApiKey}");

                try
                {
                    var response = await _httpClient.PostAsync(_humeEndpoint, content);
                    var result = await response.Content.ReadAsStringAsync();
                    ResponseLabel.Text = result;
                }
                catch (Exception ex)
                {
                    ResponseLabel.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}



