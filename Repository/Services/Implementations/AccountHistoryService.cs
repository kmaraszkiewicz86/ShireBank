using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Enums;
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

        public async Task<ResultWithModel<List<AccountHistory>>> GetHistoryAsync(GetHistoryRequestModel getHistoryRequestModel)
        {
            List<AccountHistory> accountHistories = await _shireBankDbContext.AccountHistories
                .Include(a => a.Account)
                .Include(a => a.AccountHistoryType)
                .Where(a => a.Account.AccountId == getHistoryRequestModel.Account)
                .OrderBy(a => a.HistoryDate)
                ?.ToListAsync() ?? new List<AccountHistory>();

            return new ResultWithModel<List<AccountHistory>>(accountHistories);
        }

        public async Task AddHistoryAsync(AccountHistoryTypeOperation accountHistoryTypeOperation, Account account, float amountOfFunds)
        {
            var accountHistoryTypeFromDb = await _shireBankDbContext.AccountHistoryTypes.FirstOrDefaultAsync(at => at.Name == accountHistoryTypeOperation.ToString());

            if (accountHistoryTypeFromDb != null)
            {
                return;
            }

            _shireBankDbContext.AccountHistories.Add(new AccountHistory
            {
                Account = account,
                AccountHistoryType = accountHistoryTypeFromDb,
                AmountOfFunds = amountOfFunds,
                AmountOfFoundsAfterOperation = account.Amount,
                HistoryDate = System.DateTime.Now
            });
        }
    }
}