using System.Text.Json.Serialization;

namespace Library;
public class Order : IHasID
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("foodList")]
    public List<Food> FoodList { get; set; }

    [JsonPropertyName("totalPrice")]
    public double TotalPrice { get; set; }

    public Order(int id, List<Food> foodList)
    {
        Id = id;
        FoodList = foodList;
        TotalPrice = foodList.Select(x => x.Price).Sum();
    }

    public bool Equals(Order? o)
    {
        if (o == null) return false;
        return Id == o.Id && FoodList == o.FoodList && TotalPrice == o.TotalPrice;
    }
}