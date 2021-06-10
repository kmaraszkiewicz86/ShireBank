using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Enums;
using Models.Models;
using Repository.Core;
using Repository.Services.Interfaces;

namespace Repository.Services.Implementations
{
    public sealed class AccountOperationService : IAccountOperationService
    {
        private readonly ShireBankDbContext _shireBankDbContext;

        private readonly IAccountHistoryService _accountHistoryService;

        public AccountOperationService(ShireBankDbContext shireBankDbContext,
            IAccountHistoryService accountHistoryService)
        {
            _shireBankDbContext = shireBankDbContext;
            _accountHistoryService = accountHistoryService;
        }

        public async Task<Result> DepositAsync(DepositRequestModel depositRequestModel)
        {
            using (var transaction = await _shireBankDbContext.Database.BeginTransactionAsync())
            {
                Account account = await GetAccountAsync(depositRequestModel.Account);

                if (account == null)
                    return new Result(false);

                account.Amount += depositRequestModel.Amount;
                _shireBankDbContext.Update(account);

                await _accountHistoryService.AddHistoryAsync(_shireBankDbContext, AccountHistoryTypeOperation.Deposit, account, depositRequestModel.Amount);

                await _shireBankDbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return new Result(true);
            }
        }

        public async Task<ResultWithModel<float>> WithdrawAsync(WithdrawRequestModel withdrawRequestModel)
        {
            using (var transaction = await _shireBankDbContext.Database.BeginTransactionAsync())
            {
                Account account = await GetAccountAsync(withdrawRequestModel.Account);

                if (account == null)
                    return new ResultWithModel<float>();

                float fundsToGet = GetFoundsToWithDraw(account, withdrawRequestModel);

                account.Amount -= fundsToGet;

                _shireBankDbContext.Update(account);

                await _accountHistoryService.AddHistoryAsync(_shireBankDbContext, AccountHistoryTypeOperation.Withdraw, account, fundsToGet);

                await _shireBankDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ResultWithModel<float>(fundsToGet);
            }
        }

        private async Task<Account> GetAccountAsync(uint accountId)
        {
            return await _shireBankDbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        private float GetFoundsToWithDraw(Account account, WithdrawRequestModel withdrawRequestModel)
        {
            float fundsToGet = withdrawRequestModel.Asmount;
            float maximumFundsToGet = account.Amount + account.DebitLimit;

            if (maximumFundsToGet < fundsToGet)
            {
                fundsToGet = maximumFundsToGet;
            }

            return fundsToGet;
        }
    }
}