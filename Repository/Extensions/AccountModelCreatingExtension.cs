using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Repository.Extensions
{
    public static class AccountModelCreatingExtension
    {
        public static void InitializeAccountModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId);
                entity.Property(e => e.AccountId).ValueGeneratedOnAdd();
            });
        }
    }
}