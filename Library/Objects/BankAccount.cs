public class BankAccount : Account
{
    public override int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    private string _password = "";
    public string Password { get { return _password; } set { _password = value; } }

    protected int _balance;
    public int Balance { get { return _balance; } set { _balance = value; } }

    public BankAccount(int id, string name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        _balance = 0;
    }

    public bool Deposit(int amount)
    {
        if (amount < 0) return false;
        _balance += amount;
        return true;
    }

    public bool Withdraw(int amount)
    {
        if (amount < 0 || _balance - amount < 0) return false;
        _balance -= amount;
        return true;
    }

    public override string ToString()
    {
        return $"({Id}) Name: {Name}, Email: {Email}, Balance: ${_balance}";
    }
}