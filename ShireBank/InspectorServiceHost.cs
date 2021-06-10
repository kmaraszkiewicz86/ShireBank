using System.Threading.Tasks;
using Grpc.Core;
using Services.Services.Interfaces;
using SharedInterface.Interfaces.InspectorInterface;
using static SharedInterface.Interfaces.InspectorInterface.InspectorInterface;

namespace ShireBank
{
    public class InspectorServiceHost : InspectorInterfaceBase
    {
        private IInspectorBlockerService _inspectorBlockerService;

        private IMonitorActivityService _monitorActivityService;

        public InspectorServiceHost(IInspectorBlockerService inspectorBlockerService, 
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