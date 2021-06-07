using System.Threading.Tasks;
using Grpc.Core;
using SharedInterface.Interfaces.InspectorInterface;
using static SharedInterface.Interfaces.InspectorInterface.InspectorInterface;

namespace ShireBank
{
    public class InspectorServiceHost : InspectorInterfaceBase
    {
        public override async Task<Empty> FinishInspectionAsync(Empty request, ServerCallContext context)
        {
            return new Empty();
        }

        public override async Task<GetFullSummaryResponse> GetFullSummaryAsync(Empty request, ServerCallContext context)
        {
            return new GetFullSummaryResponse { Summary = string.Empty };
        }

        public override async Task<Empty> StartInspectionAsync(Empty request, ServerCallContext context)
        {
            return new Empty();
        }
    }
}