using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;
using Models.Enums;
using Models.Models;

namespace Repository.Services.Interfaces
{
    public interface IAccountHistoryService
    {
        Task<ResultWithModel<List<AccountHistory>>> GetHistoryAsync(GetHistoryRequestModel getHistoryRequestModel);

        Task AddHistoryAsync(AccountHistoryTypeOperation accountHistoryTypeOperation, Account account, float amountOfFunds);
    }
}