using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DataLayer.Backend
{
    public class RestaurantBackend
    {

        //Visar alla köpta matlådor för en restaurang, tex "Espresso House"
        public static List<FoodPackage> AllSoldFoodBoxes(string restaurant)
        {
            using var ctx = new FoodRescue();

            var query = ctx.FoodPackages.Include(c => c.Orders)
                .Select(c => new
                {
                    PurchaseMade = c.Orders.Count > 0,
                    Restaurant = c.Restaurant.RestaurantName,
                    FoodPackage = c.FoodBox,
                    price = c.Price,
                    FoodID = c.FoodOrderId
                })
                .Where(c => c.Restaurant == restaurant && c.PurchaseMade == true)
                .ToList();

            List<FoodPackage> newList = new List<FoodPackage>();

            foreach (var item in ctx.FoodPackages)
            {
                foreach (var q in query)
                {
                    if (q.FoodPackage == item.FoodBox)
                    {
                        newList.Add(item);
                    }
                }
            }
            return newList;
        }
    

        //Lägger till en ny matlåda till en ny restaurang
        public static FoodPackage AddNewFoodBoxesToANewRestaurant(string foodType, string foodBox, int price, DateTime expiryDate, string restaurantName, string phoneNumber, string city)
        {
            using var ctx = new FoodRescue();

            var foodPackage = new FoodPackage
            {
                FoodType = foodType,
                FoodBox = foodBox,
                Price = price,
                ExpiryDate = expiryDate,
                Restaurant = new Restaurant() { RestaurantName = restaurantName, PhoneNumber = phoneNumber , City = city }
            };

            ctx.FoodPackages.Add(foodPackage);
            ctx.SaveChanges();

            return foodPackage;
        }

        //Ny matlåda som läggs till hos en befintlig restaurang 
        public static Restaurant AddNewFoodBoxToAnExistingRestaurant(string restaurant, string foodType, string foodBox, int price, DateTime expiryDate)
        {
            using var ctx = new FoodRescue();

            var restaurants = ctx.Restaurants.Find(restaurant);

            ctx.FoodPackages.Add(new FoodPackage()
            {
                FoodType = foodType,
                FoodBox = foodBox,
                Price = price,
                ExpiryDate = expiryDate,
                Restaurant = restaurants
            });

            ctx.SaveChanges();

            return restaurants;
        }

        //Visar restaurangens förtjänst
        public static int TotalProfit(string restaurant)
        {
            using var ctx = new FoodRescue();

            ctx.Restaurants.Find(restaurant);

            var totalProfit= ctx.FoodPackages.SelectMany(c => c.Orders).Where(c => c.FoodPackage.Restaurant.RestaurantName == restaurant).Sum(c => c.FoodPackage.Price);

            return totalProfit;
        }

        //Raderar matlåda
        public static FoodPackage DeleteOldFoodBox(int foodId)
        {
            using var ctx = new FoodRescue();

            var foodBox = ctx.FoodPackages.Find(foodId);

            ctx.FoodPackages.Remove(foodBox);
            ctx.SaveChanges();

            return foodBox;
        }

        //Ändrar pris på matlåda
        public static FoodPackage ChangePrice(int foodId, int foodPrice)
        {
            using var ctx = new FoodRescue();

            var price = ctx.FoodPackages.Find(foodId);

            price.Price = foodPrice;
            ctx.SaveChanges();

            return price;
        }
    }
}