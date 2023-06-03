namespace Library;
public static class Data
{
    public static JsonAccess<BankAccount> BankAccountAccess = new JsonAccess<BankAccount>(@"\Data\bankAccounts.json");
    public static List<BankAccount> BankAccounts = new JsonAccess<BankAccount>(@"\Data\bankAccounts.json").LoadAll();
}