namespace Library;
public class Write
{
    public static void Text(string s)
    {
        Console.WriteLine(s);
    }

    public static void PrintStringDictionary(Dictionary<string, string> dict)
    {
        foreach (var item in dict)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}