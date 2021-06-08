using System;
using System.Threading.Tasks;
using Grpc.Core;
using SharedInterface;
using ShireBank.Services.Interfaces;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;
using static SharedInterface.Interfaces.InspectorInterface.InspectorInterface;

namespace ShireBank.Services.Implementations
{
    public sealed class ServerRunnerService : IServerRunnerService
    {
        private readonly ILoggerService _loggerService;

        private readonly InspectorInterfaceBase _inspectorInterfaceBase;

        private readonly CustomerInterfaceBase _customerInterfaceBase;

        public ServerRunnerService(ILoggerService loggerService,
            InspectorInterfaceBase inspectorInterfaceBase,
            CustomerInterfaceBase customerInterfaceBase)
        {
            _loggerService = loggerService;
            _inspectorInterfaceBase = inspectorInterfaceBase;
            _customerInterfaceBase = customerInterfaceBase;
        }

        public async Task RunAsync()
        {
            Server server = new Server
            {
                Ports = { new ServerPort(Constants.BankBaseAddressUri.Host, Constants.BankBaseAddressUri.Port, ServerCredentials.Insecure) },
                Services =
                {
                    BindService(_inspectorInterfaceBase),
                    BindService(_customerInterfaceBase)
                }
            };

            try
            {
                server.Start();

                _loggerService.LogInformation($"Starting server on port {Constants.BankBaseAddressUri.Port}");
                _loggerService.LogInformation("Press any key to stop...");

                Console.ReadKey();

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