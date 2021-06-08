using Models.Models;
using SharedInterface.Interfaces.CustomInterface;

namespace SharedInterface.Extensions
{
    public static class GetHistoryRequestExtension
    {
        public static GetHistoryRequestModel ToGetHistoryRequestModel(this GetHistoryRequest getHistoryRequest)
        {
            return new GetHistoryRequestModel(getHistoryRequest.Account);
        }
    }
}
