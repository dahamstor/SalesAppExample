using System.Data.Entity;

namespace SalesAppExample.Entities
{
    public class SalesAppExampleDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DiscountType> DiscountTypes { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
