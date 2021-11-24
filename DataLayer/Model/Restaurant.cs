using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DataLayer
{
    public class Restaurant
    {
        public Restaurant()
        {
            FoodPackages = new HashSet<FoodPackage>();
        }
        [Key] 
        public string RestaurantName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]

        public ICollection<FoodPackage> FoodPackages { get; set; }
    }
}