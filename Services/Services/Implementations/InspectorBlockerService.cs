using System;
using System.Threading;
using System.Threading.Tasks;
using Services.Services.Interfaces;

namespace Services.Services.Implementations
{
    public class InspectorBlockerService : IInspectorBlockerService
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