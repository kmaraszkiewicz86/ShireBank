using System.Threading.Tasks;
using Grpc.Core;
using Models.Enums;
using Models.Models;
using Services.Services.Interfaces.Factories;
using Services.Services.Interfaces.Fasades;
using SharedInterface.Extensions;
using SharedInterface.Interfaces.CustomInterface;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;

namespace ShireBank
{
    public sealed class CustomerInterfaceService : CustomerInterfaceBase
    {
        private readonly IDbServicesFactory _dbServicesFactory;

        private readonly IInspectorFasade _inspectorFasade;

        public CustomerInterfaceService(IDbServicesFactory dbServicesFactory, 
            IInspectorFasade inspectorFasade)
        {
            _dbServicesFactory = dbServicesFactory;
            _inspectorFasade = inspectorFasade;
        }

        public override async Task<OpenAccountResponse> OpenAccountAsync(OpenAccountRequest request, ServerCallContext context)
        {
            var model = request.ToOpenAccountRequestModel();

            return await _inspectorFasade.DoActionAsync(async () =>
            {
                ResultWithModel<uint> result = await _dbServicesFactory.AccountService.OpenAccountAsync(model);

                return new OpenAccountResponse { FinishedWitSuccess = result.OperationFinisedWithSuccess, AccountId = result.ReturnType };
            }, CustomerActivityType.OpenAccount, model);
        }

        public override async Task<WithdrawResponse> WithdrawAsync(WithdrawRequest request, ServerCallContext context)
        {
            var model = request.ToWithdrawRequestExtension();

            return await _inspectorFasade.DoActionAsync(async () =>
            {
                ResultWithModel<float> result = await _dbServicesFactory.AccountOperationService.WithdrawAsync(model);

                return new WithdrawResponse { Amount = result.ReturnType };
            }, CustomerActivityType.Withdraw, model);
        }

        public override async Task<Empty> DepositAsync(DepositRequest request, ServerCallContext context)
        {
            var model = request.ToDepositRequestModel();

            return await _inspectorFasade.DoActionAsync(async () =>
            {
                await _dbServicesFactory.AccountOperationService.DepositAsync(model);

                return new Empty();
            }, CustomerActivityType.Deposit, model);
        }

        public override async Task<GetHistoryResponse> GetHistoryAsync(GetHistoryRequest request, ServerCallContext context)
        {
            var model = request.ToGetHistoryRequestModel();

            return await _inspectorFasade.DoActionAsync(async () =>
            {
                return new GetHistoryResponse { BankHistory = await _dbServicesFactory.AccountHistoryFasade.GetAccountHistoryAsync(model) };
            }, CustomerActivityType.GetHistory, model);
        }

        public override async Task<CloseAccountResponse> CloseAccountAsync(CloseAccountRequest request, ServerCallContext context)
        {
            var model = request.ToCloseAccountRequestModel();

            return await _inspectorFasade.DoActionAsync(async () =>
            {
                Result result = await _dbServicesFactory.AccountService.CloseAccountAsync(model);

                return new CloseAccountResponse { FinishedWitSuccess = result.OperationFinisedWithSuccess };
            }, CustomerActivityType.CloseAccount, model);
        }
    }
}