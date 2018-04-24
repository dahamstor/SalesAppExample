using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAppExample.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public virtual List<Purchase> Purchases { get; set; }
    }
}