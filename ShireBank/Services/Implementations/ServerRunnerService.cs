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

        private readonly CustomerInterfaceBase _customerInterfaceBase;

        private readonly InspectorInterfaceBase _inspectorInterfaceBase;

        public ServerRunnerService(ILoggerService loggerService, 
            CustomerInterfaceBase customerInterfaceBase, 
            InspectorInterfaceBase inspectorInterfaceBase)
        {
            _loggerService = loggerService;
            _customerInterfaceBase = customerInterfaceBase;
            _inspectorInterfaceBase = inspectorInterfaceBase;
        }

        public async Task RunAsync()
        {
            Server server = new Server
            {
                Ports = { new ServerPort(Constants.BankBaseAddressUri.Host, Constants.BankBaseAddressUri.Port, ServerCredentials.Insecure) },
                Services =
                {
                    CustomerInterface.BindService(_customerInterfaceBase),
                    InspectorInterface.BindService(_inspectorInterfaceBase)
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