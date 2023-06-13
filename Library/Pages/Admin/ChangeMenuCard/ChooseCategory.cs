namespace Library;
public class ChooseCategoryPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = { "Fish", "Meat", "Vegan", "Vegetarian" };
        int choice = Navigate("Choose a category:", options);
        QuestionsAnswers.Add("Category", options[choice]);
    }
}