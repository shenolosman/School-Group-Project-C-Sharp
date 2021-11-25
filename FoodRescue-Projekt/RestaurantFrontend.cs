using DataLayer;
using DataLayer.Backend;

namespace FoodRescue_Projekt;

public class RestaurantFrontend
{

    private ConsoleKeyInfo cki;
    private ConsoleKeyInfo ckiMain;

    public void Restaurant()
    {
        var UserList = AdminBackend.AllUsers();
        var UserNameList = new List<string>();

        foreach (var user in UserList)
        {
            UserNameList.Add(user.Username);
        }

        while (cki.Key != ConsoleKey.Escape)
        {
            string username = "";
            bool loggedin = false;

            Console.Clear();
            Console.WriteLine("Press any key to login");
            Console.WriteLine("Press ESCAPE to exit");
            cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.Escape) break;
            Console.Clear();
            Console.Write("Username: ");
            username = Console.ReadLine();
            if (!UserNameList.Contains(username))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User does not exist...");
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
                    Console.WriteLine("[1]: View sold food boxes [2]: View unsold food boxes [3]: Add a new food box to a restaurant");
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
                } while (cki.Key is not (ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.Escape));


                if (cki.Key == ConsoleKey.D1)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("|\tAll restaurants:\t|");
                        var restaurants = AdminBackend.ShowRestaurants();
                        foreach (var r in restaurants)
                            Console.WriteLine($"| Restaurant: {r.RestaurantName} |\tPhone number: {r.PhoneNumber} |\tCity: {r.City}\t|");

                        Console.WriteLine("|\tEnter a restaurant you want to see sold lunch boxes for:\t|");
                        var restaurant = Console.ReadLine();
                        Console.Clear();
                        var restaurantsList = RestaurantBackend.AllSoldFoodBoxes(restaurant);
                        if (restaurantsList.Count > 0)
                        {
                            //int i = 0;
                            foreach (var box in restaurantsList)
                            {
                                Console.WriteLine(
                                    $" | {box.FoodType} \t| {box.FoodBox} \t| {box.Price}kr \t|");
                            }
                        }

                        Console.ReadKey();
                        Console.WriteLine("Press BACKSPACE to go back");
                    } while (cki.Key is not ((ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.Escape)));
                }
                else
                {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.ResetColor();
                            Console.WriteLine("Try again later...");
                            Thread.Sleep(1000);
                            break;
                }
            } while (cki.Key != ConsoleKey.Backspace);
        }
    }
}