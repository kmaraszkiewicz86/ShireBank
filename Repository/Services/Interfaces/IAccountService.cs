using System.Threading.Tasks;
using Models.Models;

namespace Repository.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResultWithModel<uint>> OpenAccountAsync(OpenAccountRequestModel openAccountRequestModel);

        Task<Result> CloseAccountAsync(CloseAccountRequestModel closeAccountRequestModel);
    }
}