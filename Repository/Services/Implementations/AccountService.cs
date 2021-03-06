using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
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
            Customer customer = await GetCustomerAsync(openAccountRequestModel);

            if (customer != null)
                return new ResultWithModel<uint>();

            Customer newCustomer = await AddCustomerAsync(openAccountRequestModel);

            Account newAccount = await CreateAccountAsync(openAccountRequestModel, newCustomer);

            await _shireBankDbContext.SaveChangesAsync();

            return new ResultWithModel<uint>(newAccount.AccountId);
        }

        public async Task<Result> CloseAccountAsync(CloseAccountRequestModel closeAccountRequestModel)
        {
            Account accountToRemove = await _shireBankDbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == closeAccountRequestModel.Account);

            if (accountToRemove == null || accountToRemove.Amount != 0)
                return new Result(false);

            _shireBankDbContext.Accounts.Remove(accountToRemove);

            await _shireBankDbContext.SaveChangesAsync();

            return new Result(true);
        }

        private async Task<Customer> GetCustomerAsync(OpenAccountRequestModel openAccountRequestModel)
        {
            return await _shireBankDbContext.Customers.FirstOrDefaultAsync(
                c => c.FirstName == openAccountRequestModel.FirstName && c.LastName == openAccountRequestModel.LastName);
        }

        private async Task<Customer> AddCustomerAsync(OpenAccountRequestModel openAccountRequestModel)
        {
            var newCustomer = new Customer { FirstName = openAccountRequestModel.FirstName, LastName = openAccountRequestModel.LastName };

            await _shireBankDbContext.Customers.AddAsync(newCustomer);

            return newCustomer;
        }

        private async Task<Account> CreateAccountAsync(OpenAccountRequestModel openAccountRequestModel, Customer newCustomer)
        {
            var account = new Account { Amount = 0, DebitLimit = openAccountRequestModel.DebtLimit, Customer = newCustomer };

            await _shireBankDbContext.Accounts.AddAsync(account);

            return account;
        }
    }
}