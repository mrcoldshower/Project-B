using Library;

public class Program
{
    public static void Main(string[] args)
    {
        ReservationLogic.RemoveOldReservations(); // Removes reservations from json that are more than 1 month old
        Console.CursorVisible = false;
        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided. Use one of the following: Customer, Admin, Waiter.");
            return;
        }

        switch (args[0])
        {
            case "Customer": Customer(); break;
            case "Admin": Admin(); break;
            case "Waiter": Waiter(); break;
        }
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
}