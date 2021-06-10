using System.Threading.Tasks;
using Services.Services.Interfaces.Services;

namespace Services.Services.Implementations.Services
{
    public sealed class InspectorBlockerService : IInspectorBlockerService
    {
        private bool _shouldWaitForSignal = false;

        public void Block()
        {
            _shouldWaitForSignal = true;
        }

        public void ReleaseLock()
        {
            _shouldWaitForSignal = false;
        }

        public async Task WaitWhenInspectionIsActiveAsync()
        {
            while (_shouldWaitForSignal)
            {
                await Task.Delay(10);
            }
        }
    }
}