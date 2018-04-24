using SalesAppExample.Entities;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Collections.Generic;

namespace ClavizHub.Entities.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SalesAppExampleDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SalesAppExampleDbContext db)
        {
            // Add customer, product and purchase records
            if (!db.Customers.Any())
            {
                var customers = new List<Customer>();
                customers.Add(new Customer { Name = "LLC Example Company" });
                customers.Add(new Customer { Name = "LLC First Timer" });
                db.Customers.AddRange(customers);

                var products = new List<Product>();
                products.Add(new Product { Name = "Cheerios", Price = 3.5 });
                products.Add(new Product { Name = "Pew Pew Waffles", Price = 2 });
                db.Products.AddRange(products);
                db.SaveChanges();

                var purchases = new List<Purchase>();
                purchases.Add(new Purchase { Units = 150, CustomerId = customers[0].Id, ProductId = products[0].Id });
                purchases.Add(new Purchase { Units = 200, CustomerId = customers[0].Id, ProductId = products[1].Id });
                db.Purchases.AddRange(purchases);
                db.SaveChanges();
            }

            // Add discount type records
            if (!db.DiscountTypes.Any())
            {
                var discountTypes = new List<DiscountType>();
                discountTypes.Add(new DiscountType { Name = "Big Purchase Discount" });
                discountTypes.Add(new DiscountType { Name = "First Purchase Discount" });
                discountTypes.Add(new DiscountType { Name = "Valued Customer Discount" });
                db.DiscountTypes.AddRange(discountTypes);

                db.SaveChanges();
            }
        }
    }
}
