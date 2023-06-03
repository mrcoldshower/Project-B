public class PushView
{
    public static void PrintStringDictionary(Dictionary<string, string> dict)
    {
        foreach (var item in dict)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}