public class GetInput
{
    public static int GetInt()
    {
        int input = int.Parse((Console.ReadLine() ?? "0").TrimEnd());
        return input;
    }
}