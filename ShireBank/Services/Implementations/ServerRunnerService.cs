using System;
using System.Threading.Tasks;
using Grpc.Core;
using SharedInterface;
using ShireBank.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SharedInterface.Interfaces.CustomInterface;
using SharedInterface.Interfaces.InspectorInterface;
using static SharedInterface.Interfaces.InspectorInterface.InspectorInterface;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;
using ShireBank.Services.Implementations.Builders;

namespace ShireBank.Services.Implementations
{
    public sealed class ServerRunnerService : IServerRunnerService
    {
        private readonly ILoggerService _loggerService;

        public ServerRunnerService(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public async Task RunAsync()
        {
            Server server = new Server
            {
                Ports = { new ServerPort(Constants.BankBaseAddressUri.Host, Constants.BankBaseAddressUri.Port, ServerCredentials.Insecure) },
                Services =
                {
                    CustomerInterface.BindService(HostingBuilder.ServiceProvider.GetService<CustomerInterfaceBase>()),
                    InspectorInterface.BindService(HostingBuilder.ServiceProvider.GetService<InspectorInterfaceBase>())
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