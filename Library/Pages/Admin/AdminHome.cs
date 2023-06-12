namespace Library;
public class AdminHomePage : Page
{
    public override void Display()
    {
        string[] options = { "Change Menu Card", "Change Restaurant Info", "Exit" };
        int choice = Navigate("Admin page!", options, "<< ", " >>");
        Page page = ChoosePage(choice);
        Router.PushPage(page);
        Router.ViewCurrentPage();
    }

    public override Page ChoosePage(int input)
    {
        Page page = null!;
        switch (input)
        {
            case 0: page = new ChangeMenuCardPage(); break;
            case 1: page = new ChangeRestaurantInfoPage(); break;
            case 2: Utils.ExitApplication(); break;
        }
        return page;
    }
}