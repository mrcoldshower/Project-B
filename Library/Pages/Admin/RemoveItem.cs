namespace Library;

using Newtonsoft.Json;

public class RemoveItem : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = {
            "ID:",
            "[Remove item]"
        };
        int choice = Navigate("What item do you want to delete?", options, "", "");

        // Starts with verifying if item can be removed.
        AddToQuestionsAnswers(QuestionsAnswers.FirstOrDefault());
        bool filled = AreQuestionsFilled(options);
        if (filled == false)
        {
            Utils.Debug("The user has not filled in all the required input fields yet.");
            Display();
        }
        ValueTuple<bool, string> valid = IsValidInput(options);
        if (valid.Item1 == false)
        {
            Utils.Debug(valid.Item2);
            Display();
        }

        int Id = int.Parse(QuestionsAnswers["ID:"]);

        if (IsDeleted(Id) == true)
        {
            Utils.Debug($"Your item has been deleted!");
            Router.GoBack();
        }
        else
        {
            Utils.Debug("An unknown error has occured. Please try again.");
            Router.GoBack();
        }
        
    }

    private List<Food> GetAllJsonRecords() => JsonConvert.DeserializeObject<List<Food>>(File.ReadAllText("../Library/Data/food.json"))!;

    public bool IsDeleted(int id)
    {
        List<Food> objects = Data.Foods;

        Food objectToRemove = objects.Find(o => o.Id == id);
        if (objectToRemove != null)
        {
            try
            {
                objects.Remove(objectToRemove);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Process failed: {e.Message}");
                return false;
            }
        }

        for (int i = 0; i < objects.Count; i++)
        {
            int j = i + 1;
            objects[i].Id = j;
        }

        Data.FoodAccess.WriteAll(objects);
        Data.Foods = objects;
        return true;
    }

    public ValueTuple<bool, string> IsValidInput(string[] options)
    {
        string errorMessage;
        int ItemCount = Data.Foods.Count;
        if (!int.TryParse(QuestionsAnswers[options[0]], out int id) || id > Data.Foods.Count) // a 100 people reservation is a good max capacity in my opinion
        {
            errorMessage = id > ItemCount ? "Invalid ID." : "ID has to be a number.";
            return ValueTuple.Create(false, errorMessage);
        }
        
        return ValueTuple.Create(true, "");
    }

    public bool AreQuestionsFilled(string[] options)
    {
        foreach (var option in options)
        {
            if (!QuestionsAnswers.ContainsKey(option) && option[0] != '[') return false;
        }
        return true;
    }

    public override Page ChoosePage(int input)
    {
        return null!;
    }
}