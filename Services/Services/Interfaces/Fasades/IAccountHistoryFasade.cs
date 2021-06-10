using System.Threading.Tasks;
using Models.Models;

namespace Services.Services.Interfaces.Fasades
{
    public interface IAccountHistoryFasade
    {
        Task<string> GetAccountHistoryAsync(GetHistoryRequestModel getHistoryRequestModel);
    }
}