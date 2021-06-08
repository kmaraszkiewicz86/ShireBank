using System.Threading.Tasks;
using Models.Models;

namespace Repository.Services.Interfaces
{
    public interface IAccountHistoryService
    {
        Task<ResultWithModel<string>> GetHistoryAsync(GetHistoryRequestModel getHistoryRequestModel);
    }
}