namespace Library;
public class SubPage : Page
{
    public override bool IsQuestionPage { get; set; } = true; // set to true, because it will mostly be used for Answering stuff, and not to navigate pages
    public string Prompt { get; set; }
    public string[] Options { get; set; }

    public SubPage(string prompt, string[] options, bool isQuestionPage = true)
    {
        Prompt = prompt;
        Options = options;
        IsQuestionPage = isQuestionPage;
    }

    public override void Display()
    {
        Navigate(Prompt, Options, "", "");
        Router.Pop();
    }

    public override Page ChoosePage(int input)
    {
        return null!;
    }
}