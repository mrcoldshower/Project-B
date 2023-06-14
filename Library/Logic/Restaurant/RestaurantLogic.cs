namespace Library;

public class RestaurantLogic
{
    public static void InitializeRestaurant()
    {
        TimeOnly openTime = new TimeOnly(16, 0);
        TimeOnly closeTime = new TimeOnly(22, 0);
        int twoPT = 8; // TwoPersonTables
        int fourPT = 5;
        int sixPT = 2;
        int barChairs = 8;
        Restaurant restaurant = new Restaurant("Andre", openTime, closeTime, twoPT, fourPT, sixPT, barChairs, "Test");
        Data.RestaurantAccess.WriteItem(restaurant);
    }

    public static void ChangeRestaurant(string name, TimeOnly openTime, TimeOnly closeTime, int twoPT, int fourPT, int sixPT, int barChairs)
    {
        Restaurant restaurant = Data.Restaurant;
        restaurant.Name = name;
        restaurant.OpenTime = openTime;
        restaurant.CloseTime = closeTime;
        restaurant.TwoPersonTables = twoPT;
        restaurant.FourPersonTables = fourPT;
        restaurant.SixPersonTables = sixPT;
        restaurant.BarChairs = barChairs;
        Data.RestaurantAccess.WriteItem(restaurant);
        Data.Restaurant = restaurant;
    }

    public static void ChangeAboutDescription(string aboutDescription)
    {
        Restaurant restaurant = Data.Restaurant;
        restaurant.AboutDescription = aboutDescription;
        Data.RestaurantAccess.WriteItem(restaurant);
        Data.Restaurant = restaurant;
    }
}
