using System.Threading.Tasks;
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

            _accountOperationService = new AccountOperationService(shireBankDbContext);
        }

        [Test]
        public async Task DepositAsync_WhenAddZeroFounds_ThenReturn()
        {

        }

        [Test]
        public async Task WithdrawAsync_WhenAddZeroFounds_ThenReturn()
        {

        }
    }
}