using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Backend;

namespace FoodRescue_Projekt
{
    public class AdminFrontend
    {
        /*
         * resetta databasen
         * titta på alla användare
         * titta på alla resturanger
         * knappa in info för, och lägga till en ny restaurang
         */

        public void ResetDatabase()
        {
            AdminBackend.PrepDatabase();
            Console.WriteLine("Database initialized!");
            Console.ReadLine();
            Console.Clear();
        }
        public void AllUser()
        {
            var users = AdminBackend.AllUsers();
            foreach (var user in users)
                Console.WriteLine(user);
        }
        public void AllRestaurant()
        {
            var restaurants = AdminBackend.ShowRestaurants();
            foreach (var restaurant in restaurants)
                Console.WriteLine(restaurant);
        }

        public void AddRestaurant()
        {
            Console.WriteLine("Here we create new restaurant!");
            Console.Write("Please enter restaurant name:");
            var restaurantName = Console.ReadLine();
            Console.Write("Please enter phone nummer:");
            var phoneNr = Console.ReadLine();
            Console.Write("Please enter city:");
            var city = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(restaurantName)&&!string.IsNullOrWhiteSpace(restaurantName)&&!string.IsNullOrWhiteSpace(restaurantName))
            {
                var add = AdminBackend.AddSpecificRestaurant(restaurantName, phoneNr, city);
                if (add!=null)
                {
                    Console.WriteLine("You have created new restaurant");
                }
                else
                {
                    Console.WriteLine("Ooops! Something went wrong! Please try again!");
                }
            }
            
        }

    }
}
