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
        Restaurant restaurant = new Restaurant("Andre", openTime, closeTime, twoPT, fourPT, sixPT, barChairs);
        Data.RestaurantAccess.WriteItem(restaurant);
    }
}
