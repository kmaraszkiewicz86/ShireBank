using System.Threading.Tasks;
using Grpc.Net.Client;
using SharedInterface.Interfaces.CustomInterface;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;

namespace CustomerTest.Services
{
    public class CustomerService
    {
        private readonly CustomerInterfaceClient _customerInterfaceClient;

        public CustomerService(GrpcChannel channel)
        {
            _customerInterfaceClient = new CustomerInterfaceClient(channel);
        }

        public async Task<uint?> OpenAccount(string firstName, string lastName, float debtLimit)
        {
            var request = new OpenAccountRequest
            {
                FirstName = firstName,
                LastName = lastName,
                DebtLimit = debtLimit
            };

            OpenAccountResponse response = await _customerInterfaceClient.OpenAccountAsyncAsync(request);

            return response.FinishedWitSuccess ? response.AccountId : null;
        }

        public async Task Deposit(uint account, float amount)
        {
            var request = new DepositRequest
            {
                Account = account,
                Amount = amount
            };

            await _customerInterfaceClient.DepositAsyncAsync(request);
        }

        public async Task<float> Withdraw(uint account, float amount)
        {
            var request = new WithdrawRequest
            {
                Account = account,
                Amount = amount
            };

            WithdrawResponse response = await _customerInterfaceClient.WithdrawAsyncAsync(request);

            return response.Amount;
        }

        public async Task<string> GetHistory(uint account)
        {
            var request = new GetHistoryRequest
            {
                Account = account
            };

            GetHistoryResponse response = await _customerInterfaceClient.GetHistoryAsyncAsync(request);

            return response.BankHistory;
        }

        public async Task<bool> CloseAccount(uint account)
        {
            var request = new CloseAccountRequest
            {
                Account = account
            };

            CloseAccountResponse response = await _customerInterfaceClient.CloseAccountAsyncAsync(request);

            return response.FinishedWitSuccess;
        }
    }
}