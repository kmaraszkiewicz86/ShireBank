using System.Threading.Tasks;
using Models.Models;

namespace Services.Services.Interfaces
{
    public interface IAccountHistoryFasade
    {
        Task<string> GetAccountHistoryAsync(GetHistoryRequestModel getHistoryRequestModel);
    }
}