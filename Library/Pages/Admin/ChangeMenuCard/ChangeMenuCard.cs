namespace Library;
public class ChangeMenuCardPage : Page
{
    public override void Display()
    {
        string[] options = {
            "Add an item",
            "Delete an item",
            "Change an item",
            "Remove all items"
        };
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
            case 0: page = new AddItemPage(); break;
            case 1: page = new RemoveItem(); break;
            case 2: page = new ChangeItemPage(); break;
            case 3: page = new RemoveAllItems(); break;
        }
        return page;
    }
}