using System.Threading.Tasks;
using Grpc.Core;
using Models.Models;
using Repository.Services.Interfaces;
using SharedInterface.Extensions;
using SharedInterface.Interfaces.CustomInterface;
using ShireBank.Services.Implementations.Builders;
using Microsoft.Extensions.DependencyInjection;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;

namespace ShireBank
{
    public class CustomerServiceHost : CustomerInterfaceBase
    {
        private IAccountService _accountService;

        private IAccountOperationService _accountOperationService;

        private IAccountHistoryService _accountHistoryService;

        public void InitializeServices()
        {
            _accountService = HostingBuilder.ServiceProvider.GetService<IAccountService>();
            _accountOperationService = HostingBuilder.ServiceProvider.GetService<IAccountOperationService>();
            _accountHistoryService = HostingBuilder.ServiceProvider.GetService<IAccountHistoryService>();
        }

        public override async Task<OpenAccountResponse> OpenAccountAsync(OpenAccountRequest request, ServerCallContext context)
        {
            InitializeServices();

            ResultWithModel<uint> result = await _accountService.OpenAccountAsync(request.ToOpenAccountRequestModel());

            return new OpenAccountResponse { FinishedWitSuccess = result.OperationFinisedWithSuccess, AccountId = result.ReturnType };
        }

        public override async Task<WithdrawResponse> WithdrawAsync(WithdrawRequest request, ServerCallContext context)
        {
            InitializeServices();

            ResultWithModel<float> result = await _accountOperationService.WithdrawAsync(request.ToWithdrawRequestExtension());

            return new WithdrawResponse { Amount = result.ReturnType };
        }

        public override async Task<Empty> DepositAsync(DepositRequest request, ServerCallContext context)
        {
            InitializeServices();

            await _accountOperationService.DepositAsync(request.ToDepositRequestModel());

            return new Empty();
        }

        public override async Task<GetHistoryResponse> GetHistoryAsync(GetHistoryRequest request, ServerCallContext context)
        {
            InitializeServices();

            //ResultWithModel<string>  result = await _accountHistoryService.GetHistoryAsync(request.ToGetHistoryRequestModel());

            return new GetHistoryResponse { BankHistory = string.Empty };// result.ReturnType };
        }

        public override async Task<CloseAccountResponse> CloseAccountAsync(CloseAccountRequest request, ServerCallContext context)
        {
            InitializeServices();

            Result result = await _accountService.CloseAccountAsync(request.ToCloseAccountRequestModel());

            return new CloseAccountResponse { FinishedWitSuccess = result.OperationFinisedWithSuccess };
        }
    }
}