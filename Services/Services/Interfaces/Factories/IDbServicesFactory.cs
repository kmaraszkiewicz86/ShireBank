using Repository.Services.Interfaces;
using Services.Services.Interfaces.Fasades;

namespace Services.Services.Interfaces.Factories
{
    public interface IDbServicesFactory
    {
        IAccountService AccountService { get; }
        IAccountOperationService AccountOperationService { get; }
        IAccountHistoryFasade AccountHistoryFasade { get; }
    }
}