using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Repository.Extensions
{
    public static class CustomerModelCreatingExtension
    {
        public static void InitializeCustomerModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();

                entity.HasIndex(e => new { e.FirstName, e.LastName }).IsUnique();
            });
        }
    }
}