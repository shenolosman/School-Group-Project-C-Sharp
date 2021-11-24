using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataLayer
{
    public class Order
    {
        [Key]
        public int PurchaseNumber { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]

        public virtual User User { get; set; } 
        public int FoodPackageId { get; set; }
        public virtual FoodPackage FoodPackage { get; set; }
    }
}