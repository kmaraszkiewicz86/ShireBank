using System.Threading.Tasks;
using Models.Models;
using Repository.Services.Interfaces;

namespace Repository.Services.Implementations
{
    public sealed class AccountOperationService : IAccountOperationService
    {
        public async Task<Result> DepositAsync(DepositRequestModel depositRequestModel)
        {
            return new Result(false); 
        }

        public async Task<ResultWithModel<float>> WithdrawAsync(WithdrawRequestModel withdrawRequestModel)
        {
            return new ResultWithModel<float>();
        }
    }
}