using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace DataLayer.Backend
{
    public class AdminBackend
    {
        public static void PrepDatabase()
        {
            using var ctx = new FoodRescue();

            ctx.Database.EnsureDeleted();

            ctx.Database.EnsureCreated();

            ctx.Seed();
        }

        //Visar alla användare som finns
        public static List<User> AllUsers()
        {
            using var ctx = new FoodRescue();

            var query = ctx
                .Users
                .ToList();

            var list = query;
            return list;
        }

        //Raderar/bannar användare, både med och utan order
        public static User DeleteUsers(string username)
        {
            using var ctx = new FoodRescue();

            var user = ctx.Users.Find(username);
            var orders = ctx.Orders.Where(a => a.User.Username == username).ToList();
            orders.ForEach(a => a.User = null);
            ctx.Users.Remove(user);
            ctx.SaveChanges();

            return user;
        }

        //Lägger till en ny användare
        public static User AddUser(string username, string password, string email)
        {
            using var ctx = new FoodRescue();

            var user = new User()
                {Username = username, Password = password, EmailAddress = email};
            ctx.Users.Add(user);

            ctx.SaveChanges();

            return user;
        }

        //Visar alla restauranger som finns
        public static List<Restaurant> ShowRestaurants()
        {
            using var ctx = new FoodRescue();

            var query = ctx.Restaurants
                .ToList();

            var list = query;
            return list;
        }

        //Här läggs en ny restaurang till
        public static Restaurant AddSpecificRestaurant(string restaurantName,string phoneNr, string city)
        {
            using var ctx = new FoodRescue();

            var restaurants = new Restaurant
                { RestaurantName = restaurantName, PhoneNumber = phoneNr, City = city};
            ctx.Restaurants.Add(restaurants);

            ctx.SaveChanges();

            return restaurants;
        }

        //Raderar en restaurang och deras matlåda
        public static Restaurant DeleteRestaurants(int foodId, string restaurant)
        {
            using var ctx = new FoodRescue();

            var foodPackage = ctx.FoodPackages.Find(foodId);
            var restaurants = ctx.Restaurants.Find(restaurant);
             ctx.FoodPackages.Remove(foodPackage); 
             ctx.Restaurants.Remove(restaurants); 
            
             ctx.SaveChanges();

             return restaurants;
        }

        //Ändrar email
        public static User ChangeEmail(string username, string newEmail)
        {
            using var ctx = new FoodRescue();

            var user = ctx.Users.Find(username);

            user.EmailAddress = newEmail;
            ctx.SaveChanges();

            return user;
        }

        //Ändrar lösenord
        public static User ChangePassword(string username, string newPassword)
             {
                 using var ctx = new FoodRescue();

                 var user = ctx.Users.Find(username);

                 user.Password = newPassword;
                 ctx.SaveChanges();

                 return user;
            }
    }
}