using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Entities;
using Services.Services.Interfaces.Services;

namespace Services.Services.Implementations.Services
{
    public sealed class AccountHistoryFormaterService : IAccountHistoryFormaterService
    {
        private static int[] columnsWidth = new int[]
        {
            3, 21, 16, 17, 33
        };

        public string FormatHistory(List<AccountHistory> accountHistories)
        {
            if (accountHistories == null || accountHistories.Count == 0)
                return string.Empty;

            var account = accountHistories.First().Account;

            StringBuilder readableText = new StringBuilder();
            readableText.AppendLine($"===========================================================================================================");
            readableText.AppendLine($"Account history for customer: {account.Customer.FirstName} {account.Customer.LastName}");
            readableText.AppendLine($"===========================================================================================================");

            readableText.AppendLine($"| {GenerateColumnText("No.", 0)} |" +
                $"| {GenerateColumnText("Operation Data", 1)} | " +
                $"{ GenerateColumnText("Operation Type", 2)} | " +
                $"{ GenerateColumnText("Amount of funds", 3)} | " +
                $"{ GenerateColumnText("Amount of founds after operation", 4)} |");

            readableText.AppendLine($"-----------------------------------------------------------------------------------------------------------");

            int operationNumber = 1;

            foreach (AccountHistory accountHistory in accountHistories)
            {
                readableText.AppendLine($"| { GenerateColumnText(operationNumber++, 0) } | " +
                    $"{ GenerateColumnText(accountHistory.HistoryDate, 1) } | " +
                    $"{ GenerateColumnText(accountHistory.AccountHistoryType.Name, 2) } | " +
                    $"{ GenerateColumnText(accountHistory.AmountOfFunds, 3) } | " +
                    $"{ GenerateColumnText(accountHistory.AmountOfFoundsAfterOperation, 4) } |");
            }

            return readableText.ToString();
        }

        private string GenerateColumnText(object value, int columnIndex)
        {
            var columnText = value.ToString();

            if (columnText.Length < columnsWidth[columnIndex])
            {
                var lengthTextToAdd = columnsWidth[columnIndex] - columnText.Length;
                var restOfText = string.Join("", Enumerable.Repeat(" ", lengthTextToAdd));

                columnText = $"{restOfText}{columnText}";
            }

            return columnText;
        }
    }
}