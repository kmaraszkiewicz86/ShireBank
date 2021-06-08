using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Repository.Extensions
{
    public static class AccountHistoryModelCreatingExtension
    {
        public static void InitializeAccountHistoryModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHistory>().ToTable("AccountHistories", "dbo");
            modelBuilder.Entity<AccountHistory>(entity =>
            {
                entity.HasKey(e => e.AccountHistoryId);
                entity.Property(e => e.AccountHistoryId).ValueGeneratedOnAdd();

                entity.Property(e => e.HistoryDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}