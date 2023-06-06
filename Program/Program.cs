using Library;

public class Program
{
    public static void Main()
    {
        Customer();
    }

    public static void Customer()
    {
        HomePage homePage = new();
        Router.PushPage(homePage);
        Router.ViewCurrentPage();
    }

    public static void Admin()
    {

    }

    public static void Waiter()
    {

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