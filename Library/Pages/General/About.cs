namespace Library;
public class AboutPage : Page
{
    public override void Display()
    {
        Console.WriteLine(@"About page!

This online banking platform was created by Pablo Sanderman.
This is just to practise object-orientated-design-programming (OODP).

Keybinds:
W / Up Arrow                => Go Up,
A / Left Arrow              => Go Back,
S / Down Arrow              => Go Down,
D / Right Arrow / Enter     => Select
Escape                      => Exit program ");

        Console.WriteLine("\nPress any key to return to the home page...");
        ConsoleKeyInfo cki = Console.ReadKey(true);
        if (cki.Key == ConsoleKey.Escape) Utils.ExitApplication();
        Router.GoBack();
    }

    public override Page ChoosePage(int input)
    {
        return null!;
    }
}