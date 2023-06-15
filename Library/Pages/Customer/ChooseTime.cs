namespace Library;

public class ChooseTimePage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = TimeOnlyToString(GetTimes());
        int choice = Navigate("Choose a time:", options);
        QuestionsAnswers.Add("Time", options[choice]);
    }

    public static TimeOnly[] GetTimes()
    {
        Restaurant restaurant = Data.Restaurant;
        int duration = (restaurant.CloseTime - restaurant.OpenTime).Hours;
        TimeOnly[] nextTimes = new TimeOnly[duration];
        TimeOnly start = restaurant.OpenTime;
        for (int i = 0; i < nextTimes.Length; i++)
        {
            nextTimes[i] = start;
            start = start.AddHours(1);
        }

        return nextTimes;
    }

    public static string[] TimeOnlyToString(TimeOnly[] arr)
    {
        string[] nextTimes = new string[arr.Length];

        for (int i = 0; i < arr.Length; i++)
        {
            string currentTime = arr[i].ToString();
            nextTimes[i] = currentTime;
        }

        return nextTimes;
    }
}