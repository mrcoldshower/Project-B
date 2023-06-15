namespace Library;

using Newtonsoft.Json;

public class SeeMenuCardPage : Page
{
    private const int PageSize = 10;
    private static int currentPageIndex = 0;
    

    public override void Display()
    {
        
        ShowCurrentPage(); 

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.RightArrow && currentPageIndex <= Data.Foods.Count)
            {
                currentPageIndex++;
                ShowCurrentPage();
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow && currentPageIndex > 0)
            {
                currentPageIndex--;
                ShowCurrentPage();
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                Router.GoBack();
            }
        }
        
    }


    private static void ShowCurrentPage()
    {
        int startIndex = currentPageIndex * PageSize;
        int endIndex = startIndex + PageSize;

        if (startIndex >= Data.Foods.Count)
        {
            Console.WriteLine("No more pages left.");
            return;
        }

        Console.Clear();
        Console.WriteLine($"Page {currentPageIndex + 1}:\n");

        for (int i = startIndex; i < endIndex && i < Data.Foods.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Data.Foods[i].Name} | {Data.Foods[i].Description} | ${Data.Foods[i].Price}");
        }

        if (currentPageIndex >= endIndex)
        {
            Console.WriteLine("\nPress the right arrow key for the next page.");
        }

        if (currentPageIndex > 0)
        {
            Console.WriteLine("Press the left arrow key for the previous page.");
        }

        Console.WriteLine("Press the ESC key to exit.");
    }


    public override Page ChoosePage(int input)
    {
        return null!;
    }
}