using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Repository.Extensions
{
    public static class AccountHistoryTypeModelCreatingExtension
    {
        public static void InitializeAccountHistoryTypeModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHistoryType>().ToTable("AccountHistoryTypes");
            modelBuilder.Entity<AccountHistoryType>(entity =>
            {
                entity.HasKey(e => e.AccountHistoryTypeId);
                entity.Property(e => e.AccountHistoryTypeId).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.AddInitialData();
        }

        private static void AddInitialData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHistoryType>().HasData(new AccountHistoryType[]
            {
                new AccountHistoryType { AccountHistoryTypeId = 1, Name = "Deposit" },
                new AccountHistoryType { AccountHistoryTypeId = 2, Name = "Withdraw" }
            });
        }
    }
}