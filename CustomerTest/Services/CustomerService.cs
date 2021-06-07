using System;
using Grpc.Net.Client;
using SharedInterface.Interfaces.CustomInterface;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;

namespace CustomerTest.Services
{
    public class CustomerService : IDisposable
    {
        private readonly GrpcChannel _channel;
        private readonly CustomerInterfaceClient _customerInterfaceClient;

        public CustomerService(string fullBankAddress)
        {
            _channel = GrpcChannel.ForAddress(fullBankAddress);
            _customerInterfaceClient = new CustomerInterfaceClient(_channel);

        }

        public uint? OpenAccount(string firstName, string lastName, float debtLimit)
        {
            var request = new OpenAccountRequest
            {
                FirstName = firstName,
                LastName = lastName,
                DebtLimit = debtLimit
            };

            OpenAccountResponse response = _customerInterfaceClient.OpenAccountAsync(request);

            return response.AccountId;
        }

        public void Deposit(uint account, float amount)
        {
            var request = new DepositRequest
            {
                Account = account,
                Amount = amount
            };

            _customerInterfaceClient.DepositAsync(request);
        }

        public float Withdraw(uint account, float amount)
        {
            var request = new WithdrawRequest
            {
                Account = account,
                Amount = amount
            };

            WithdrawResponse response = _customerInterfaceClient.WithdrawAsync(request);

            return response.Amount;
        }

        public string GetHistory(uint account)
        {
            var request = new GetHistoryRequest
            {
                Account = account
            };

            GetHistoryResponse response = _customerInterfaceClient.GetHistoryAsync(request);

            return response.BankHistory;
        }

        public bool CloseAccount(uint account)
        {
            var request = new CloseAccountRequest
            {
                Account = account
            };

            CloseAccountResponse response = _customerInterfaceClient.CloseAccountAsync(request);

            return response.FinishedWitSuccess;
        }

        public void Dispose()
        {
            _channel?.Dispose();
        }
    }
}