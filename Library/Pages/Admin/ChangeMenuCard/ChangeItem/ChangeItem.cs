namespace Library;
public class ChangeItemPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public int id { get; set; }
    public ChangeItemPage()
    {
        Page page1 = new ChooseDishIDPage();
        Router.PushPage(page1);
        Router.ViewCurrentPage();
        id = int.Parse(page1.QuestionsAnswers["ID:"]);
        Food dish = Data.Foods.Find(x => x.Id == id)!;
        QuestionsAnswers["Name:"] = dish.Name;
        QuestionsAnswers["Price:"] = dish.Price.ToString();
        QuestionsAnswers["Description:"] = dish.Description;
        QuestionsAnswers["Category"] = dish.Category.ToString();
    }

    public override void Display()
    {
        string[] options = { "Name:", "Price:", "Description:", "Category", "[Change item]" };
        int choice = Navigate("Change a the values of the dish:", options);
        if (choice == 3)
        {
            Page page2 = new ChooseCategoryPage();
            Router.PushPage(page2);
            Router.ViewCurrentPage();
            AddToQuestionsAnswers(page2.QuestionsAnswers.FirstOrDefault());
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
        FoodLogic.ChangeDish(id, QuestionsAnswers["Name:"], Double.Parse(QuestionsAnswers["Price:"]), QuestionsAnswers["Description:"], (FoodCategory)Enum.Parse(typeof(FoodCategory), QuestionsAnswers["Category"]));
        Utils.Debug("Item successfully changed!");
        Router.Pop();
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