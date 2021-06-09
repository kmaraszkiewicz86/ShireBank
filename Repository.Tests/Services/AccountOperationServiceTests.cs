using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Models.Entities;
using Models.Models;
using NUnit.Framework;
using Repository.Services.Implementations;

namespace Repository.Test.Services.Implementations
{
    public sealed class AccountOperationServiceTests : DbServiceTestsBase
    {
        private AccountOperationService _accountOperationService;

        [SetUp]
        public async Task SetUpAsync()
        {
            InitializeDbContext();

            var accountHistoryService = new AccountHistoryService(shireBankDbContext);
            _accountOperationService = new AccountOperationService(shireBankDbContext, accountHistoryService);

            await shireBankDbContext.Accounts.AddAsync(new Account { AccountId = 1, Amount = -200, DebitLimit = 200 });
            await shireBankDbContext.Accounts.AddAsync(new Account { AccountId = 2, Amount = 0, DebitLimit = 50 });
            await shireBankDbContext.Accounts.AddAsync(new Account { AccountId = 3, Amount = 1000, DebitLimit = 0 });
            await shireBankDbContext.Accounts.AddAsync(new Account { AccountId = 4, Amount = 50, DebitLimit = 50 });

            await shireBankDbContext.SaveChangesAsync();
        }

        [Test]
        public async Task DepositAsync_WhenUserDeposit_ThenUserShoulSeeValidAmountOfFundsOnAccount()
        {
            Result result = await _accountOperationService.DepositAsync(new DepositRequestModel(1, 250));

            result.OperationFinisedWithSuccess.Should().BeTrue();
            shireBankDbContext.Accounts.First(a => a.AccountId == 1).Amount.Should().Be(50);
        }

        [Test]
        public async Task DepositAsync_WhenTryToAddFundsToNotExistAccount_ThenResultShouldBeInvalid()
        {
            Result result = await _accountOperationService.DepositAsync(new DepositRequestModel(10, 250));

            result.OperationFinisedWithSuccess.Should().BeFalse();
        }

        [Test]
        public async Task WithdrawAsync_WhenUserWantsToGetMoreMoneyThenHeHasOnAccount_ThenReturnOnlyMoneyToZeroFromHisAccount()
        {
            ResultWithModel<float> result = await _accountOperationService.WithdrawAsync(new WithdrawRequestModel(3, 1200));

            result.OperationFinisedWithSuccess.Should().BeTrue();
            result.ReturnType.Should().Be(1000);
            shireBankDbContext.Accounts.First(a => a.AccountId == 3).Amount.Should().Be(0);
        }

        [Test]
        public async Task WithdrawAsync_WhenUserHasAccountWithDebitAndHeWantsToGetMoreMonayThenHas_ThenReturnOnlyMoneyToDebitLimit()
        {
            ResultWithModel<float> result = await _accountOperationService.WithdrawAsync(new WithdrawRequestModel(2, 1200));

            result.OperationFinisedWithSuccess.Should().BeTrue();
            result.ReturnType.Should().Be(50);
            shireBankDbContext.Accounts.First(a => a.AccountId == 2).Amount.Should().Be(-50);
        }

        [Test]
        public async Task WithdrawAsync_WhenUserHasAccountWithDebitAndHeWantsToGetFundsFromDebit_ThenReturnDebitFounds()
        {
            ResultWithModel<float> result = await _accountOperationService.WithdrawAsync(new WithdrawRequestModel(4, 100));

            result.OperationFinisedWithSuccess.Should().BeTrue();
            result.ReturnType.Should().Be(100);
            shireBankDbContext.Accounts.First(a => a.AccountId == 4).Amount.Should().Be(-50);
        }

        [Test]
        public async Task WithdrawAsync_WhenTryToGetFundsFromNotExistAccount_ThenResultShouldBeInvalid()
        {
            ResultWithModel<float> result = await _accountOperationService.WithdrawAsync(new WithdrawRequestModel(10, 100));

            result.OperationFinisedWithSuccess.Should().BeFalse();
        }
    }
}