namespace Library;

using Newtonsoft.Json;

public class SeeMenuCardPage : Page
{
    private const int PageSize = 15;
    private static int currentPageIndex = 0;
    private static dynamic[] jsonData;

    public override void Display()
    {
        LoadJsonData(); 
        ShowCurrentPage(); 

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.RightArrow && currentPageIndex <= jsonData.Count())
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
                break;
            }
        }
        
    }

    private static void LoadJsonData()
    {
        string jsonFilePath = "../Library/Data/food.json";
        string jsonDataString = File.ReadAllText(jsonFilePath);
        jsonData = JsonConvert.DeserializeObject<dynamic[]>(jsonDataString);
    }

    private static void ShowCurrentPage()
    {
        int startIndex = currentPageIndex * PageSize;
        int endIndex = startIndex + PageSize;

        if (startIndex >= jsonData.Length)
        {
            Console.WriteLine("No more pages left.");
            return;
        }

        Console.Clear();
        Console.WriteLine($"Page {currentPageIndex + 1}:\n");

        for (int i = startIndex; i < endIndex && i < jsonData.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {jsonData[i].name} | {jsonData[i].description} | ${jsonData[i].price}");
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