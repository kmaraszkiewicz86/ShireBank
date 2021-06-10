using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Models.Enums;
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
        private readonly IServiceProvider _serviceProvider;

        private IAccountService _accountService => _serviceProvider.GetService<IAccountService>();

        private IAccountOperationService _accountOperationService => _serviceProvider.GetService<IAccountOperationService>();

        private IAccountHistoryFasade _accountHistoryFasade => _serviceProvider.GetService<IAccountHistoryFasade>();

        private IInspectorFasade _inspectorFasade;

        public CustomerServiceHost(IServiceProvider serviceProvider, 
            IInspectorFasade inspectorFasade)
        {
            _serviceProvider = serviceProvider;
            _inspectorFasade = inspectorFasade;
        }

        public override async Task<OpenAccountResponse> OpenAccountAsync(OpenAccountRequest request, ServerCallContext context)
        {
            var model = request.ToOpenAccountRequestModel();

            return await _inspectorFasade.DoActionAsync(async () =>
            {
                ResultWithModel<uint> result = await _accountService.OpenAccountAsync(model);

                return new OpenAccountResponse { FinishedWitSuccess = result.OperationFinisedWithSuccess, AccountId = result.ReturnType };
            }, CustomerActivityType.OpenAccount, model);
        }

        public override async Task<WithdrawResponse> WithdrawAsync(WithdrawRequest request, ServerCallContext context)
        {
            var model = request.ToWithdrawRequestExtension();

            return await _inspectorFasade.DoActionAsync(async () =>
            {
                ResultWithModel<float> result = await _accountOperationService.WithdrawAsync(model);

                return new WithdrawResponse { Amount = result.ReturnType };
            }, CustomerActivityType.Withdraw, model);
        }

        public override async Task<Empty> DepositAsync(DepositRequest request, ServerCallContext context)
        {
            var model = request.ToDepositRequestModel();

            return await _inspectorFasade.DoActionAsync(async () =>
            {
                await _accountOperationService.DepositAsync(model);

                return new Empty();
            }, CustomerActivityType.Deposit, model);
        }

        public override async Task<GetHistoryResponse> GetHistoryAsync(GetHistoryRequest request, ServerCallContext context)
        {
            var model = request.ToGetHistoryRequestModel();

            return await _inspectorFasade.DoActionAsync(async () =>
            {
                return new GetHistoryResponse { BankHistory = await _accountHistoryFasade.GetAccountHistoryAsync(model) };
            }, CustomerActivityType.GetHistory, model);
        }

        public override async Task<CloseAccountResponse> CloseAccountAsync(CloseAccountRequest request, ServerCallContext context)
        {
            var model = request.ToCloseAccountRequestModel();

            return await _inspectorFasade.DoActionAsync(async () =>
            {
                Result result = await _accountService.CloseAccountAsync(model);

                return new CloseAccountResponse { FinishedWitSuccess = result.OperationFinisedWithSuccess };
            }, CustomerActivityType.CloseAccount, model);
        }
    }
}