using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repository.Extensions;

namespace Repository.Core
{
    public class ShireBankDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountHistory> AccountHistories { get; set; }
        public DbSet<AccountHistoryType> AccountHistoryTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public ShireBankDbContext(DbContextOptions<ShireBankDbContext> options)
            : base(options)
        { }

        public void CreateDatabaseIfNotExists()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.InitializeAccountModel();
            modelBuilder.InitializeAccountHistoryModel();
            modelBuilder.InitializeAccountHistoryTypeModel();
            modelBuilder.InitializeCustomerModel();

            base.OnModelCreating(modelBuilder);
        }
    }
}