using DataLayer;
using DataLayer.Backend;

namespace FoodRescue_Projekt;

public class RestaurantFrontend
{

    private ConsoleKeyInfo cki;

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
                    Console.WriteLine(
                        "[1]: View sold food boxes [2]: View unsold food boxes [3]: Add a new food box to a restaurant");
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
                } while (cki.Key is not (ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3 or ConsoleKey.Escape));

                if (cki.Key == ConsoleKey.D1)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("\t\t\tAll restaurants:\t");
                        var restaurants = AdminBackend.ShowRestaurants();
                        foreach (var r in restaurants)
                            Console.WriteLine(
                                $"| Restaurant: {r.RestaurantName}, Phone number: {r.PhoneNumber}, City: {r.City}|");

                        Console.WriteLine("\n\tEnter a restaurant you want to see sold lunch boxes for:\t");
                        var restaurant = Console.ReadLine();
                        Console.Clear();
                        var restaurantsList = RestaurantBackend.AllSoldFoodBoxes(restaurant);
                        if (restaurantsList.Count > 0)
                        {
                            foreach (var box in restaurantsList)
                            {
                                Console.WriteLine(
                                    $" | {box.FoodType} \t| {box.FoodBox} \t| {box.Price}kr \t|");
                            }
                        }


                        Console.WriteLine("Press BACKSPACE to go back");
                        cki = Console.ReadKey();

                    } while (cki.Key is not ((ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3 or ConsoleKey.Escape)));
                }

                if (cki.Key == ConsoleKey.D2)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("\t\t\tAll restaurants:\t");
                        var restaurants = AdminBackend.ShowRestaurants();
                        foreach (var r in restaurants)
                            Console.WriteLine(
                                $"| Restaurant: {r.RestaurantName}, Phone number: {r.PhoneNumber}, City: {r.City}|");

                        Console.WriteLine("\n\tEnter a restaurant you want to see unsold lunch boxes for:\t");
                        var restaurant = Console.ReadLine();
                        Console.Clear();
                        var restaurantsList = RestaurantBackend.AllUnsoldFoodBoxes(restaurant);
                        if (restaurantsList.Count > 0)
                        {
                            foreach (var box in restaurantsList)
                            {
                                Console.WriteLine(
                                    $" | {box.FoodType} \t| {box.FoodBox} \t| {box.Price}kr \t|");
                            }

                            Console.WriteLine("Press BACKSPACE to go back");
                            cki = Console.ReadKey();
                        }
                    } while (cki.Key is not ((ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3
                             or ConsoleKey.Escape)));

                }

                if (cki.Key == ConsoleKey.D3)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("\t\t\tAll restaurants:     \t");
                        var restaurants = AdminBackend.ShowRestaurants();
                        foreach (var r in restaurants)
                            Console.WriteLine(
                                $"| Restaurant: {r.RestaurantName}, Phone number: {r.PhoneNumber}, City: {r.City}|");

                        Console.WriteLine("\n\tEnter a restaurant you want to add a new lunch box to:\t");
                        var restaurant = Console.ReadLine();
                        Console.WriteLine("\tEnter food type for the food box [Fisk/Kött/Vego]:\t");
                        var foodType = Console.ReadLine();
                        Console.WriteLine("\tEnter dish for the food box:\t");
                        var foodBox = Console.ReadLine();
                        Console.WriteLine("\tEnter price for the food box:\t");
                        var price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\tEnter expiry date for the food box:\t");
                        var expiryDate = Convert.ToDateTime(Console.ReadLine());

                        Console.Clear();

                        var newFoodBoxAdded =
                            RestaurantBackend.AddNewFoodBoxToAnExistingRestaurant(restaurant, foodType, foodBox,
                                price, expiryDate);

                        if (newFoodBoxAdded != null)
                        {
                            Console.WriteLine("New food box was added to the restaurant");
                        }
                        else
                        {
                            Console.WriteLine("You were not able to add a new food box, Please try again!");
                        }

                        Console.WriteLine("Press BACKSPACE to go back");
                        cki = Console.ReadKey();
                    } while (cki.Key is not ((ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3
                             or ConsoleKey.Escape)));
                }

                else

                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Try again later...");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    break;
                }
            }
            cki = Console.ReadKey();
            while (cki.Key != ConsoleKey.Backspace) ;
        }
    }
}