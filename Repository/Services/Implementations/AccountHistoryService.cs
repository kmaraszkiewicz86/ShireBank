using System.Threading.Tasks;
using Models.Models;
using Repository.Core;
using Repository.Services.Interfaces;

namespace Repository.Services.Implementations
{
    public sealed class AccountHistoryService : IAccountHistoryService
    {
        private readonly ShireBankDbContext _shireBankDbContext;

        public AccountHistoryService(ShireBankDbContext shireBankDbContext)
        {
            _shireBankDbContext = shireBankDbContext;
        }

        public async Task<ResultWithModel<string>> GetHistoryAsync(GetHistoryRequestModel getHistoryRequestModel)
        {
            return new ResultWithModel<string>();
        }
    }
}