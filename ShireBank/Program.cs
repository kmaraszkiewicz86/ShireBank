using System;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using SharedInterface;
using SharedInterface.Interfaces.CustomInterface;
using SharedInterface.Interfaces.InspectorInterface;
using ShireBank.Helper;
using ShireBank.Services.Builders;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;
using static SharedInterface.Interfaces.InspectorInterface.InspectorInterface;

namespace ShireBank
{
    class Program
    {
        static void Main(string[] args)
        {
            new HostingBuilder()
                .InitializeRequiredServices()
                .Build();

            DatabaseCreatorHelper.CreateDatabase();

            Server server = new Server
            {
                Ports = { new ServerPort(Constants.BankBaseAddressUri.Host, Constants.BankBaseAddressUri.Port, ServerCredentials.Insecure) },
                Services =
                {
                    InspectorInterface.BindService(HostingBuilder.ServiceProvider.GetService<InspectorInterfaceBase>()),
                    CustomerInterface.BindService(HostingBuilder.ServiceProvider.GetService<CustomerInterfaceBase>())
                }
            };

            server.Start();

            Console.WriteLine($"Starting server on port {Constants.BankBaseAddressUri.Port}");
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}