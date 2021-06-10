using System.Threading;
using Services.Services.Interfaces;

namespace Services.Services.Implementations
{
    public class InspectorBlockerService : IInspectorBlockerService
    {
        private ManualResetEvent manualResetEvent;

        public InspectorBlockerService()
        {
            manualResetEvent = new ManualResetEvent(true);
        }

        public void Block()
        {
            manualResetEvent.Reset();
        }

        public void ReleaseLock()
        {
            manualResetEvent.Set();
        }

        public void WaitWhenInspectionIsActive()
        {
            manualResetEvent.WaitOne();
        }
    }
}