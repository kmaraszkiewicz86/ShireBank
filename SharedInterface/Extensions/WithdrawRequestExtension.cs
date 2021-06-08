using Models.Models;
using SharedInterface.Interfaces.CustomInterface;

namespace SharedInterface.Extensions
{
    public static class WithdrawRequestExtension
    {
        public static WithdrawRequestModel ToWithdrawRequestExtension(this WithdrawRequest withdrawRequest)
        {
            return new WithdrawRequestModel(withdrawRequest.Account, withdrawRequest.Amount);
        }
    }
}
