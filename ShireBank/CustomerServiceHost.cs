using System.Threading.Tasks;
using Grpc.Core;
using Models.Models;
using Repository.Services.Interfaces;
using Services.Services.Interfaces;
using SharedInterface.Extensions;
using SharedInterface.Interfaces.CustomInterface;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;

namespace ShireBank
{
    public class CustomerServiceHost : CustomerInterfaceBase
    {
        private IAccountService _accountService;

        private IAccountOperationService _accountOperationService;

        private IAccountHistoryFasade _accountHistoryFasade;

        public CustomerServiceHost(IAccountService accountService, IAccountOperationService accountOperationService, IAccountHistoryFasade accountHistoryFasade)
        {
            _accountService = accountService;
            _accountOperationService = accountOperationService;
            _accountHistoryFasade = accountHistoryFasade;
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
            return new GetHistoryResponse { BankHistory = await _accountHistoryFasade.GetAccountHistoryAsync(request.ToGetHistoryRequestModel()) };
        }

        public override async Task<CloseAccountResponse> CloseAccountAsync(CloseAccountRequest request, ServerCallContext context)
        {
            Result result = await _accountService.CloseAccountAsync(request.ToCloseAccountRequestModel());

            return new CloseAccountResponse { FinishedWitSuccess = result.OperationFinisedWithSuccess };
        }
    }
}