using Library;

public class Program
{
    public static void Main(string[] args)
    {
        // ReservationLogic.FillTodayWithTestReservations(12, new TimeOnly(18, 0));
        ReservationLogic.RemoveOldReservations(); // Removes reservations from json that are more than 1 month old
        Console.CursorVisible = false;
        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided. Use one of the following: customer, admin, waiter.");
            return;
        }

        switch (args[0].ToLower())
        {
            case "customer": Customer(); break;
            case "admin": Admin(); break;
            case "waiter": Waiter(); break;
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