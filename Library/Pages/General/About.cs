namespace Library;
public class AboutPage : Page
{
    public override void Display()
    {
        Console.WriteLine(Data.Restaurant.AboutDescription);
        Console.WriteLine("Press any key to return to the home page...");
        ConsoleKeyInfo cki = Console.ReadKey(true);
        if (cki.Key == ConsoleKey.Escape) Utils.ExitApplication();
        Router.GoBack();
    }

    public override Page ChoosePage(int input)
    {
        return null!;
    }
}