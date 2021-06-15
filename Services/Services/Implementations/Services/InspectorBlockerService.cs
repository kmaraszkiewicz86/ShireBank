using System.Threading;
using System.Threading.Tasks;
using Services.Services.Interfaces.Services;

namespace Services.Services.Implementations.Services
{
    public sealed class InspectorBlockerService : IInspectorBlockerService
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public void Block()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void ReleaseLock()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task WaitWhenInspectionIsActiveAsync()
        {
            while (_cancellationTokenSource.Token.IsCancellationRequested)
            {
                await Task.Delay(10);
            }
        }
    }
}