namespace Library;

public class DismissCustomerPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        string[] options = new string[] { "Table ID:", "[Select]" };
        int choice = this.Navigate("Select a table to dismiss:", options);
        bool filled = this.AreQuestionsFilled(options);
        if (filled == false)
        {
            Utils.Debug("The user has not filled in all the required input fields yet.");
            Display();
            return;
        }
        (bool, string) valid = AreValidInputs(options);
        if (valid.Item1 == false)
        {
            Utils.Debug(valid.Item2);
            Display(); return;
        }
        int tableId = int.Parse(QuestionsAnswers["Table ID:"]);
        double totalPrice = OrderLogic.GetTotalPrice(tableId);
        Utils.Debug($"Total price: {totalPrice}");
        CustomerLogic.DismissCustomer(tableId);
        Utils.Debug("Customer successfully dismissed.");
        Router.GoBack();
    }

    public (bool, string) AreValidInputs(string[] options)
    {
        string errorMessage;
        int maxId = Data.Tables.Last().Id;
        if (!int.TryParse(QuestionsAnswers[options[0]], out int id) || (id < 1 || maxId < id) || Data.Tables.FirstOrDefault(x => x.Id == id)!.IsAvailable == true)
        {
            if (id < 1 || maxId < id) errorMessage = "Provided Id doesn't exists";
            else if (Data.Tables.FirstOrDefault(x => x.Id == id)!.IsAvailable == true) errorMessage = "Table doens't have a customer.";
            else errorMessage = "Id has to be a number.";
            return (false, errorMessage);
        }
        return ValueTuple.Create(true, "");
    }
}
