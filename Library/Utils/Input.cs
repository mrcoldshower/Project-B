public static class Input
{
    public static int CheckIfInputIsInt()
    {
        int i = 0;
        while (true)
        {
            string input = Console.ReadLine() ?? "";
            if (int.TryParse(input, out i)) return int.Parse(input);
            else Console.WriteLine("Invalid input, your input has to be a number.");
        }
    }

    public static T MultiChoiceIntCheck<T>(List<T> objects)
    {
        while (true)
        {
            Console.Write("> ");
            int action = Input.CheckIfInputIsInt();
            for (int i = 0; i < objects.Count; i++)
            {
                if (action == i + 1) return objects[i];
            }
            Console.WriteLine("Invalid input. Choose a valid number.");
        }
    }
}