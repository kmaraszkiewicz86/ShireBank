using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using SharedInterface;
using SharedInterface.Interfaces.CustomInterface;
using SharedInterface.Interfaces.InspectorInterface;
using ShireBank.Services.Interfaces;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;
using static SharedInterface.Interfaces.InspectorInterface.InspectorInterface;

namespace ShireBank.Services.Implementations
{
    public sealed class ServerRunnerService : IServerRunnerService
    {
        private readonly ILoggerService _loggerService;

        private readonly IServiceProvider _serviceProvider;

        public ServerRunnerService(ILoggerService loggerService, 
            IServiceProvider serviceProvider)
        {
            _loggerService = loggerService;
            _serviceProvider = serviceProvider;
        }

        public async Task RunAsync()
        {
            Server server = new Server
            {
                Ports = { new ServerPort(Constants.BankBaseAddressUri.Host, Constants.BankBaseAddressUri.Port, ServerCredentials.Insecure) },
                Services =
                {
                    CustomerInterface.BindService(_serviceProvider.GetService<CustomerInterfaceBase>()),
                    InspectorInterface.BindService(_serviceProvider.GetService<InspectorInterfaceBase>())
                }
                
            };

            try
            {
                server.Start();

                _loggerService.LogInformation($"Starting server on port {Constants.BankBaseAddressUri.Port}");
                _loggerService.LogInformation("Press any key to stop...");

                Console.ReadLine();

                _loggerService.LogInformation("Service stopped...");

                await server.ShutdownAsync();
            }
            catch (Exception exception)
            {
                _loggerService.LogError(exception);
            }
        }
    }
}