using DataLayer.Backend;
using FoodRescue_Projekt;

AdminBackend.PrepDatabase(); //Detta kan raderas sedan.

ConsoleKeyInfo cki = default;
void Menu()
{
    while (cki.Key != ConsoleKey.Escape)
    {
        Console.Clear();
        Console.WriteLine("[1]: Customer\n[2]: Restaurant\n[3]: Admin");
        cki = Console.ReadKey();

        while (cki.Key is (ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3) or ConsoleKey.Escape)
        {
            if (cki.Key == ConsoleKey.D1)
            {
                do
                {
                    Console.Clear();
                    var customerclient = new CustomerClient();
                    customerclient.client();
                    Console.WriteLine("Press BACKSPACE to go back");
                    cki = Console.ReadKey();
                } while (cki.Key != ConsoleKey.Backspace);
            }

            if (cki.Key == ConsoleKey.D2)
            {
                do
                {
                    Console.Clear();
                    var restaurantfrontent = new RestaurantFrontend();
                    restaurantfrontent.Restaurant();
                    Console.WriteLine("Press BACKSPACE to go back");
                    cki = Console.ReadKey();
                } while (cki.Key != ConsoleKey.Backspace);
            }

            if (cki.Key == ConsoleKey.D3)
            {
                do
                {
                    //Hanna is admin
                    Console.Clear();
                    var adminfrontend = new AdminFrontend();
                    adminfrontend.Admin();
                    Console.WriteLine("Press BACKSPACE to go back");
                    cki = Console.ReadKey();
                } while (cki.Key != ConsoleKey.Backspace);
            }
        }
    }
}

Menu();