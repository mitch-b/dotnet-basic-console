using ConsoleApp.Models.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleApp.Services
{
    internal interface IDemoService
    {
        void WriteWelcomeMessage();
    }

    internal class DemoService : IDemoService
    {
        private readonly ILogger<DemoService> _logger;
        private readonly IOptions<ConsoleAppSettings> _consoleAppSettings;

        public DemoService(ILogger<DemoService> logger,
            IOptions<ConsoleAppSettings> consoleAppSettings)
        {
            _logger = logger;
            _consoleAppSettings = consoleAppSettings;
        }

        public void WriteWelcomeMessage()
        {
            _logger.LogInformation("Saying hello");
            Console.WriteLine(_consoleAppSettings.Value?.WelcomeMessage);
        }
    }
}

