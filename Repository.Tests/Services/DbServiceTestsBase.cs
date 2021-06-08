using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Repository.Core;

namespace Repository.Test.Services.Implementations
{
    public abstract class DbServiceTestsBase
    {
        protected ShireBankDbContext shireBankDbContext;

        protected void InitializeDbContext()
        {
            var testDbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("FileName = TestContext")
                .Options;

            shireBankDbContext = new ShireBankDbContext(testDbOptions);
        }

        [TearDown]
        public async Task TearDownAsync()
        {
            if (shireBankDbContext == null)
                return;

            shireBankDbContext.RemoveRange(shireBankDbContext.Accounts.ToList());
            shireBankDbContext.RemoveRange(shireBankDbContext.AccountHistories.ToList());
            shireBankDbContext.RemoveRange(shireBankDbContext.Customers.ToList());

            await shireBankDbContext.SaveChangesAsync();

            await shireBankDbContext.DisposeAsync();
        }

        private void RemoveRange(List<object> items)
        {
            var accounts = shireBankDbContext.Accounts.ToList();
            for (var index = 0; index < items.Count; index++)
            {
                var accountToRemove = accounts[index];
                shireBankDbContext.Remove(accountToRemove);
            }
        }
    }
}