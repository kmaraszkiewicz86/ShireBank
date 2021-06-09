using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Entities;
using Services.Services.Interfaces;

namespace Services.Services.Implementations
{
    public class AccountHistoryFormaterService : IAccountHistoryFormaterService
    {
        public string FormatHistory(List<AccountHistory> accountHistories)
        {
            if (accountHistories == null)
                return string.Empty;

            var account = accountHistories.First().Account;

            StringBuilder readableText = new StringBuilder();
            readableText.AppendLine($"Account history for customer: {account.Customer.FirstName} {account.Customer.LastName}");
            readableText.AppendLine($"======================================================================================");

            foreach (AccountHistory accountHistory in accountHistories)
            {
                readableText.AppendLine($"| {accountHistory.AccountHistoryType.Name} | {accountHistory.AmountOfFunds} | {accountHistory.AmountOfFoundsAfterOperation} |");
            }

            return readableText.ToString();
        }
    }
}