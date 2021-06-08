using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Repository.Extensions
{
    public static class AccountHistoryTypeModelCreatingExtension
    {
        public static void InitializeAccountHistoryTypeModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHistoryType>().ToTable("AccountHistoryTypes", "dbo");
            modelBuilder.Entity<AccountHistoryType>(entity =>
            {
                entity.HasKey(e => e.AccountHistoryTypeId);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.AddInitialData();
        }

        private static void AddInitialData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHistoryType>().HasData(new AccountHistoryType[]
            {
                new AccountHistoryType { Name = "OpenAccount" },
                new AccountHistoryType { Name = "Deposit" },
                new AccountHistoryType { Name = "Withdraw" }
            });
        }
    }
}