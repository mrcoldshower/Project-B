namespace Library;
public class ChooseTablePage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public Reservation searchedReservation { get; set; }
    public Dictionary<Reservation, List<int>>? Tables { get; set; }
    public List<int> TableIds { get; set; }

    public ChooseTablePage(Reservation reservation, Dictionary<Reservation, List<int>> tables)
    {
        searchedReservation = reservation;
        Tables = tables;
        TableIds = new();
    }

    public override void Display()
    {
        List<int> TypeTables = Tables![searchedReservation];
        (string, List<Table>) promptAndTables = DisplayAvailableTables(TypeTables);
        string prompt = promptAndTables.Item1;
        List<Table> possibleChoices = promptAndTables.Item2;
        string[] options = new string[TypeTables.Count + 1];
        for (int i = 0; i < TypeTables.Count; i++)
        {
            options[i] = $"Table ID ({TypeTables[i]} person table):";
        }
        options[TypeTables.Count] = "[Enter]";
        int choice = Navigate(prompt, options);
        bool filled = AreQuestionsFilled(options);
        if (filled == false)
        {
            Utils.Debug("The user has not filled in all the required input fields yet."); Display(); return;
        }
        int validCounter = 0;
        for (int i = 0; i < TypeTables.Count; i++)
        {
            if (!int.TryParse(QuestionsAnswers[$"Table ID ({TypeTables[i]} person table):"], out int id))
            {
                Utils.Debug("Error, Id has to be a number."); Display(); return;
            }
            else if (!possibleChoices.Select(x => x.Id).Contains(id))
            {
                Utils.Debug("Error, table id is not in the options."); Display(); return;
            }
            else if (TableIds.Contains(id))
            {
                Utils.Debug("Error, you can't choose a table more than once."); Display(); return;
            }
            else if (!TablesLogic.TypeIdCheck(TypeTables[i], id))
            {
                Utils.Debug($"Error, table with that id isn't a table with {TypeTables[i]} seats."); Display(); return;
            }
            else
            {
                validCounter++;
            }
        }
        if (validCounter == TypeTables.Count)
        {
            for (int i = 0; i < TypeTables.Count; i++)
            {
                TableIds.Add(int.Parse(QuestionsAnswers[$"Table ID ({TypeTables[i]} person table):"]));
            }
        }
    }

    public (string, List<Table>) DisplayAvailableTables(List<int> typeTableList)
    {
        string s = "";
        List<Table> tables = Data.Tables;
        List<Table> selectedTables = new List<Table>();
        for (int i = 0; i < typeTableList.Count; i++)
        {
            for (int j = 0; j < tables.Count; j++)
            {
                if (tables[j].Type == typeTableList[i] && tables[j].IsAvailable)
                {
                    if (!selectedTables.Contains(tables[j])) selectedTables.Add(tables[j]); // checks for duplicates
                }
            }
        }
        s += "Choose a table for the customer:\n";
        for (int i = 0; i < selectedTables.Count; i++)
        {
            if (i > 0) if (selectedTables[i - 1].Type != selectedTables[i].Type) s += "\n"; // puts an empty line inbetween types for more readability
            s += $"Table ID: {selectedTables[i].Id}, Table type: {selectedTables[i].Type} person table\n";
        }
        return (s, selectedTables);
    }

    public static List<int> DisplayAndChooseAvailableTables(List<int> typeTableList) // Displays and chooses available tables selected on typetables.
    {
        List<Table> tables = Data.Tables;
        List<Table> selectedTables = new List<Table>();
        List<int> chosenTableIds = new List<int>();
        for (int i = 0; i < typeTableList.Count; i++)
        {
            for (int j = 0; j < tables.Count; j++)
            {
                if (tables[j].Type == typeTableList[i] && tables[j].IsAvailable)
                {
                    if (!selectedTables.Contains(tables[j])) selectedTables.Add(tables[j]); // checks for duplicates
                }
            }
        }
        Console.WriteLine("Choose a table for the customer:\n");
        for (int i = 0; i < selectedTables.Count; i++)
        {
            if (i > 0) if (selectedTables[i - 1].Type != selectedTables[i].Type) Console.WriteLine(); // puts an empty line inbetween types for more readability
            Console.WriteLine($"Table ID: {selectedTables[i].Id}, Table type: {selectedTables[i].Type} person table, Status: Available");
        }
        for (int i = 0; i < typeTableList.Count; i++)
        {
            if (typeTableList.Count == 1) Console.WriteLine("Enter a table id: ");
            else Console.WriteLine($"Enter a table id ({i + 1}/{typeTableList.Count}): ");
            bool valid = false;
            while (!valid)
            {
                int tableId = Input.CheckIfInputIsInt();
                for (int j = 0; j < selectedTables.Count; j++)
                {                                                           // bug: user is able to choose one type of table multiple times even though he selected different types of tables
                    if (selectedTables[j].Id == tableId) valid = true; // bug: if table gets chosen 2 times, it will continue, it should not allow that
                }
                if (!valid) Console.WriteLine("Error, you chose an invalid table id.");
                else chosenTableIds.Add(tableId);
            }
        }
        return chosenTableIds;
    }
}
