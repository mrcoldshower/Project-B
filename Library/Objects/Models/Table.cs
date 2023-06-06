using System.Text.Json.Serialization;

namespace Library;
public class Table : IHasID
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("isAvailable")]
    public bool IsAvailable { get; set; }

    public Table(int id, int type)
    {
        Id = id;
        Type = type;
        IsAvailable = true;
    }
}