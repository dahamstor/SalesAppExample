using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAppExample.Models
{
    public class DiscountCalculationModel
    {
        public int Units { get; set; }
        public Guid CustomerId { get; set; }
        public double Sum { get; set; }
    }
}
