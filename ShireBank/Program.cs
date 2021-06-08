using System;
using Grpc.Core;
using SharedInterface;
using SharedInterface.Interfaces.CustomInterface;
using SharedInterface.Interfaces.InspectorInterface;
using ShireBank.Services.Builders;
using Microsoft.Extensions.DependencyInjection;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;

namespace ShireBank
{
    class Program
    {
        static void Main(string[] args)
        {
            new HostingBuilder()
                .InitializeRequiredServices()
                .Build();

            var customerInterfaceBase = HostingBuilder.ServiceProvider.GetService<CustomerInterfaceBase>();

            Server server = new Server
            {
                Ports = { new ServerPort(Constants.BankBaseAddressUri.Host, Constants.BankBaseAddressUri.Port, ServerCredentials.Insecure) },
                Services =
                {
                    InspectorInterface.BindService(new InspectorServiceHost()),
                    CustomerInterface.BindService(customerInterfaceBase)
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