using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShireBank.Extensions;

namespace ShireBank.Services.Builders
{
    public class HostingBuilder
    {
        public static IServiceProvider ServiceProvider;

        private IHostBuilder _hostingBuilder;

        public HostingBuilder InitializeRequiredServices()
        {
            _hostingBuilder = new HostBuilder()
                .ConfigureServices(ConfigureServices);

            return this;
        }

        public void Build()
        {
            ServiceProvider = _hostingBuilder.Build().Services;
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddDbServices()
                .AddGrpcServices();
        }
    }
}