using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repository.Core;

namespace Repository.Tests.TestData
{
    public static class AccountHistoryTypeDbInitializatorHelper
    {
        public static async Task InitializeAccountHistoryTypeTestDataAsync(this ShireBankDbContext shireBankDbContext)
        {
            await shireBankDbContext.AddIdNewItemIfNotExists(new AccountHistoryType { AccountHistoryTypeId = 1, Name = "OpenAccount" });
            await shireBankDbContext.AddIdNewItemIfNotExists(new AccountHistoryType { AccountHistoryTypeId = 2, Name = "Deposit" });
            await shireBankDbContext.AddIdNewItemIfNotExists(new AccountHistoryType { AccountHistoryTypeId = 3, Name = "Withdraw" });

            await shireBankDbContext.SaveChangesAsync();
        }

        private static async Task AddIdNewItemIfNotExists(this ShireBankDbContext shireBankDbContext, AccountHistoryType accountHistoryType)
        {
            AccountHistoryType accountHistoryTypeFromDb =
                await shireBankDbContext.AccountHistoryTypes.FirstOrDefaultAsync(aht => aht.AccountHistoryTypeId == accountHistoryType.AccountHistoryTypeId);

            if (accountHistoryTypeFromDb != null)
            {
                return;
            }

            await shireBankDbContext.AccountHistoryTypes.AddAsync(accountHistoryType);
        }
    }
}
