using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SalesAppExample.Services.DiscountCalculator;

namespace SalesAppExample.Factories
{
    public interface IDiscountCalculatorFactory
    {
        IDiscountCalculator Get(string discountName);
    }
}