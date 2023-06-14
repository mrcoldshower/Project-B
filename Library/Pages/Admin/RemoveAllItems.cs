namespace Library;

public class RemoveAllItems : Page
{
    public override void Display()
    {
        string[] options = {
            "[Confirm]"
        };
        int choice = Navigate("Are you sure that you want to delete ALL items from the menu?", options, "", "");

        if (DeleteAllItems())
        {
            Utils.Debug("All items have been deleted.");
            Router.GoBack();
        }
        else
        {
            Utils.Debug("An unknown error has occured. Please try again.");
            Router.GoBack();
        }
    }

    private bool DeleteAllItems()
    {
        Data.FoodAccess.WriteAll(new List<Food>());
        Data.Foods = Data.FoodAccess.LoadAll();
        return true;
    }
}