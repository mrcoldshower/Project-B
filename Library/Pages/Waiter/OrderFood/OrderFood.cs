namespace Library;
public class OrderFoodPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;

    public override void Display()
    {
        string[] options = new string[] { "Table ID:", "[Select]" };
        int choice = this.Navigate("Select a table for ordering:", options);
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
        List<Food> chosenFood = new();
        Console.CursorVisible = true;
        while (true)
        {
            Food dish = (Food)SearchLogic.SearchItem(Data.Foods);
            if (dish == null) return;
            chosenFood.Add(dish);
            Console.Write("Do you want to order more? y/n: ");
            string input = "";
            while (input != "y" && input != "n")
            {
                input = Console.ReadLine() ?? "".TrimEnd().ToLower();
                if (input != "y" && input != "n") Console.WriteLine("Error, please enter 'y' or 'n'");
            }
            if (input == "n") break;
        }
        OrderLogic.PlaceOrder(chosenFood, int.Parse(QuestionsAnswers["Table ID:"]));
        Utils.Debug("Order has been placed");
        Console.CursorVisible = false;
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