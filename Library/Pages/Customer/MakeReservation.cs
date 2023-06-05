namespace Library;

public class MakeReservationPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = new string[] { "Quantity people:", "Date", "Time", "Name:", "Phone Number:", "E-mailaddress:", "[Make Reservation]" };
        int choice = Navigate("Make a reservation:", options, "", "");
        if (choice == 1 || choice == 2)
        {
            Page page = ChoosePage(choice);
            Router.PushPage(page);
            Router.ViewCurrentPage();
            AddToQuestionsAnswers(page.QuestionsAnswers.FirstOrDefault());
            Router.GoBack();
        }
        else
        {
            bool filled = AreQuestionsFilled(options);
            if (filled == false)
            {
                Utils.Debug("Wrong input. Try again");
                Display();
            }

        }
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

    public bool AreQuestionsFilled(string[] options)
    {
        foreach (var option in options)
        {
            if (!QuestionsAnswers.ContainsKey(option) && option[0] != '[') return false;
        }
        return true;
    }
}