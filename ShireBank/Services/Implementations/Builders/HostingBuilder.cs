using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ShireBank.Extensions;

namespace ShireBank.Services.Implementations.Builders
{
    public class HostingBuilder
    {
        public static IServiceProvider ServiceProvider;

        private IHostBuilder _hostingBuilder;

        public HostingBuilder InitializeRequiredServices(IConfiguration configuration)
        {
            _hostingBuilder = new HostBuilder()
                .ConfigureServices(serviceCollection => ConfigureServices(serviceCollection, configuration));

            return this;
        }

        public void Build()
        {
            ServiceProvider = _hostingBuilder.Build().Services;
        }

        private void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
                .AddDbServices()
                .AddGrpcServices()
                .AddServices()
                .AddFasades()
                .AddLoggers()
                .InitializeNlog(configuration);
        }
    }
}