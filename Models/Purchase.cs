using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set;  } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }


    }
}