using Models.Models;
using SharedInterface.Interfaces.CustomInterface;

namespace SharedInterface.Extensions
{
    public static class OpenAccountRequestExtension
    {
        public static OpenAccountRequestModel ToOpenAccountRequestModel(this OpenAccountRequest openAccountRequest)
        {
            return new OpenAccountRequestModel(openAccountRequest.FirstName, openAccountRequest.LastName, openAccountRequest.DebtLimit);
        }
    }
}