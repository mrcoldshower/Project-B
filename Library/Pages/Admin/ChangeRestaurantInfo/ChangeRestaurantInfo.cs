namespace Library;
public class ChangeRestaurantInfoPage : Page
{
    public override void Display()
    {
        string[] options = { "Change proporties", "Change about", };
        int choice = Navigate("Menu Options!", options, "<< ", " >>");
        Page page = ChoosePage(choice);
        Router.PushPage(page);
        Router.ViewCurrentPage();

    }

    public override Page ChoosePage(int input)
    {
        Page page = null!;
        switch (input)
        {
            case 0: page = new ChangeProportiesPage(); break;
            case 1: page = new ChangeAboutPage(); break;
        }
        return page;
    }
}