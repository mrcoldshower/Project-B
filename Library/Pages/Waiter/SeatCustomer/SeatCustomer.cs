namespace Library;
public class SeatCustomerPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = new string[] { "Reservation Code:", "[Search]" };
        int choice = Navigate("Seat a customer page!", options);
        bool filled = AreQuestionsFilled(options);
        if (filled == false)
        {
            Utils.Debug("The user has not filled in all the required input fields yet.");
            Display();
            return;
        }
        string code = QuestionsAnswers["Reservation Code:"];
        Reservation? searchedReservation = ReservationLogic.FindReservation(x => x.ReservationCode == code);
        bool isValid = IsCodeValid(code, searchedReservation);
        if (isValid == false)
        {
            Utils.Debug("The reservation code is not valid.");
            Display();
            return;
        }
        Utils.Debug($"{searchedReservation!.ToString()}");
        Dictionary<Reservation, List<int>>? Tables = AlgorithmLogic.AlgorithmReturnTables(searchedReservation);
        if (Tables == null)
        {
            Utils.Debug("Error. Null type at Algorithm.AlgorithmReturnTables");
            Display();
            return;
        }
        ChooseTablePage page = new ChooseTablePage(searchedReservation, Tables);
        Router.PushPage(page);
        Router.ViewCurrentPage();
        Router.Pop();
        List<Table> tablesList = TablesLogic.TableIdsToTablesList(page.TableIds);
        CustomerLogic.CreateCustomer(tablesList);
        Utils.Debug("Success");
        Router.GoBack();
    }

    public bool IsCodeValid(string code, Reservation? reservation)
    {
        if (reservation == null) return false;
        if (reservation.Date != DateOnly.FromDateTime(DateTime.Now)) return false; // if the date is same as today's date then yes, else no
        return true;
    }


}