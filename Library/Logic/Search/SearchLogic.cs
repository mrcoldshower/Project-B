namespace Library;
public class SearchLogic
{
    public static void SearchInformation<T>(List<T> data) // accepts every list of models that inherits from the Model class
    {
        List<IHasName> models = SearchName(data);
        if (models == null) return;
        Console.Clear();
        if (models.Count == 1) ShowModelInfo(models[0]); // if only one model was visible
        else
        {
            IHasName model = ChooseSearchedItem(models); // if multiple search items were visible
            Console.Clear();
            ShowModelInfo(model);
        }
        Utils.Debug("press enter");
    }

    public static IHasName SearchItem<T>(List<T> data) // return an item
    {
        List<IHasName> models = SearchName(data);
        if (models == null) return null!;
        if (models.Count == 1) return models[0];
        else
        {
            IHasName model = ChooseSearchedItem(models); // if multiple search items were visible
            return model;
        }
    }

    public static void SeeSearch(List<IHasName> modelData, string s)
    {
        Console.Clear();
        for (int i = 0; i < modelData.Count; i++)
        {
            if (modelData[i].Name.Length < s.Length) continue;
            if (modelData[i].Name.Substring(0, s.Length).ToLower() == s.ToLower())
            {
                Console.WriteLine(modelData[i].Name);
            }
        }
        Console.WriteLine("Enter a word to search:");
        Console.Write($"> {s}");
    }


    public static List<IHasName> SearchName<T>(List<T> data)
    {
        List<IHasName> modelData = data.Cast<IHasName>().ToList();
        string s = "";
        while (true)
        {
            Console.Clear();
            for (int i = 0; i < modelData.Count; i++)
            {
                if (modelData[i].Name.Length < s.Length) continue;
                if (modelData[i].Name.Substring(0, s.Length).ToLower() == s.ToLower())
                {
                    Console.WriteLine(modelData[i].Name);
                }
            }
            Console.WriteLine("Enter a word to search:");
            Console.Write($"> {s}");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                s += key.KeyChar.ToString() ?? "";
            }
            else
            {
                if (key.Key == ConsoleKey.Backspace && s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                }
                else if (key.Key == ConsoleKey.Enter && s == "")
                {
                    Utils.Debug("You canceled the order you were making");
                    return null!;
                }
                else if (key.Key == ConsoleKey.Enter) break;
            }
        }
        List<IHasName> models = new List<IHasName>();
        for (int i = 0; i < modelData.Count; i++)
        {
            if (modelData[i].Name.Length < s.Length) continue;
            if (modelData[i].Name.Substring(0, s.Length).ToLower() == s.ToLower())
            {
                models.Add(modelData[i]);
            }
        }
        return models;
    }

    // public static List<Model> SearchMultipleNames<T>(List<T> data)
    // {
    //     List<Model> modelData = data.Cast<Model>().ToList();
    //     string s = "";
    //     while (true)
    //     {
    //         SearchView.SeeSearch(modelData, s);
    //         ConsoleKeyInfo key = Console.ReadKey();
    //         if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
    //         {
    //             s += key.KeyChar.ToString() ?? "";
    //         }
    //         else
    //         {
    //             if (key.Key == ConsoleKey.Backspace && s.Length > 0)
    //             {
    //                 s = s.Substring(0, s.Length - 1);
    //             }
    //             else if (key.Key == ConsoleKey.Enter)
    //             {
    //                 List<Model> models = new List<Model>();
    //                 for (int i = 0; i < modelData.Count; i++)
    //                 {
    //                     if (modelData[i].Name.Length < s.Length) continue;
    //                     if (modelData[i].Name.Substring(0, s.Length).ToLower() == s.ToLower())
    //                     {
    //                         models.Add(modelData[i]);
    //                     }
    //                 }
    //                 if
    //                 return models;
    //             }
    //             break;
    //         }
    //     }
    //     return null!;
    // }

    public static IHasName ChooseSearchedItem(List<IHasName> models) // chooses an option
    {
        if ((models != null) && (!models.Any())) return null!; // if null or empty, then return null;
        Console.WriteLine("Choose an item:");
        for (int i = 0; i < models!.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {models[i].Name}");
        }
        IHasName chosenModel = Input.MultiChoiceIntCheck(models);
        return chosenModel;
    }

    public static void ShowModelInfo(IHasName model)
    {
        if (model == null) return;
        Console.WriteLine("Item information:");
        if (model is Food)
        {
            Food dish = (Food)model;
            Console.WriteLine($"ID: {dish.Id}\nDish name: {dish.Name}\nPrice: ${dish.Price}\nDescription: {dish.Description}");
        }
        else if (model is Reservation)
        {
            Reservation reservation = (Reservation)model;
            Console.WriteLine(reservation.ToString());
        }
    }
}