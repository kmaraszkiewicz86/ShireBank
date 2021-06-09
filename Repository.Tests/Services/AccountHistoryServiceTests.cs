using System;
using System.Collections.Generic;
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
    public sealed class AccountHistoryServiceTests : DbServiceTestsBase
    {
        private AccountHistoryService _accountHistoryService;

        private List<AccountHistoryType> accountHistoryTypes => shireBankDbContext.AccountHistoryTypes.ToList();
        private List<Account> accounts => shireBankDbContext.Accounts.ToList();

        private List<AccountHistory> ExpectedAccountHistories => new List<AccountHistory>
            {
                new AccountHistory
                {
                    Account = accounts[0],
                    AccountHistoryType = accountHistoryTypes[1],
                    AmountOfFunds = 50,
                    AmountOfFoundsAfterOperation = 100,
                    HistoryDate = new DateTime(2020, 10, 10, 12, 9, 0)
                },
                new AccountHistory
                {
                    Account = accounts[0],
                    AccountHistoryType = accountHistoryTypes[2],
                    AmountOfFunds = 100,
                    AmountOfFoundsAfterOperation = -50,
                    HistoryDate = new DateTime(2020, 10, 10, 12, 11, 0)
                },
                new AccountHistory
                {
                    Account = accounts[0],
                    AmountOfFunds = 50,
                    AccountHistoryType = accountHistoryTypes[1],
                    AmountOfFoundsAfterOperation = 0,
                    HistoryDate = new DateTime(2020, 10, 10, 12, 15, 0)
                }
            };

        [SetUp]
        public async Task SetUpAsync()
        {
            InitializeDbContext();

            _accountHistoryService = new AccountHistoryService(shireBankDbContext);

            await InitializeTestDataAsync();
        }

        [Test]
        public async Task GetHistoryAsync_WhenUserFetchAccountWithHistory_ThenExpectedHistoryShouldBeFetchedFromDb()
        {
            ResultWithModel<List<AccountHistory>> result = await _accountHistoryService.GetHistoryAsync(new GetHistoryRequestModel(1));

            result.OperationFinisedWithSuccess.Should().BeTrue();
            result.ReturnType.Count.Should().Be(3);

            CheckIfHistoryFromDbIsCorrect(result);
        }

        [Test]
        public async Task GetHistoryAsync_WhenUserWantsToFetchHistoryFromNoExistsAccount_TheAccountHistoryShouldBeEmpty()
        {
            ResultWithModel<List<AccountHistory>> result = await _accountHistoryService.GetHistoryAsync(new GetHistoryRequestModel(10));

            result.OperationFinisedWithSuccess.Should().BeTrue();
            result.ReturnType.Should().BeEmpty();
        }

        private async Task InitializeTestDataAsync()
        {
            await shireBankDbContext.InitializeAccountHistoryTypeTestDataAsync();
            await shireBankDbContext.InitializeAccountTestDataAsync();

            foreach (AccountHistory expectedAccountHistory in ExpectedAccountHistories)
            {
                await shireBankDbContext.AccountHistories.AddAsync(expectedAccountHistory);
            }

            await shireBankDbContext.AccountHistories.AddAsync(new AccountHistory
            {
                Account = accounts[1],
                AccountHistoryType = accountHistoryTypes[2],
                AmountOfFunds = 100,
                AmountOfFoundsAfterOperation = -50,
                HistoryDate = new DateTime(2020, 10, 10, 13, 11, 0)
            });

            await shireBankDbContext.AccountHistories.AddAsync(new AccountHistory
            {
                Account = accounts[1],
                AmountOfFunds = 50,
                AccountHistoryType = accountHistoryTypes[1],
                AmountOfFoundsAfterOperation = 0,
                HistoryDate = new DateTime(2020, 10, 10, 13, 25, 0)
            });

            await shireBankDbContext.SaveChangesAsync();
        }

        private void CheckIfHistoryFromDbIsCorrect(ResultWithModel<List<AccountHistory>> result)
        {
            List<AccountHistory> expectedItems = ExpectedAccountHistories;

            for (var index = 0; index < result.ReturnType.Count; index++)
            {
                IsHistoryItemCorrect(index, result, expectedItems);
            }
        }

        private void IsHistoryItemCorrect(int currentIndex, ResultWithModel<List<AccountHistory>> result, List<AccountHistory> expectedItems)
        {
            AccountHistory accountHistory = result.ReturnType[currentIndex];
            AccountHistory expectedAccountHistory = expectedItems[currentIndex];

            accountHistory.Account.AccountId.Should().Be(expectedAccountHistory.Account.AccountId);
            accountHistory.AccountHistoryType.AccountHistoryTypeId.Should().Be(expectedAccountHistory.AccountHistoryType.AccountHistoryTypeId);
            accountHistory.AmountOfFunds.Should().Be(expectedAccountHistory.AmountOfFunds);
            accountHistory.AmountOfFoundsAfterOperation.Should().Be(expectedAccountHistory.AmountOfFoundsAfterOperation);
            accountHistory.HistoryDate.Should().Be(expectedAccountHistory.HistoryDate);
        }
    }
}