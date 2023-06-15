namespace Library;
public class AddItemPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = { "Name:", "Price:", "Description:", "Category", "[Add item]" };
        int choice = Navigate("Add a dish to the menu card:", options);
        if (choice == 3)
        {
            Page page = new ChooseCategoryPage();
            Router.PushPage(page);
            Router.ViewCurrentPage();
            AddToQuestionsAnswers(page.QuestionsAnswers.FirstOrDefault());
            Router.GoBack();
        }
        // Starts with verifying if Reservation can be made.
        bool filled = AreQuestionsFilled(options);
        if (filled == false)
        {
            Utils.Debug("The user has not filled in all the required input fields yet.");
            Display(); return;
        }
        ValueTuple<bool, string> valid = AreValidInputs(options);
        if (valid.Item1 == false)
        {
            Utils.Debug(valid.Item2);
            Display(); return;
        }
        // item can be added
        FoodLogic.AddDish(QuestionsAnswers["Name:"], Double.Parse(QuestionsAnswers["Price:"]), QuestionsAnswers["Description:"], (FoodCategory)Enum.Parse(typeof(FoodCategory), QuestionsAnswers["Category"]));
        Utils.Debug("Item successfully added!");
        Router.GoBack();
    }

    public (bool, string) AreValidInputs(string[] options)
    {
        string errorMessage;
        if (!QuestionsAnswers[options[0]].Replace(" ", "").All(Char.IsLetter))
        {
            errorMessage = "Name can only contain alphabetical letters.";
            return (false, errorMessage);
        }
        if (!Double.TryParse(QuestionsAnswers[options[1]], out double price) || price <= 0)
        {
            errorMessage = "Price has to be higher than 0.";
            return (false, errorMessage);
        }
        return (true, "");
    }

}