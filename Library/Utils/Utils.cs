namespace Library;
public static class Utils
{
    public static void ExitApplication()
    {
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey(true);
        Environment.Exit(0);
    }

    public static void Incomplete()
    {
        Console.WriteLine("\nThis part of the program is incomplete. Press any key to go back...");
        Console.ReadKey(true);
        Router.GoBack();
    }

    public static void LoadingStatus(int time = 3)
    {
        Console.Write("Loading ");
        for (int i = 0; i < 3; i++)
        {
            System.Threading.Thread.Sleep(time * 100);
            Console.Write(".");
        }
        Console.WriteLine();
    }

    public static void Debug(string message)
    {
        Console.WriteLine($"Debug: {message}");
        Console.ReadKey(true);
    }
}