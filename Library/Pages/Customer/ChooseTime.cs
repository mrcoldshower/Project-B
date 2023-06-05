namespace Library;

public class ChooseTimePage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = TimeOnlyToString(GetTimes(5));
        int choice = Navigate("Choose a time:", options, "", "");
        QuestionsAnswers.Add("Time", options[choice]);
    }

    public override Page ChoosePage(int input)
    {
        return null!;
    }

    public TimeOnly[] GetTimes(int amount)
    {
        Restaurant restaurant = new Restaurant();
        TimeOnly[] nextDates = new TimeOnly[amount];

        for (int i = 0; i < amount; i++)
        {
            TimeOnly currentDate = TimeOnly.FromDateTime(DateTime.Now.Date.AddHours(i + 1));
            nextDates[i] = currentDate;
        }

        return nextDates;
    }

    public string[] TimeOnlyToString(TimeOnly[] arr)
    {
        string[] nextDates = new string[arr.Length];

        for (int i = 0; i < arr.Length; i++)
        {
            string currentDate = arr[i].ToString();
            nextDates[i] = currentDate;
        }

        return nextDates;
    }
}