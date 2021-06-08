using System.Threading.Tasks;
using Models.Models;
using Repository.Core;
using Repository.Services.Interfaces;

namespace Repository.Services.Implementations
{
    public sealed class AccountService : IAccountService
    {
        private readonly ShireBankDbContext _shireBankDbContext;

        public AccountService(ShireBankDbContext shireBankDbContext)
        {
            _shireBankDbContext = shireBankDbContext;
        }

        public async Task<ResultWithModel<uint>> OpenAccountAsync(OpenAccountRequestModel openAccountRequestModel)
        {
            return new ResultWithModel<uint>();
        }

        public async Task<Result> CloseAccountAsync(CloseAccountRequestModel closeAccountRequestModel)
        {
            return new Result(false);
        }
    }
}