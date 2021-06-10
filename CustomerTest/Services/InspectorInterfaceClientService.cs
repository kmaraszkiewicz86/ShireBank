using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using SharedInterface.Interfaces.InspectorInterface;
using static SharedInterface.Interfaces.InspectorInterface.InspectorInterface;

namespace CustomerTest.Services
{
    public class InspectorInterfaceClientService
    {
        private readonly InspectorInterfaceClient _inspectorInterfaceClient;

        public InspectorInterfaceClientService(GrpcChannel channel)
        {
            _inspectorInterfaceClient = new InspectorInterfaceClient(channel);
        }

        public void FinishInspection()
        {
            _inspectorInterfaceClient.FinishInspectionAsync(new Empty());
        }

        public void GetFullSummary()
        {
            Console.WriteLine("Summary of inspection:");
            Console.WriteLine(_inspectorInterfaceClient.GetFullSummaryAsync(new Empty()).Summary);
        }

        public void StartInspection()
        {
            _inspectorInterfaceClient.StartInspectionAsync(new Empty());
        }
    }
}