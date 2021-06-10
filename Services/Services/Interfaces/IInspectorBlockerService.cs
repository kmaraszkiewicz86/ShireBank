using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IInspectorBlockerService
    {
        void Block();
        void ReleaseLock();
        Task WaitWhenInspectionIsActiveAsync();
    }
}