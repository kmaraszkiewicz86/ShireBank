using System.Threading.Tasks;
using Grpc.Core;
using SharedInterface.Interfaces.CustomInterface;
using static SharedInterface.Interfaces.CustomInterface.CustomerInterface;

namespace ShireBank
{
    public class CustomerServiceHost : CustomerInterfaceBase
    {
        public override async Task<OpenAccountResponse> OpenAccountAsync(OpenAccountRequest request, ServerCallContext context)
        {
            return new OpenAccountResponse { AccountId = 1 };
        }

        public override async Task<WithdrawResponse> WithdrawAsync(WithdrawRequest request, ServerCallContext context)
        {
            return new WithdrawResponse { Amount = 10 };
        }

        public override async Task<Empty> DepositAsync(DepositRequest request, ServerCallContext context)
        {
            return new Empty();
        }

        public override async Task<GetHistoryResponse> GetHistoryAsync(GetHistoryRequest request, ServerCallContext context)
        {
            return new GetHistoryResponse { BankHistory = string.Empty };
        }

        public override async Task<CloseAccountResponse> CloseAccountAsync(CloseAccountRequest request, ServerCallContext context)
        {
            return new CloseAccountResponse { FinishedWitSuccess = true };
        }
    }
}