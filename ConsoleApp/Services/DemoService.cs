using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using ConsoleApp.Models.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleApp.Services
{
    internal interface IDemoService
    {
        void WriteWelcomeMessage();
        Task<string> MakeGraphApiCall();
    }

    internal class DemoService : IDemoService
    {
        private readonly ILogger<DemoService> _logger;
        private readonly IClientCredentialService _clientCredentialService;
        private readonly IOptions<ConsoleAppSettings> _consoleAppSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public DemoService(ILogger<DemoService> logger,
            IClientCredentialService clientCredentialService,
            IOptions<ConsoleAppSettings> consoleAppSettings,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _clientCredentialService = clientCredentialService;
            _consoleAppSettings = consoleAppSettings;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> MakeGraphApiCall()
        {
            var accessToken = await _clientCredentialService.GetAccessToken(
                new[] { "https://graph.microsoft.com/.default" });

            var httpClient = _httpClientFactory.CreateClient("GraphApi");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync("/v1.0/users");

            JsonNode? result = null;
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                result = JsonNode.Parse(json);
            }
            return result is null
                ? ""
                : result.ToJsonString();
        }

        public void WriteWelcomeMessage()
        {
            _logger.LogInformation("Saying hello");
            Console.WriteLine(_consoleAppSettings.Value?.WelcomeMessage);
        }
    }
}

