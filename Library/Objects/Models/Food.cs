using System.Text.Json.Serialization;

namespace Library;
public class Food : IHasID, IHasName
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("category")]
    public FoodCategory Category { get; set; }

    public Food(int id, string name, double price, string description, FoodCategory category)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        Category = category;
    }

    public override string ToString()
    {
        return $"ID: {Id}\nDish name: {Name}\nPrice: ${Price}\nDescription: {Description}\nCategory: {Category}";
    }
}