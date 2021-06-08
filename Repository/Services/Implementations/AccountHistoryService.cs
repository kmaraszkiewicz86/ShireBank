using System.Threading.Tasks;
using Models.Models;
using Repository.Services.Interfaces;

namespace Repository.Services.Implementations
{
    public class AccountHistoryService : IAccountHistoryService
    {
        public async Task<ResultWithModel<string>> GetHistoryAsync(GetHistoryRequestModel getHistoryRequestModel)
        {
            return new ResultWithModel<string>();
        }
    }
}