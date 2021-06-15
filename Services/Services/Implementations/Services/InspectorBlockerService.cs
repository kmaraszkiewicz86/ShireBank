using System.Threading;
using System.Threading.Tasks;
using Services.Services.Interfaces.Services;

namespace Services.Services.Implementations.Services
{
    public sealed class InspectorBlockerService : IInspectorBlockerService
    {
        private CancellationTokenSource _cancellationTokenSource;

        public InspectorBlockerService()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.Cancel();
        }

        public void Block()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void ReleaseLock()
        {
            _cancellationTokenSource?.Cancel();
        }

        public async Task WaitWhenInspectionIsActiveAsync()
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                await Task.Delay(10);
            }
        }
    }
}