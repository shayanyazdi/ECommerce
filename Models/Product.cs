using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required (ErrorMessage = "The Title field can not be left empty!")]
        [Display (Name = "Title")]
        public string Title { get; set; }

        [Display (Name = "Image (url):")]
        public string Image { get; set; }

        [Display (Name = "Description")]
        public string Description { get; set; }

        [Display (Name = "Amount")]
        [Range(1, 100)]
        public int Amount { get; set; }

        [Display (Name = "Price")]
        public decimal Price { get; set; }

        public List<Purchase> Purchases { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}