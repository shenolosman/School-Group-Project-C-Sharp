using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks.Dataflow;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DataLayer.Backend
{
    public class UserBackend
    {
        //Visar alla sålda matlådor
        public static List<FoodPackage> EverySoldFoodBox()
        {
            using var ctx = new FoodRescue();

            var querys = ctx.FoodPackages.Include(c => c.Orders)
                .Select(c => new
                {
                    Restaurant = c.Restaurant.RestaurantName,
                    FoodPackage = c.FoodBox,
                    price = c.Price,
                    foodId = c.FoodOrderId,
                    PurchaseMade = c.Orders.Count > 0,
                })
                .Where(c => c.PurchaseMade == true)
                .ToList();


            //foreach (var q in querys) list.Add();
            List<FoodPackage> newlist = new List<FoodPackage>();

            foreach (var item in ctx.FoodPackages)
            {
                foreach (var q in querys)
                {
                    if (q.FoodPackage == item.FoodBox)
                    {
                        newlist.Add(item);
                    }
                }
            }
            return newlist;    
        }

        //Visar alla osålda matlådor, sorterade på pris, lägst först
        public static List<FoodPackage> AllUnsoldFoodBoxes()
        {
            using var ctx = new FoodRescue();

            var query = ctx
                .FoodPackages
                .Where(u => u.Orders.Count == 0)
                .OrderBy(u => u.Price)
                .Select(u => new
                {
                    FoodpackageID = u.FoodOrderId,
                    FoodPackage = u.FoodBox,
                    price = u.Price,
                    Best_before = u.ExpiryDate,
                    Restaurant = u.Restaurant.RestaurantName,
                    Type = u.FoodType
                }).ToList();

            var toBuy = query.AsEnumerable();

            var mealToBuy = toBuy.GroupBy(u => u.Type);

            
            //foreach (var q in query) list.Add(q);
            List<FoodPackage> newlist = new List<FoodPackage>();

            
            
            foreach (var q in query)
            {
                foreach (var item in ctx.FoodPackages)
                {
                    if (q.FoodPackage == item.FoodBox)
                    {
                        newlist.Add(item);
                    }
                }
            }
            return newlist;
        }


        //Köper en angiven matlåda som är null(inte såld)
        public static Order BuyFoodBox(string username, int foodId)
        {
            using var ctx = new FoodRescue();

            var user = ctx.Users.Find(username);

            var food = ctx.FoodPackages.Find(foodId);

            var newOrder = new Order()
            {
                OrderDate = DateTime.Now, FoodPackage = food, User = user
            };
            ctx.Orders.Add(newOrder);
            ctx.SaveChanges();

            return newOrder;
        }

        //Visar all köphistorik för en användare
        public static List<FoodPackage> UserPurchaseHistory(string username)
        {
            using var ctx = new FoodRescue();

            var query = ctx.Orders
                .Select(c => new
                {
                    User = c.User.Username,
                    OrderNumber = c.PurchaseNumber,
                    orderdate = c.OrderDate,
                    Restaurant = c.FoodPackage.Restaurant.RestaurantName,
                    FoodPackage = c.FoodPackage.FoodBox,
                    price = c.FoodPackage.Price,
                })
                .Where(c => c.User == username && c.OrderNumber > 0)
                .ToList();

            List<FoodPackage> newlist = new List<FoodPackage>();

            foreach (var item in ctx.FoodPackages)
            {
                foreach (var q in query)
                {
                    if (q.FoodPackage == item.FoodBox)
                    {
                        newlist.Add(item);
                    }
                }
            }
            return newlist;
        }

        //Visar mat alternativen, Kött, fisk eller vego
        public static List<FoodPackage> ShowFoodType()  
        {
            using var ctx = new FoodRescue();

            var query = ctx.FoodPackages
                .Select(c => new
                {
                    Restaurant = c.Restaurant.RestaurantName,
                    Type = c.FoodType,
                    FoodPackage = c.FoodBox,
                    price = c.Price,
                })
                .OrderBy(c => c.Type)
                .ToList();

            List<FoodPackage> newlist = new List<FoodPackage>();

            foreach (var item in ctx.FoodPackages)
            {
                foreach (var q in query)
                {
                    if (q.FoodPackage == item.FoodBox)
                    {
                        newlist.Add(item);
                    }
                }
            }
            return newlist;
        }
    }
}
