using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesAppExample.Entities;
using SalesAppExample.Models;

namespace SalesAppExample.Services.DiscountCalculator
{
    public class FirstPurchaseDiscountCalculator : IDiscountCalculator
    {
        private readonly SalesAppExampleDbContext _db;

        public FirstPurchaseDiscountCalculator(SalesAppExampleDbContext db)
        {
            _db = db;
        }

    
        public double Calculate (DiscountCalculationModel model)
        {
            // Discount applied only on first purchase by the customer
            var isFirstPurchase = !_db.Purchases.Any(p => p.CustomerId == model.CustomerId);
            return (isFirstPurchase ? model.Sum * 0.7 : model.Sum);
        }
    }
}
