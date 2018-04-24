using SalesAppExample.Models;
using SalesAppExample.Entities;
using System.Linq;

namespace SalesAppExample.Services.DiscountCalculator
{
    public class ValuedCustomerDiscountCalculator : IDiscountCalculator
    {

        private readonly SalesAppExampleDbContext _db;
        public ValuedCustomerDiscountCalculator(SalesAppExampleDbContext db)
        {
            _db = db;
        }

        public double Calculate (DiscountCalculationModel model)
        {
            // Discount applied if the overall value of previous purchases exceeds
            var customerPurchases = _db.Purchases.Where(p => p.CustomerId == model.CustomerId).ToList();
            double customerProvidedValue = 0;

            foreach (var purchase in customerPurchases)
            {
                customerProvidedValue += purchase.Units * purchase.Product.Price;
            }

            return (customerProvidedValue > 800 ? model.Sum * 0.9 : model.Sum);
        }
    }
}
