namespace Library;

public class MakeReservationPage : Page
{
    public override bool IsQuestionPage { get; set; } = true;
    public override void Display()
    {
        int choice = Navigate("Make a reservation:", new string[] { "Quantity people:", "Date:", "Time:", "Name:", "Phone Number:", "E-mailaddress:", "[Make Reservation]" }, "", "");
        // SubPage page = ChoosePage(choice);
        // Router.PushPage(page);
        // Router.ViewCurrentPage();
        Utils.Incomplete();
    }

    public override SubPage ChoosePage(int input)
    {
        SubPage page = null!;
        switch (input)
        {
            case 0: page = new SubPage("Quantity People:", new string[] { "Amount", "[Confirm]" }); break;
            case 1: page = new SubPage("Enter an amount to deposit:", new string[] { "Amount", "[Confirm]" }); break;
        }
        return page;
    }
}