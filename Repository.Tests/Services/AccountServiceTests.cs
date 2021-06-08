using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Models.Entities;
using Models.Models;
using NUnit.Framework;
using Repository.Core;
using Repository.Services.Implementations;

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

            Customer[] customers = new[]
            {
                new Customer { CustomerId = 1, FirstName = "PersonName1", LastName = "PersonSurname1" },
                new Customer { CustomerId = 2, FirstName = "PersonName2", LastName = "PersonSurname2" }
            };

            foreach (Customer customer in customers)
            {
                await shireBankDbContext.Customers.AddAsync(customer);
            }

            await shireBankDbContext.Accounts.AddAsync(new Account { AccountId = 1, Amount = 0, DebitLimit = 100, Customer = customers[0] });
            await shireBankDbContext.Accounts.AddAsync(new Account { AccountId = 2, Amount = 0, DebitLimit = 300, Customer = customers[1] });

            await shireBankDbContext.SaveChangesAsync();
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
        public async Task CloseAccountAsync_WhenAccountNotExists_ThenResultShouldBeInvalid()
        {
            Result result = await _accountService.CloseAccountAsync(new CloseAccountRequestModel(3));

            result.OperationFinisedWithSuccess.Should().BeFalse();
            shireBankDbContext.Accounts.Count().Should().Be(2);
        }
    }
}