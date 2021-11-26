using DataLayer.Backend;
namespace FoodRescue_Projekt;
public class CustomerClient
{
    private ConsoleKeyInfo cki;
    public void client()
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
                    Console.WriteLine("[1]: View Products \n[2]: View purchase history");
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
                        var FoodBoxList = UserBackend.AllUnsoldFoodBoxes();
                        if (FoodBoxList.Count > 0)
                        {
                            int i = 0;
                            foreach (var box in FoodBoxList)
                            {
                                i++;
                                var name = box.FoodBox;
                                while (name.Length < 25)
                                {
                                    name += " ";
                                }
                                Console.WriteLine($"{i}. | {name} \t| {box.Price}kr \t| {box.ExpiryDate} \t|");
                            }
                            Console.WriteLine("Press BACKSPACE to go back");
                            Console.WriteLine("Press the number of the foodbox you wish to purchase");
                            cki = Console.ReadKey();
                            int buyme;
                            var isint = int.TryParse(cki.KeyChar.ToString(), out buyme);
                            if (isint)
                            {
                                if (buyme != null)
                                {
                                    try
                                    {
                                        int BoxId = FoodBoxList[buyme - 1].FoodOrderId;
                                        UserBackend.BuyFoodBox(username, BoxId);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"No foodbox with the number {buyme} exists...");
                                        Console.ResetColor();
                                        Thread.Sleep(1000);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("There are no foodboxes in stock at the moment!");
                            Console.ResetColor();
                            Console.WriteLine("Try again later...");
                            Thread.Sleep(1000);
                            break;
                        }
                    } while (cki.Key != ConsoleKey.Backspace);
                }
                if (cki.Key == ConsoleKey.D2)
                {
                    do
                    {
                        Console.Clear();
                        var userPurchaseHistory = UserBackend.UserPurchaseHistory(username);
                        foreach (var box in userPurchaseHistory)
                        {
                            Console.WriteLine($"{box.FoodBox} | {box.Price} | {box.ExpiryDate}");
                        }
                        Console.WriteLine("Press BACKSPACE to go back");
                        cki = Console.ReadKey();
                    } while (cki.Key != ConsoleKey.Backspace);
                }
            }
        }
    }
}

