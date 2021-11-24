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
        private ConsoleKeyInfo cki;
        public void Admin()
        {
            var UserList = AdminBackend.AllUsers().Where(x => x.IsAdmin == true);
            var UserNameList = new List<string>();

            foreach (var user in UserList)
            {
                UserNameList.Add(user.Username);
            }
            while (cki.Key != ConsoleKey.Escape)
            {
                bool loggedin = false;
                Console.Clear();
                Console.WriteLine("Press any key to login");
                Console.WriteLine("Press ESCAPE to exit");
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape) break;
                Console.Clear();
                Console.Write("Username: ");
                string username = Console.ReadLine();

                if (!UserNameList.Contains(username))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("User is not an admin...");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                }

                loggedin = UserNameList.Contains(username);

                while (loggedin)
                {
                    Console.Clear();
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("[1]: Reset Database\n[2]: Show All User\n[3]: Show All Restaurants\n[4]: Add New Restaurant");
                        cki = Console.ReadKey();
                        if (cki.Key == ConsoleKey.Backspace)
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Are you sure you want to log out?[Y/N]");
                                var choise = Console.ReadKey();
                                if (choise.Key == ConsoleKey.Y)
                                {
                                    loggedin = false;
                                    username = "";
                                    break;
                                }

                                if (choise.Key == ConsoleKey.N)
                                {
                                    break;
                                }
                            } while (true);
                        }

                        if (loggedin == false)
                        {
                            break;
                        }
                    } while (cki.Key is not (ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3 or ConsoleKey.D4 or ConsoleKey.Escape));

                    if (cki.Key == ConsoleKey.D1)
                    {
                        do
                        {
                            Console.Clear();
                            AdminBackend.PrepDatabase();
                            Console.WriteLine("Database initialized!");
                            Console.WriteLine("Press BACKSPACE to go back");
                            cki = Console.ReadKey();
                        } while (cki.Key != ConsoleKey.Backspace);
                    }

                    if (cki.Key == ConsoleKey.D2)
                    {
                        do
                        {
                            Console.Clear();
                            var users = AdminBackend.AllUsers();
                            foreach (var user in users)
                                Console.WriteLine($"Username: {user.Username}, email: {user.EmailAddress}");
                            Console.WriteLine("Press BACKSPACE to go back");
                            cki = Console.ReadKey();
                        } while (cki.Key != ConsoleKey.Backspace);
                    }
                    if (cki.Key == ConsoleKey.D3)
                    {
                        do
                        {
                            Console.Clear();
                            var restaurants = AdminBackend.ShowRestaurants();
                            foreach (var restaurant in restaurants)
                                Console.WriteLine(restaurant);
                            Console.WriteLine("Press BACKSPACE to go back");
                            cki = Console.ReadKey();
                        } while (cki.Key != ConsoleKey.Backspace);
                    }
                    if (cki.Key == ConsoleKey.D4)
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Here we create new restaurant!");
                            Console.Write("Please enter restaurant name:");
                            var restaurantName = Console.ReadLine();
                            Console.Write("Please enter phone nummer:");
                            var phoneNr = Console.ReadLine();
                            Console.Write("Please enter city:");
                            var city = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(restaurantName) && !string.IsNullOrWhiteSpace(restaurantName) && !string.IsNullOrWhiteSpace(restaurantName))
                            {
                                var add = AdminBackend.AddSpecificRestaurant(restaurantName, phoneNr, city);
                                if (add != null)
                                {
                                    Console.WriteLine("You have created new restaurant");
                                }
                                else
                                {
                                    Console.WriteLine("Ooops! Something went wrong! Please try again!");
                                }
                            }
                            Console.WriteLine("Press BACKSPACE to go back");
                            cki = Console.ReadKey();
                        } while (cki.Key != ConsoleKey.Backspace);
                    }
                }

            }
        }
    }
}
