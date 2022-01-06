using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Okta.Sdk.Issue529
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOktaClient _oktaClient;

        public Worker(ILogger<Worker> logger, IOktaClient oktaClient)
        {
            _logger = logger;
            _oktaClient = oktaClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var user = await _oktaClient.Users.GetUserAsync("fakeUserId", stoppingToken);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
