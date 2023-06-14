namespace Library;
public class ChangeAboutPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;

    public override void Display()
    {
        Console.Clear();
        Console.CursorVisible = true;
        Console.WriteLine("How many lines do you want to write?");
        int lines = Input.CheckIfInputIsInt();
        Console.CursorVisible = true;
        string[] options = new string[lines + 1];
        for (int i = 0; i < lines; i++)
        {
            options[i] = $"Line {i + 1}:";
        }
        options[lines] = "[Finish]";
        int choice = Navigate("Change the about text:", options);
        string aboutDescription = "";
        for (int i = 0; i < lines; i++)
        {
            if (!QuestionsAnswers.ContainsKey(options[i])) aboutDescription += "\n";
            else aboutDescription += QuestionsAnswers[options[i]] + "\n";
        }
        RestaurantLogic.ChangeAboutDescription(aboutDescription);
        Console.WriteLine(aboutDescription);
        Utils.Debug("About description successfully changed!");
        Router.GoBack();
    }


}