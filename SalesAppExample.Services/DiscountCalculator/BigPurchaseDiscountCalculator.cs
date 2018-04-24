using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesAppExample.Models;

namespace SalesAppExample.Services.DiscountCalculator
{
    public class BigPurchaseDiscountCalculator : IDiscountCalculator
    {
        public double Calculate (DiscountCalculationModel model)
        {
            // Discount applied only when the unit count exceeds 1000
            return (model.Units > 1000 ? model.Sum * 0.8 : model.Sum);
        }
    }
}
