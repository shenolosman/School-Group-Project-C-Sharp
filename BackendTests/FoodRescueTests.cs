using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DataLayer.Backend;
using Xunit;

namespace BackendTests
{
    public class FoodRescueTests
    {
        //Testing to list all users
        [Fact]
        public void AllUsers()
        {
            AdminBackend.PrepDatabase();
            var user = AdminBackend.AllUsers();
            Assert.NotEmpty(user);
        }
        //Testing if user exits
        [Fact]
        public void UserTest()
        {
            AdminBackend.PrepDatabase();
            var users = AdminBackend.AllUsers();
            Assert.True(users.Where(x => x.Equals("KevinJ")) != null);
        }
        //Testing to list all restaurants
        [Fact]
        public void AllRestaurants()
        {
            AdminBackend.PrepDatabase();
            var restaurant = AdminBackend.ShowRestaurants();
            Assert.NotEmpty(restaurant);
        }
        //Testing to add new restaurant
        [Fact]
        public void AddRestaurantTest()
        {
            AdminBackend.PrepDatabase();
            var restaurant = AdminBackend.AddSpecificRestaurant(restaurantName: "Testsson", city: "TestAndCity", phoneNr: "0730001122");
            Assert.NotNull(restaurant);
        }
      
        //Visar att matl�dorna �r k�pta fr�n Espresso House
        [Fact]
        public void RestaurantTest()
        {
            AdminBackend.PrepDatabase();

            var food = RestaurantBackend.AllSoldFoodBoxes("Espresso House");

            Assert.True(food.Find(box => box.FoodBox == "Sm�rg�s") != null);
            Assert.True(food.Find(box => box.FoodBox == "Wrap med gr�nsaker") != null);

            Assert.False(food.Find(box => box.FoodBox == "Sm�rg�s med kalkon") != null);
            Assert.False(food.Find(box => box.FoodBox == "Falafel") != null);
        }

        // Testet visar att Hannas k�p g�tt igenom
        [Fact]
        public void UserPurchaseHistoryTest()
        {
            AdminBackend.PrepDatabase();

            var purchaseHistory = UserBackend.UserPurchaseHistory("Hanna");

            Assert.True(purchaseHistory.Find(box => box.FoodBox == "Oxfilepasta") != null);
            Assert.False(purchaseHistory.Find(box => box.FoodBox == "Wrap med gr�nsaker") != null);
        }

        //Testar att l�gga till ny matl�da till en restaurang
        [Fact]
        public void AddFoodBoxTest()
        {
            AdminBackend.PrepDatabase();
            var foodBox = RestaurantBackend.AddNewFoodBoxToAnExistingRestaurant("Gateau", "Vego", "Sallad", 55, DateTime.Now);
            Assert.NotNull(foodBox);
        }
    }
}