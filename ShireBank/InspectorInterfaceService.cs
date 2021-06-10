using System.Threading.Tasks;
using Grpc.Core;
using Services.Services.Interfaces.Services;
using SharedInterface.Interfaces.InspectorInterface;
using static SharedInterface.Interfaces.InspectorInterface.InspectorInterface;

namespace ShireBank
{
    public sealed class InspectorInterfaceService : InspectorInterfaceBase
    {
        private IInspectorBlockerService _inspectorBlockerService;

        private IMonitorActivityService _monitorActivityService;

        public InspectorInterfaceService(IInspectorBlockerService inspectorBlockerService, 
            IMonitorActivityService monitorActivityService)
        {
            _inspectorBlockerService = inspectorBlockerService;
            _monitorActivityService = monitorActivityService;
        }

        public override Task<Empty> FinishInspectionAsync(Empty request, ServerCallContext context)
        {
            _inspectorBlockerService.ReleaseLock();

            return Task.FromResult(new Empty());
        }

        public override Task<GetFullSummaryResponse> GetFullSummaryAsync(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new GetFullSummaryResponse { Summary = _monitorActivityService.ActivityLoggerStringBuilder.ToString() });
        }

        public override Task<Empty> StartInspectionAsync(Empty request, ServerCallContext context)
        {
            _inspectorBlockerService.Block();

            return Task.FromResult(new Empty());
        }
    }
}