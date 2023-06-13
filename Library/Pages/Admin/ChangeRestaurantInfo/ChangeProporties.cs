namespace Library;
public class ChangeProportiesPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;

    public ChangeProportiesPage()
    {
        Restaurant restaurant = Data.Restaurant;
        QuestionsAnswers["Name:"] = restaurant.Name;
        QuestionsAnswers["Open Time:"] = restaurant.OpenTime.ToString();
        QuestionsAnswers["Close Time:"] = restaurant.CloseTime.ToString();
        QuestionsAnswers["Two-Person-Tables:"] = restaurant.TwoPersonTables.ToString();
        QuestionsAnswers["Four-Person-Tables:"] = restaurant.FourPersonTables.ToString();
        QuestionsAnswers["Six-Person-Tables:"] = restaurant.SixPersonTables.ToString();
        QuestionsAnswers["Bar Chairs:"] = restaurant.BarChairs.ToString();
    }

    public override void Display()
    {
        string[] options = new string[] { "Name:", "Open Time:", "Close Time:", "Two-Person-Tables:", "Four-Person-Tables:", "Six-Person-Tables:", "Bar Chairs:", "[Change]" };
        int choice = Navigate("Change the restaurant's proporties:", options);
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
        RestaurantLogic.ChangeRestaurant(QuestionsAnswers["Name:"], TimeOnly.Parse(QuestionsAnswers["Open Time:"]),
                                        TimeOnly.Parse(QuestionsAnswers["Close Time:"]), int.Parse(QuestionsAnswers["Two-Person-Tables:"]),
                                        int.Parse(QuestionsAnswers["Four-Person-Tables:"]), int.Parse(QuestionsAnswers["Six-Person-Tables:"]),
                                        int.Parse(QuestionsAnswers["Bar Chairs:"]));
        Utils.Debug("Proporties successfully changed!");
        Router.GoBack();
    }

    public (bool, string) AreValidInputs(string[] options)
    {
        string errorMessage;
        for (int i = 1; i < 3; i++)
        {
            if (!TimeOnly.TryParse(QuestionsAnswers[options[i]], out TimeOnly time))
            {
                errorMessage = "Time has to be in the format of 00:00.";
                return (false, errorMessage);
            }
        }
        for (int i = 3; i < 7; i++)
            if (!int.TryParse(QuestionsAnswers[options[i]], out int seats) || seats <= 0)
            {
                errorMessage = "Seats has to be a number and be higher than 0.";
                return (false, errorMessage);
            }
        return (true, "");
    }
}