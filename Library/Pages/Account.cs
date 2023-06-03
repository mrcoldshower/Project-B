namespace Library;
public class AccountPage : Page
{
    public override void Display()
    {
        BankAccount acc = Router.CurrentAccount!;
        string prompt = $"Your Bank Account:\nName: {acc.Name}\nEmail: {acc.Email}\nBalance: {acc.Balance}\n";
        int choice = Navigate(prompt, new string[] { "Withdraw", "Deposit" });
        SubPage page = ChoosePage(choice);
        Router.PushPage(page);
        Router.ViewCurrentPage();
        Handle(choice, page);
    }

    public override SubPage ChoosePage(int input)
    {
        SubPage page = null!;
        switch (input)
        {
            case 0: page = new SubPage("Enter an amount to withdraw:", new string[] { "Amount", "[Confirm]" }); break;
            case 1: page = new SubPage("Enter an amount to deposit:", new string[] { "Amount", "[Confirm]" }); break;
        }
        return page;
    }

    public void Handle(int choice, SubPage page)
    {
        int answer;
        bool isNumeric = int.TryParse(page.QuestionsAnswers["Amount"], out answer);

        if (isNumeric == false || page.QuestionsAnswers.ContainsKey("Amount") == false || answer <= 0) // checks if input is wrong
        {
            Console.WriteLine("Debug: Wrong input.");
            Console.ReadKey(true);
            Router.PushPage(page);
            Router.ViewCurrentPage();
            Handle(choice, page);
            return;
        }
        if (choice == 0 && page.QuestionsAnswers.ContainsKey("Amount"))
        {
            Router.CurrentAccount!.Withdraw(answer);
        }
        else if (choice == 1 && page.QuestionsAnswers.ContainsKey("Amount"))
        {
            Router.CurrentAccount!.Deposit(answer);
        }
        UpdateItem(Router.CurrentAccount!);
        Display();
    }

    public void UpdateList()
    {
        UpdateItem(Router.CurrentAccount!);
    }

    public void UpdateItem(BankAccount newItem)
    {
        List<BankAccount> list = Data.BankAccountAccess.LoadAll();
        BankAccount bankAccount = list.Find(x => x.Id == Router.CurrentAccount!.Id)!;
        int index = list.IndexOf(bankAccount);

        if (index != -1)
        {
            list[index] = newItem;
            Data.BankAccountAccess.WriteAll(list);
        }
        else
        {
            throw new InvalidOperationException("Item not found in the list.");
        }
    }
}