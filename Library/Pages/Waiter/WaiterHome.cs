namespace Library;
public class WaiterHomePage : Page
{
    public override void Display()
    {
        string[] options = { "Seat a Customer", "Order Food", "Dismiss a Customer", "Exit" };
        int choice = Navigate("Waiter page!", options, "<< ", " >>");
        Page page = ChoosePage(choice);
        Router.PushPage(page);
        Router.ViewCurrentPage();
    }

    public override Page ChoosePage(int input)
    {
        Page page = null!;
        switch (input)
        {
            case 0: page = new SeatCustomerPage(); break;
            case 1: page = new OrderFoodPage(); break;
            case 2: page = new DismissCustomerPage(); break;
            case 3: Utils.ExitApplication(); break;
        }
        return page;
    }
}