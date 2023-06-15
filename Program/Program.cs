using Library;

public class Program
{
    public static void Main()
    {
        ReservationLogic.RemoveOldReservations(); // Removes reservations from json that are more than 1 month old
        Console.CursorVisible = false;
        Admin();
    }

    public static void Customer()
    {
        CustomerHomePage homePage = new();
        Router.PushPage(homePage);
        Router.ViewCurrentPage();
    }

    public static void Admin()
    {
        AdminHomePage homePage = new();
        Router.PushPage(homePage);
        Router.ViewCurrentPage();
    }

    public static void Waiter()
    {
        WaiterHomePage homePage = new();
        Router.PushPage(homePage);
        Router.ViewCurrentPage();
    }

    public static void RealMain(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided.");
            return;
        }

        switch (args[0])
        {
            case "Customer": Customer(); break;
            case "Admin": Admin(); break;
            case "Waiter": Waiter(); break;
        }
    }
}