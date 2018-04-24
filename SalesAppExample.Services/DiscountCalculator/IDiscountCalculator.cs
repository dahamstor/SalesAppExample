using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesAppExample.Models;

namespace SalesAppExample.Services.DiscountCalculator
{
    public interface IDiscountCalculator
    {
        double Calculate(DiscountCalculationModel model);
    }
}
