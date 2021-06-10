using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Entities;
using Models.Models;
using Repository.Services.Interfaces;
using Services.Services.Interfaces.Fasades;
using Services.Services.Interfaces.Services;

namespace Services.Services.Implementations.Fasades
{
    public sealed class AccountHistoryFasade : IAccountHistoryFasade
    {
        private readonly IAccountHistoryFormaterService _accountHistoryFormaterService;

        private readonly IAccountHistoryService _accountHistoryService;

        public AccountHistoryFasade(IAccountHistoryFormaterService accountHistoryFormaterService,
            IAccountHistoryService accountHistoryService)
        {
            _accountHistoryFormaterService = accountHistoryFormaterService;
            _accountHistoryService = accountHistoryService;
        }

        public async Task<string> GetAccountHistoryAsync(GetHistoryRequestModel getHistoryRequestModel)
        {
            ResultWithModel<List<AccountHistory>> result = await _accountHistoryService.GetHistoryAsync(getHistoryRequestModel);

            if (!result.OperationFinisedWithSuccess)
                return string.Empty;

            return _accountHistoryFormaterService.FormatHistory(result.ReturnType);
        }
    }
}
