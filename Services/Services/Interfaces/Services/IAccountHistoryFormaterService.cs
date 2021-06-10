using System.Collections.Generic;
using Models.Entities;

namespace Services.Services.Interfaces.Services
{
    public interface IAccountHistoryFormaterService
    {
        string FormatHistory(List<AccountHistory> accountHistories);
    }
}