using Models.Models;
using SharedInterface.Interfaces.CustomInterface;

namespace SharedInterface.Extensions
{
    public static class CloseAccountRequestExtension
    {
        public static CloseAccountRequestModel ToCloseAccountRequestModel(this CloseAccountRequest closeAccountRequest)
        {
            return new CloseAccountRequestModel(closeAccountRequest.Account);
        }
    }
}
