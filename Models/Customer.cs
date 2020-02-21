using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Customer
    {

        [Key]
        public int CustomerId { get; set; }

        [Required (ErrorMessage = "The First Name field can not be left empty!")]
        [Display (Name = "First Name")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "The Last Name field can not be left empty!")]
        [Display (Name = "Last Name")]
        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<Purchase> Purchases { get; set; }
    }
}