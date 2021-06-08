using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Repository.Extensions
{
    public static class CustomerModelCreatingExtension
    {
        public static void InitializeCustomerModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers", "dbo");
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.HasIndex(e => new { e.Name, e.Surname }).IsUnique();
            });
        }
    }
}