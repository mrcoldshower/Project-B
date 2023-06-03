namespace Library;
public class CreateAccountPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        Navigate("Create An Account:", new string[] { "Name", "Email", "Password", "[Create]" });
        if (QuestionsAnswers.ContainsKey("Name") && QuestionsAnswers.ContainsKey("Email") && QuestionsAnswers.ContainsKey("Password"))
        {
            BankAccount bankAccount = CreateAccount(QuestionsAnswers["Name"], QuestionsAnswers["Email"], QuestionsAnswers["Password"]);

            Router.CurrentAccount = bankAccount;
            Router.ChangePage(new AccountPage());
            Router.ViewCurrentPage();
        }
        else
        {
            // User didn't type everything in. Try again
            Console.WriteLine("Debug: Not all queries were filled in");
            Console.ReadKey(true);
            Display();
        }
    }

    public override Page ChoosePage(int input)
    {
        return null!;
    }

    public BankAccount CreateAccount(string name, string email, string password)
    {
        List<BankAccount> bankAccounts = Data.BankAccounts;

        int nextId;
        if (bankAccounts.Count == 0 || bankAccounts == null) nextId = 1;
        else nextId = bankAccounts[bankAccounts.Count - 1].Id + 1;

        BankAccount bankAccount = new BankAccount(nextId, name, email, password);

        if (bankAccounts == null)
        {
            Data.BankAccountAccess.WriteAll(new List<BankAccount>() { bankAccount });
        }
        else
        {
            bankAccounts.Add(bankAccount);
            Data.BankAccountAccess.WriteAll(bankAccounts);
        }
        return bankAccount;
    }
}