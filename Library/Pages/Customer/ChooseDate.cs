namespace Library;

public class ChooseDatePage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = DateOnlyToString(GetDates(10));
        int choice = Navigate("Choose a date:", options, "", "");
        QuestionsAnswers.Add("Date", options[choice]);
    }

    public DateOnly[] GetDates(int amount)
    {
        Restaurant restaurant = new Restaurant();
        DateOnly[] nextDates = new DateOnly[amount];

        for (int i = 0; i < amount; i++)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now.AddDays(i + 1));
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
}