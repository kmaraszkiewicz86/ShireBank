using System.Threading.Tasks;
using Models.Models;
using Repository.Core;
using Repository.Services.Interfaces;

namespace Repository.Services.Implementations
{
    public sealed class AccountOperationService : IAccountOperationService
    {
        private readonly ShireBankDbContext _shireBankDbContext;

        public AccountOperationService(ShireBankDbContext shireBankDbContext)
        {
            _shireBankDbContext = shireBankDbContext;
        }

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