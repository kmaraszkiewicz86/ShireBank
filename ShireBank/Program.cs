using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShireBank.Helper;
using ShireBank.Services.Implementations.Builders;
using ShireBank.Services.Interfaces;

namespace ShireBank
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = ConfigurationBuilderHelper.GetNLogConfig();

            IServiceProvider serviceProvider = new HostingBuilder()
                .InitializeRequiredServices(configuration)
                .Build();

            await serviceProvider.CreateDatabaseAsync();

            await serviceProvider.GetService<IServerRunnerService>().RunAsync();
        }
    }
}