namespace Library;
public class ChooseDishIDPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = new string[] { "ID:", "[Find]" };
        int choice = Navigate("Find the dish you want to change:", options);
        bool filled = AreQuestionsFilled(options);
        if (filled == false)
        {
            Utils.Debug("The user has not filled in all the required input fields yet.");
            Display();
            return;
        }
        (bool, string) valid = AreValidInputs(options);
        if (valid.Item1 == false)
        {
            Utils.Debug(valid.Item2);
            Display(); return;
        }
    }

    public (bool, string) AreValidInputs(string[] options)
    {
        string errorMessage;
        int maxId = Data.Foods.Last().Id;
        if (!int.TryParse(QuestionsAnswers[options[0]], out int id) || (id < 1 || maxId < id))
        {
            if (id < 1 || maxId < id) errorMessage = "Provided Id doesn't exists";
            else errorMessage = "Id has to be a number.";
            return (false, errorMessage);
        }
        return (true, "");
    }
}