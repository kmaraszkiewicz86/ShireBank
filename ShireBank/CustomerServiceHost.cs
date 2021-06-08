using System.Threading.Tasks;
using Grpc.Core;
using Models.Models;
using Repository.Services.Interfaces;
using SharedInterface.Extensions;
using SharedInterface.Interfaces.CustomInterface;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;

namespace ShireBank
{
    public class CustomerServiceHost : CustomerInterfaceBase
    {
        private readonly IAccountService _accountService;

        private readonly IAccountOperationService _accountOperationService;

        private readonly IAccountHistoryService _accountHistoryService;

        public CustomerServiceHost(IAccountService accountService,
            IAccountOperationService accountOperationService, 
            IAccountHistoryService accountHistoryService)
        {
            _accountService = accountService;
            _accountOperationService = accountOperationService;
            _accountHistoryService = accountHistoryService;
        }

        public override async Task<OpenAccountResponse> OpenAccountAsync(OpenAccountRequest request, ServerCallContext context)
        {
            ResultWithModel<uint> result = await _accountService.OpenAccountAsync(request.ToOpenAccountRequestModel());

            return new OpenAccountResponse { FinishedWitSuccess = result.OperationFinisedWithSuccess, AccountId = result.ReturnType };
        }

        public override async Task<WithdrawResponse> WithdrawAsync(WithdrawRequest request, ServerCallContext context)
        {
            ResultWithModel<float> result = await _accountOperationService.WithdrawAsync(request.ToWithdrawRequestExtension());

            return new WithdrawResponse { Amount = result.ReturnType };
        }

        public override async Task<Empty> DepositAsync(DepositRequest request, ServerCallContext context)
        {
            await _accountOperationService.DepositAsync(request.ToDepositRequestModel());

            return new Empty();
        }

        public override async Task<GetHistoryResponse> GetHistoryAsync(GetHistoryRequest request, ServerCallContext context)
        {
            ResultWithModel<string>  result = await _accountHistoryService.GetHistoryAsync(request.ToGetHistoryRequestModel());

            return new GetHistoryResponse { BankHistory = result.ReturnType };
        }

        public override async Task<CloseAccountResponse> CloseAccountAsync(CloseAccountRequest request, ServerCallContext context)
        {
            Result result = await _accountService.CloseAccountAsync(request.ToCloseAccountRequestModel());

            return new CloseAccountResponse { FinishedWitSuccess = result.OperationFinisedWithSuccess };
        }
    }
}