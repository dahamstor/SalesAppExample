using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesAppExample.ViewModels
{
    public class CalculatorViewModel
    {
        public List<CustomerViewModel> Customers { get; set; }

        public List<DiscountTypeViewModel> DiscountTypes { get; set; }

        [Required (ErrorMessage = "Customer is required")]
        public Guid? PickedCustomerId { get; set; }

        [Required (ErrorMessage = "Discount Type is required")]
        public Guid? PickedDiscountTypeId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Unit count must be a positive value")]
        public int Units { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Sum must be a positive value")]
        public double Sum { get; set; }

        public double DiscountedSum { get; set; }
    }
}
