using DataLayer.Backend;
namespace FoodRescue_Projekt;
public class RestaurantFrontend
{
    private ConsoleKeyInfo cki;
    public void Restaurant()
    {
        //Kajsa och Sandra är servitriser 
        var userList = AdminBackend.AllUsers().Where(s => s.IsWaitress == true);
        var userNameList = new List<string>();

        foreach (var user in userList)
        {
            userNameList.Add(user.Username);
            userNameList.Add(user.Password);
        }
        while (cki.Key != ConsoleKey.Escape)
        {
            string username = "";
            string password = "";
            bool loggedIn = false;

            Console.Clear();
            Console.WriteLine("Press any key to login");
            Console.WriteLine("Press ESCAPE to exit");
            cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.Escape) break;
            Console.Clear();
            Console.Write("Username: ");
            username = Console.ReadLine();
            
            if (!userNameList.Contains(username))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User is not service personal. Please try again!");
                Console.ResetColor();
                Thread.Sleep(1000);
                break;
            }
            Console.Write("Password: ");
            password = Console.ReadLine();
            if (!userNameList.Contains(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User is not service personal. Please try again!");
                Console.ResetColor();
                Thread.Sleep(1000);
                break;
            }
            loggedIn = userNameList.Contains(username);

            while (loggedIn)
            {
                Console.Clear();
                do
                {
                    Console.Clear();
                    Console.WriteLine(
                        "[1]: View sold food boxes \n[2]: View unsold food boxes \n[3]: Add a new food box to a restaurant");
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Backspace)
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Are you sure you want to log out?[Y/N]");
                            var choice = Console.ReadKey();
                            if (choice.Key == ConsoleKey.Y)
                            {
                                loggedIn = false;
                                username = "";
                                break;
                            }

                            if (choice.Key == ConsoleKey.N)
                            {
                                break;
                            }
                        } while (true);
                    }
                    if (loggedIn == false)
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
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThe restaurant has no sold food boxes yet!!...");
                            Console.ResetColor();
                            
                        }
                        Console.WriteLine("Press BACKSPACE to go back");
                        cki = Console.ReadKey();
                    } while (cki.Key != ConsoleKey.Backspace);
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
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n\nNo unsold lunch boxes available for this restaurant!!...");
                            Console.ResetColor();

                            Console.WriteLine("Press BACKSPACE to go back");
                            cki = Console.ReadKey();
                        }
                    } while (cki.Key != ConsoleKey.Backspace);
                }
                if (cki.Key == ConsoleKey.D3)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("\t\t\tAll restaurants:\t");
                        var restaurants = AdminBackend.ShowRestaurants();
                        foreach (var r in restaurants)
                            Console.WriteLine(
                                $"| Restaurant: {r.RestaurantName}, Phone number: {r.PhoneNumber}, City: {r.City}|");
                        Console.Write("\n\tEnter a restaurant you want to add a new lunch box to:\t");
                        var restaurant = Console.ReadLine();
                        Console.Write("\tEnter food type for the food box [Fisk/Kött/Vego]:\t");
                        var foodType = Console.ReadLine();
                        Console.Write("\tEnter dish for the food box:\t");
                        var foodBox = Console.ReadLine();
                        Console.Write("\tEnter price for the food box:\t");
                        var price = Console.ReadLine();
                        Console.Write("\tEnter expiry date for the food box:\t");
                        var expiryDate = Console.ReadLine();
                        if (!(string.IsNullOrWhiteSpace(restaurant) && string.IsNullOrWhiteSpace(foodType) && string.IsNullOrWhiteSpace(foodBox) && string.IsNullOrWhiteSpace(price) && string.IsNullOrWhiteSpace(expiryDate)))
                        {
                            var date=Convert.ToDateTime(expiryDate);
                            var prices = Convert.ToInt32(price);
                            var newFoodBoxAdded =
                                RestaurantBackend.AddNewFoodBoxToAnExistingRestaurant(restaurant, foodType, foodBox, prices, date);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n\tNew food box was added to {newFoodBoxAdded.RestaurantName}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n\tYou were not able to add a new food box! Please try again...");
                            Console.ResetColor();
                            Thread.Sleep(3000);
                            break;
                        }
                        Console.WriteLine("\tPress BACKSPACE to go back");
                        cki = Console.ReadKey();
                    } while (cki.Key != ConsoleKey.Backspace);
                }
            }
        }
    }
}
