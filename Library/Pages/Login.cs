namespace Library;
public class LoginPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        Navigate("Log into your account:", new string[] { "Email", "Password", "[Login]" });
        BankAccount? bankAccount = FindBankAccount(QuestionsAnswers["Email"], QuestionsAnswers["Password"]);
        bool wasLoginSuccessfull = ChangeCurrentAccount(bankAccount);
        if (wasLoginSuccessfull)
        {
            Router.ChangePage(new AccountPage());
            Router.ViewCurrentPage();
        }
        else
        {
            // User typed in wrong email and/or password. Try again
            Console.WriteLine("Debug: Wrong input");
            Console.ReadKey(true);
            Display();
        }
    }

    public override Page ChoosePage(int input)
    {
        return null!;
    }

    public static BankAccount? FindBankAccount(string email, string password)
    {
        BankAccount? bankAccount = Data.BankAccounts.Find(x => x.Email == email && x.Password == password);
        return bankAccount;
    }

    public static bool ChangeCurrentAccount(BankAccount? bankAccount)
    {
        if (bankAccount == null) return false;
        Router.CurrentAccount = bankAccount;
        return true;
    }
}