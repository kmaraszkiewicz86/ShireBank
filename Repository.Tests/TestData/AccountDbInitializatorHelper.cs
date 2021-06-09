using System.Threading.Tasks;
using Models.Entities;
using Repository.Core;

namespace Repository.Tests.TestData
{
    public static class AccountDbInitializatorHelper
    {
        public static async Task InitializeAccountTestDataAsync(this ShireBankDbContext shireBankDbContext)
        {
            Customer[] customers = new[]
            {
                new Customer { CustomerId = 1, FirstName = "PersonName1", LastName = "PersonSurname1" },
                new Customer { CustomerId = 2, FirstName = "PersonName2", LastName = "PersonSurname2" }
            };

            foreach (Customer customer in customers)
            {
                await shireBankDbContext.Customers.AddAsync(customer);
            }

            await shireBankDbContext.Accounts.AddAsync(new Account { AccountId = 1, Amount = 1, DebitLimit = 100, Customer = customers[0] });
            await shireBankDbContext.Accounts.AddAsync(new Account { AccountId = 2, Amount = 0, DebitLimit = 300, Customer = customers[1] });

            await shireBankDbContext.SaveChangesAsync();
        }
    }
}