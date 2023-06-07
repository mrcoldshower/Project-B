namespace Library;

public class CancelReservationPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = new string[] { "Email:", "Reservation Code:", "[Cancel Reservation]" };
        int choice = Navigate("Cancel a reservation:", options, "", "");
        bool filled = AreQuestionsFilled(options);
        if (filled == false)
        {
            Utils.Debug("The user has not filled in all the required input fields yet.");
            Display();
        }
        Reservation? reservation = ReservationLogic.FindReservation(x => x.Email == QuestionsAnswers["Email:"] && x.ReservationCode == QuestionsAnswers["Reservation Code:"]);
        if (reservation == null)
        {
            Utils.Debug("Can't find reservation");
            Display();
        }
        ReservationLogic.RemoveReservation(reservation!);
        Utils.Debug("You have canceled your reservation");
        Router.GoBack();
    }
}
