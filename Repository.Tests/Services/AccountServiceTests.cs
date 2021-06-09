using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Models.Entities;
using Models.Models;
using NUnit.Framework;
using Repository.Services.Implementations;
using Repository.Tests.TestData;

namespace Repository.Test.Services.Implementations
{
    public sealed class AccountServiceTests : DbServiceTestsBase
    {
        private AccountService _accountService;

        private OpenAccountRequestModel OpenAccountRequestModelTestData => new OpenAccountRequestModel("FirstName", "LastName", 111);

        [SetUp]
        public async Task SetUpAsync()
        {
            InitializeDbContext();

            _accountService = new AccountService(shireBankDbContext);

            await shireBankDbContext.InitializeAccountTestDataAsync();
        }

        [Test]
        public async Task OpenAccountAsync_WhenAddNewAccount_ThenResultShouldBeValidAndNewAccountWillAppearOnTheDatabase()
        {
            var expectedAccount = new Account
            {
                AccountId = 3,
                Amount = 0,
                DebitLimit = 111,
                Customer = new Customer
                {
                    CustomerId = 3,
                    FirstName = "PersonName1",
                    LastName = "PersonSurname2"
                }
            };

            ResultWithModel<uint>  result = await _accountService.OpenAccountAsync(new OpenAccountRequestModel("PersonName1", "PersonSurname2", 111));

            result.OperationFinisedWithSuccess.Should().BeTrue();
            result.ReturnType.Should().Be(3);
            shireBankDbContext.Accounts.Count().Should().Be(3);
            shireBankDbContext.Accounts.OrderBy(a => a.AccountId).Last().Should().BeEquivalentTo(expectedAccount);
        }

        [Test]
        public async Task OpenAccountAsync_WhenAccountAlreadyExists_ThenResultShouldBeInvalidAndNoNewAccountWillAppearOnTheDatabase()
        {
            ResultWithModel<uint> result = await _accountService.OpenAccountAsync(new OpenAccountRequestModel("PersonName1", "PersonSurname1", 111));

            result.OperationFinisedWithSuccess.Should().BeFalse();
            result.ReturnType.Should().Be(0);
            shireBankDbContext.Accounts.Count().Should().Be(2);
        }

        [Test]
        public async Task CloseAccountAsync_WhenAccountExists_ThenResultShouldBeValidAndAccountWillDisaperFromTheDatabase()
        {
            Result result = await _accountService.CloseAccountAsync(new CloseAccountRequestModel(2));

            result.OperationFinisedWithSuccess.Should().BeTrue();
            shireBankDbContext.Accounts.Count().Should().Be(1);
            shireBankDbContext.Accounts.First().AccountId.Should().Be(1);
        }

        [Test]
        public async Task CloseAccountAsync_WhenUserWantCloseAccountWithFundsOnIt_ThenResultShouldBeInvalid()
        {
            Result result = await _accountService.CloseAccountAsync(new CloseAccountRequestModel(1));

            result.OperationFinisedWithSuccess.Should().BeFalse();
            shireBankDbContext.Accounts.Count().Should().Be(2);
        }

        [Test]
        public async Task CloseAccountAsync_WhenAccountNotExists_ThenResultShouldBeInvalid()
        {
            Result result = await _accountService.CloseAccountAsync(new CloseAccountRequestModel(3));

            result.OperationFinisedWithSuccess.Should().BeFalse();
            shireBankDbContext.Accounts.Count().Should().Be(2);
        }
    }
}