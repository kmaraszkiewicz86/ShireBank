using Models.Models;
using SharedInterface.Interfaces.CustomInterface;

namespace SharedInterface.Extensions
{
    public static class DepositRequestExtension
    {
        public static DepositRequestModel ToDepositRequestModel(this DepositRequest depositRequest)
        {
            return new DepositRequestModel(depositRequest.Account, depositRequest.Amount);
        }
    }
}
