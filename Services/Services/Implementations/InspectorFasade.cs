using System;
using System.Threading.Tasks;
using Models.Enums;
using Services.Services.Interfaces;

namespace Services.Services.Implementations
{
    public class InspectorFasade : IInspectorFasade
    {
        private IInspectorBlockerService _inspectorBlockerService;

        private IMonitorActivityService _monitorActivityService;

        public InspectorFasade(IInspectorBlockerService inspectorBlockerService, IMonitorActivityService monitorActivityService)
        {
            _inspectorBlockerService = inspectorBlockerService;
            _monitorActivityService = monitorActivityService;
        }

        public async Task<TReturnModel> DoActionAsync<TReturnModel>(Func<Task<TReturnModel>> onWorkAsync, CustomerActivityType customerActivityType, object requestData)
        {
            await _inspectorBlockerService.WaitWhenInspectionIsActiveAsync();

            _monitorActivityService.AddUserActivity(customerActivityType, CustomerActivityPoint.Start, requestData);
            
            TReturnModel result = await onWorkAsync();

            _monitorActivityService.AddUserActivity(customerActivityType, CustomerActivityPoint.End);

            return result;
        }
    }
}