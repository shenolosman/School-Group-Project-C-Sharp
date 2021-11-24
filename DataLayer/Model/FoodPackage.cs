using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    public class FoodPackage
    {
        public FoodPackage()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public int FoodOrderId { get; set; }
        [Required]
        public string FoodType { get; set; }
        [Required]
        public string FoodBox { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        
        public virtual Restaurant Restaurant { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}