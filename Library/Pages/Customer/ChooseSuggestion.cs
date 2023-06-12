namespace Library;

public class ChooseSuggestionPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public string Prompt { get; set; }
    public string[] Options { get; set; }

    public ChooseSuggestionPage(string prompt, string[] options)
    {
        Prompt = prompt;
        Options = options;
    }

    public override void Display()
    {
        int choice = Navigate(Prompt, Options);
        QuestionsAnswers.Add("Suggestion", Options[choice]);
    }
}