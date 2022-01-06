using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Okta.Sdk.Configuration;

namespace Okta.Sdk.Issue529
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var oktaClientConfig = hostContext.Configuration
                        .GetSection("Okta:Client")
                        .Get<OktaClientConfiguration>();

                    services.AddSingleton<IOktaClient>(_ => new OktaClient(oktaClientConfig));
                    services.AddHostedService<Worker>();
                });
    }
}
