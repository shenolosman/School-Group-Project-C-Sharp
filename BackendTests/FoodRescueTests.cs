using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DataLayer.Backend;
using Xunit;

namespace BackendTests
{
    public class FoodRescueTests
    {
        //Testing if user exits
        [Fact]
        public void UserTest()
        {
            AdminBackend.PrepDatabase();
            var users = AdminBackend.AllUsers();
            Assert.True(users.Where(x => x.Equals("KevinJ")) != null);
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
    }
}