using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAppExample.Entities
{
    public class Purchase : EntityBase
    {
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int Units { get; set; }
    }
}