using System.Collections.Generic;
using Models.Entities;

namespace Services.Services.Interfaces
{
    public interface IAccountHistoryFormaterService
    {
        string FormatHistory(List<AccountHistory> accountHistories);
    }
}