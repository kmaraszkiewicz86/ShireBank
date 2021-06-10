using System.Threading.Tasks;

namespace Services.Services.Interfaces.Services
{
    public interface IInspectorBlockerService
    {
        void Block();
        void ReleaseLock();
        Task WaitWhenInspectionIsActiveAsync();
    }
}