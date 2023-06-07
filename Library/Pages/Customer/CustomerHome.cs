namespace Library;
public class CustomerHomePage : Page
{
    public override void Display()
    {
        string[] options = { "Make Reservation", "Menu Card", "About", "Exit" };
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
            case 0: page = new MakeReservationPage(); break;
            case 1: page = new SeeMenuCardPage(); break;
            case 2: page = new AboutPage(); break;
            case 3: Utils.ExitApplication(); break;
        }
        return page;
    }
}