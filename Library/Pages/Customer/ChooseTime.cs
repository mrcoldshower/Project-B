namespace Library;

public class ChooseTimePage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = TimeOnlyToString(GetTimes());
        int choice = Navigate("Choose a time:", options, "", "");
        QuestionsAnswers.Add("Time", options[choice]);
    }

    public static TimeOnly[] GetTimes()
    {
        Restaurant restaurant = Data.Restaurant;
        int duration = (restaurant.CloseTime - restaurant.OpenTime).Hours + 1;
        TimeOnly[] nextDates = new TimeOnly[duration];
        TimeOnly start = restaurant.OpenTime;
        for (int i = 0; i < nextDates.Length; i++)
        {
            nextDates[i] = start;
            start = start.AddHours(1);
        }

        return nextDates;
    }

    public static string[] TimeOnlyToString(TimeOnly[] arr)
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