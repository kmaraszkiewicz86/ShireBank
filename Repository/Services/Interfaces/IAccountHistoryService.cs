using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;
using Models.Enums;
using Models.Models;
using Repository.Core;

namespace Repository.Services.Interfaces
{
    public interface IAccountHistoryService
    {
        Task<ResultWithModel<List<AccountHistory>>> GetHistoryAsync(GetHistoryRequestModel getHistoryRequestModel);

        Task AddHistoryAsync(ShireBankDbContext shireBankDbContext, AccountHistoryTypeOperation accountHistoryTypeOperation, Account account, float amountOfFunds);
    }
}