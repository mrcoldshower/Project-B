using System.Text.Json.Serialization;

namespace Library;
public class Order : IHasID
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("foodList")]
    public List<Food> FoodList { get; set; }

    [JsonPropertyName("totalPrice")]
    private double _totalPrice;
    public double TotalPrice { get { return _totalPrice; } set { _totalPrice = FoodList.Select(x => x.Price).Sum(); } }

    public Order(int id, List<Food> foodList)
    {
        Id = id;
        FoodList = foodList;
    }
}