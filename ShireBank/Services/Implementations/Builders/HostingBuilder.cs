using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShireBank.Extensions;

namespace ShireBank.Services.Implementations.Builders
{
    public sealed class HostingBuilder
    {
        private IHostBuilder _hostingBuilder;

        public HostingBuilder InitializeRequiredServices(IConfiguration configuration)
        {
            _hostingBuilder = new HostBuilder()
                .ConfigureServices(serviceCollection => ConfigureServices(serviceCollection, configuration));

            return this;
        }

        public IServiceProvider Build()
        {
            return _hostingBuilder.Build().Services;
        }

        private void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
                .AddDbServices()
                .AddGrpcServices()
                .AddServices()
                .AddFasades()
                .AddLoggers()
                .AddFactories()
                .InitializeNlog(configuration);
        }
    }
}