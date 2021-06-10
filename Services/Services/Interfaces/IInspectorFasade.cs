using System;
using System.Threading.Tasks;
using Models.Enums;

namespace Services.Services.Interfaces
{
    public interface IInspectorFasade
    {
        Task<TReturnModel> DoActionAsync<TReturnModel>(Func<Task<TReturnModel>> onWorkAsync, CustomerActivityType customerActivityType, object requestData);
    }
}