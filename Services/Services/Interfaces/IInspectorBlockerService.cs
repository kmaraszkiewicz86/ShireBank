namespace Services.Services.Interfaces
{
    public interface IInspectorBlockerService
    {
        void Block();
        void ReleaseLock();
        void WaitWhenInspectionIsActive();
    }
}