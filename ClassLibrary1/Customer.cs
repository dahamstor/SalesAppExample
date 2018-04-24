using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAppExample.Entities
{
    public class Customer : EntityBase
    {
        public string Name { get; set; }

        public virtual List<Purchase> Purchases { get; set; }
    }
}