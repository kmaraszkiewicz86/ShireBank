using System.Threading.Tasks;
using NUnit.Framework;
using Repository.Services.Implementations;

namespace Repository.Test.Services.Implementations
{
    public sealed class AccountHistoryServiceTests : DbServiceTestsBase
    {
        private AccountHistoryService _accountHistoryService;

        [SetUp]
        public async Task SetUpAsync()
        {
            InitializeDbContext();

            _accountHistoryService = new AccountHistoryService(shireBankDbContext);
        }

        [Test]
        public async Task GetHistoryAsync()
        {
        }
    }
}