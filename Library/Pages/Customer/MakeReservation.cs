namespace Library;

public class MakeReservationPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = new string[] { "Quantity people:", "Date", "Time", "Name:", "Phone Number:", "E-mailaddress:", "[Make Reservation]" };
        int choice = Navigate("Make a reservation:", options);
        if (choice == 1 || choice == 2) // summary: for the options Date and Time, their answers get added to this pages QuestionsAnswers Dictionary.
        {
            Page page = ChoosePage(choice);
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
        // algorithm starts
        Reservation reservation = ReservationLogic.CreateReservation(int.Parse(QuestionsAnswers["Quantity people:"]), DateOnly.Parse(QuestionsAnswers["Date"]),
                                                                    TimeOnly.Parse(QuestionsAnswers["Time"]), QuestionsAnswers["Name:"],
                                                                    QuestionsAnswers["Phone Number:"], QuestionsAnswers["E-mailaddress:"]);
        bool isPossible = AlgorithmLogic.AlgorithmIsPossible(reservation);
        if (isPossible == false)
        {
            TimeOnly newTime = HandleSuggestions(reservation); // only returns if user chooses a suggestion, else it just goes back
            Router.Pop(); // pops ChooseSuggestionPage
            QuestionsAnswers["Time"] = newTime.ToString();
            Utils.Debug("Your chosen time has changed. Reservation is ready to be made. Try again!");
            Display(); return;
        }
        ReservationLogic.AddReservation(reservation);
        Utils.Debug($"Congratulations! Your reservation has been made.\nYour reservation code is: {reservation.ReservationCode}");
        Router.GoBack();
    }

    public override Page ChoosePage(int input)
    {
        Page page = null!;
        switch (input)
        {
            case 1: page = new ChooseDatePage(); break;
            case 2: page = new ChooseTimePage(); break;
        }
        return page;
    }

    public ValueTuple<bool, string> AreValidInputs(string[] options)
    {
        string errorMessage;
        int maxCap = Data.Restaurant.TwoPersonTables + Data.Restaurant.FourPersonTables + Data.Restaurant.SixPersonTables + Data.Restaurant.BarChairs;
        if (!int.TryParse(QuestionsAnswers[options[0]], out int quantity) || quantity > maxCap)
        {
            errorMessage = quantity > 100 ? "You can't reservate for more than 100 people online." : "Quantity people has to be a number.";
            return ValueTuple.Create(false, errorMessage);
        }
        if (!DateOnly.TryParse(QuestionsAnswers[options[1]], out DateOnly date) || date < DateOnly.FromDateTime(DateTime.Now)) // you can't reservate for in the past
        {
            errorMessage = date < DateOnly.FromDateTime(DateTime.Now) ? "You can't select a date of the past." : "Your custom date is an invalid date.";
            return ValueTuple.Create(false, errorMessage);
        }
        if (!TimeOnly.TryParse(QuestionsAnswers[options[2]], out TimeOnly time) || date.ToDateTime(time) < DateTime.Now)
        {
            errorMessage = date.ToDateTime(time) < DateTime.Now ? "Your chosen time has already passed." : "Your chosen time could not be converted to a TimeOnly object.";
            return ValueTuple.Create(false, errorMessage);
        }
        if (!QuestionsAnswers[options[3]].Replace(" ", "").All(char.IsLetter))
        {
            errorMessage = "Your name can only consist of alphabetical letters.";
            return ValueTuple.Create(false, errorMessage);
        }
        if (!QuestionsAnswers[options[4]].Replace(" ", "").All(char.IsDigit))
        {
            errorMessage = "Your phone number can only consist of numbers.";
            return ValueTuple.Create(false, errorMessage);
        }
        if (!QuestionsAnswers[options[5]].Contains('@') || !QuestionsAnswers[options[5]].Contains('.'))
        {
            errorMessage = "Your email has to contain a '@' and '.'.";
            return ValueTuple.Create(false, errorMessage);
        }
        return ValueTuple.Create(true, "");
    }

    public TimeOnly HandleSuggestions(Reservation reservation)
    {
        List<TimeOnly> suggestions = AlgorithmLogic.AlgorithmSuggestions(reservation);
        string[] options = ChooseTimePage.TimeOnlyToString(suggestions.ToArray());

        List<string> list = options.ToList(); // adds a not interested button for the user
        list.Add("Not interested");
        options = list.ToArray();

        string prompt = "\nThe timeslot you chose is already fully booked. Here are some suggestions:";
        ChooseSuggestionPage page = new ChooseSuggestionPage(prompt, options);
        Router.PushPage(page);
        Router.ViewCurrentPage();
        if (page.QuestionsAnswers["Suggestion"] == "Not interested")
        {
            Router.GoBack();
        }
        AddToQuestionsAnswers(page.QuestionsAnswers.FirstOrDefault());
        return TimeOnly.Parse(QuestionsAnswers["Suggestion"]);
    }
}