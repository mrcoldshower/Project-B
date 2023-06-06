namespace Library;
public static class Data
{
    public static JsonAccess<Reservation> ReservationAccess = new JsonAccess<Reservation>(@"\Data\reservations.json");
    public static List<Reservation> Reservations = new JsonAccess<Reservation>(@"\Data\reservations.json").LoadAll();

    public static JsonAccess<Table> TableAccess = new JsonAccess<Table>(@"\Data\Tables.json");
    public static List<Table> Tables = new JsonAccess<Table>(@"\Data\Tables.json").LoadAll();

    public static JsonAccess<Customer> CustomerAccess = new JsonAccess<Customer>(@"\Data\customers.json");
    public static List<Customer> Customers = new JsonAccess<Customer>(@"\Data\customers.json").LoadAll();

    public static JsonAccess<Food> FoodAccess = new JsonAccess<Food>(@"\Data\food.json");
    public static List<Food> Foods = new JsonAccess<Food>(@"\Data\food.json").LoadAll();

    public static JsonAccess<Order> OrderAccess = new JsonAccess<Order>(@"\Data\orders.json");
    public static List<Order> Orders = new JsonAccess<Order>(@"\Data\orders.json").LoadAll();

    public static JsonAccess<Restaurant> RestaurantAccess = new JsonAccess<Restaurant>(@"\Data\restaurant.json");
    public static Restaurant Restaurant = new JsonAccess<Restaurant>(@"\Data\restaurant.json").LoadItem();
}