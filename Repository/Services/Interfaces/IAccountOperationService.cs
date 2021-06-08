using System.Threading.Tasks;
using Models.Models;

namespace Repository.Services.Interfaces
{
    public interface IAccountOperationService
    {
        Task<ResultWithModel<float>> WithdrawAsync(WithdrawRequestModel withdrawRequestModel);

        Task<Result> DepositAsync(DepositRequestModel depositRequestModel);
    }
}