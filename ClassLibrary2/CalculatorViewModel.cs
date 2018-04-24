using SalesAppExample.Entities;
using System;
using System.Collections.Generic;

namespace SalesAppExample.ViewModels
{
    public class CalculatorViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<DiscountType> DiscountTypes { get; set; }
        public Guid PickedCustomerId { get; set; }
        public Guid PickedDiscountTypeId { get; set; }
    }
}
