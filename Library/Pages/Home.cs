namespace Library;
public class HomePage : Page
{
    public override void Display()
    {
        string[] options = { "Login", "Create Account", "About", "Exit" };
        int choice = Navigate("Home page!", options);

        Page page = ChoosePage(choice);
        Router.PushPage(page);
        Router.ViewCurrentPage();
    }

    public override Page ChoosePage(int input)
    {
        Page page = null!;
        switch (input)
        {
            case 0: page = new LoginPage(); break;
            case 1: page = new CreateAccountPage(); break;
            case 2: page = new AboutPage(); break;
            case 3: Utils.ExitApplication(); break;
        }
        return page;
    }
}