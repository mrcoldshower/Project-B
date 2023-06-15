namespace Library;

public class ChooseDatePage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = AddCustomString(DateOnlyToString(GetDates(amount: 7))); // amount: how many dates you want the user to pick from
        int choice = Navigate("Choose a date:", options);
        if (options[choice] == "[Create]")
        {   // user made a custom date.
            bool filled = AreQuestionsFilled(options);
            if (filled == false)
            {
                Utils.Debug("The user has not filled in all the required input fields yet.");
                Display();
                return;
            }
            Utils.Debug("--");
            string customDate = QuestionsAnswers["Custom date:"];
            Utils.Debug(customDate);
            if (QuestionsAnswers.ContainsKey("Custom date:"))
            {
                QuestionsAnswers["Date"] = customDate;
                QuestionsAnswers.Remove("Custome date:");
            }
            else QuestionsAnswers.Add("Date", customDate);
        }
        else QuestionsAnswers.Add("Date", options[choice]);
    }

    public DateOnly[] GetDates(int amount)
    {
        // Restaurant restaurant = new Restaurant(); // GET THE OPEN DAYS
        DateOnly[] nextDates = new DateOnly[amount];

        for (int i = 0; i < amount; i++)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now.AddDays(i));
            nextDates[i] = currentDate;
        }

        return nextDates;
    }

    public string[] DateOnlyToString(DateOnly[] arr)
    {
        string[] nextDates = new string[arr.Length];

        for (int i = 0; i < arr.Length; i++)
        {
            string currentDate = arr[i].ToString();
            nextDates[i] = currentDate;
        }

        return nextDates;
    }

    public string[] AddCustomString(string[] arr)
    {
        List<string> list = arr.ToList();
        list.Add("Custom date:");
        list.Add("[Create]");
        arr = list.ToArray();
        return arr;
    }
}