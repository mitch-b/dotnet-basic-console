using ConsoleApp.Services;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp
{
    internal class WorkerService : IHostedService
	{
        private readonly IDemoService _demoService;

		public WorkerService(IDemoService demoService)
		{
            _demoService = demoService;
		}

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var response = await _demoService.MakeGraphApiCall();
            if (!string.IsNullOrWhiteSpace(response))
            {
                Console.WriteLine(response);
            }

            while (true)
            {
                _demoService.WriteWelcomeMessage();
                await Task.Delay(3000);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

