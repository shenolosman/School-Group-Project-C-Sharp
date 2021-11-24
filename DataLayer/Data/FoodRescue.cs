using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace DataLayer.Data
{
    public class FoodRescue : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<FoodPackage> FoodPackages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(b => b.EmailAddress)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(b => b.Username)
                .IsUnique();

            modelBuilder.Entity<FoodPackage>()
                .HasIndex(b => b.FoodBox)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(b => b.FoodPackageId)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .LogTo(m => Debug.WriteLine(m), LogLevel.Information)
                    .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=FoodRescue"
                    );
            }
        }

        public void Seed()
        {
            using var ctx = new FoodRescue();

            var users = new User[]
            {
                new() {Username = "KevinJ", Password = "Pass123", EmailAddress = "john@gmail.com"},
                new() {Username = "Hanna", Password = "Hund", EmailAddress = "joh@gmail.com"},
                new() {Username = "Kajsa", Password = "Katt", EmailAddress = "Kajsa@hotmail.com"},
                new() {Username = "Emmy", Password = "Password", EmailAddress = "EmmyP@gmail.com"},
                new() {Username = "Klara", Password = "SweetCat", EmailAddress = "Klara@utb.ecutbildning.se"},
                new() {Username = "Sandra", Password = "Pass123", EmailAddress = "Sandras@gmail.com"},
            };

            ctx.Users.AddRange(users);
            ctx.SaveChanges();


            var restaurants = new Restaurant[]
            {
                new() {RestaurantName = "Espresso House", PhoneNumber = "078-822 66 33", City = "Göteborg"},
                new() {RestaurantName = "Esters", PhoneNumber = "079-822 33 63", City = "Kungsbacka"},
                new() {RestaurantName = "Wagamama", PhoneNumber = "078-899 66 33", City = "Stockholm"},
                new() {RestaurantName = "Gateau", PhoneNumber = "076-534 65 00", City = "Malmö"},
                new() {RestaurantName = "Max", PhoneNumber = "076-298 33 55", City = "Halmstad"},
            };
            ctx.Restaurants.AddRange(restaurants);
            ctx.SaveChanges();

            var foodPackages = new FoodPackage[]
            {
                new()
                {
                    FoodType = "Vego", FoodBox = "Smörgås", Price = 39, ExpiryDate = DateTime.Today + TimeSpan.FromDays(2), 
                    Restaurant = restaurants[0]

                },
                new()
                {
                    FoodType = "Kött", FoodBox = "Oxfilepasta", Price = 40, ExpiryDate = DateTime.Today,
                    Restaurant = restaurants[1]
                },
                new()
                {
                    FoodType = "Fisk", FoodBox = "Nudlar med räkor", Price = 90,
                    ExpiryDate = DateTime.Today + TimeSpan.FromDays(3), Restaurant = restaurants[2]
                },
                new()
                {
                    FoodType = "Kött", FoodBox = "Fralla med skinka", Price = 30, ExpiryDate = DateTime.Today,
                    Restaurant = restaurants[3]
                },
                new()
                {
                    FoodType = "Kött", FoodBox = "Smörgås med kalkon", Price = 39,
                    ExpiryDate = DateTime.Today + TimeSpan.FromDays(1), Restaurant = restaurants[0]
                },
                new()
                {
                    FoodType = "Vego", FoodBox = "Wrap med grönsaker", Price = 43, ExpiryDate = DateTime.Today,
                    Restaurant = restaurants[0]
                },
                new()
                {
                    FoodType = "Vego", FoodBox = "Falafel", Price = 25,
                    ExpiryDate = DateTime.Today + TimeSpan.FromDays(4), Restaurant = restaurants[2]
                },
                new()
                {
                    FoodType = "Fisk", FoodBox = "Fisk burgare med pommes", Price = 60, ExpiryDate = DateTime.Today,
                    Restaurant = restaurants[4]
                },
                new()
                {
                    FoodType = "Fisk", FoodBox = "Rökt makrill med potatis", Price = 65,
                    ExpiryDate = DateTime.Today - TimeSpan.FromDays(1), Restaurant = restaurants[2]
                },

            };
            ctx.FoodPackages.AddRange(foodPackages);
            ctx.SaveChanges();


            var orders = new Order[]
            {
                new() {OrderDate = DateTime.Now, User = users[0], FoodPackage = foodPackages[0]},
                new() {OrderDate = DateTime.Now - TimeSpan.FromHours(9), User = users[1], FoodPackage = foodPackages[1]},
                new() {OrderDate = DateTime.Now, User = users[1], FoodPackage = foodPackages[2]},
                new() {OrderDate = DateTime.Now, User = users[2], FoodPackage = foodPackages[3]},
                new() {OrderDate = DateTime.Now, User = users[3], FoodPackage = foodPackages[5]},
            };
            ctx.Orders.AddRange(orders);
            ctx.SaveChanges();
            ctx.SaveChanges();
        }
    }
}

           