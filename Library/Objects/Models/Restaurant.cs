using System.Text.Json.Serialization;

namespace Library;
public class Restaurant : IHasName
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("openTime")]
    public TimeOnly OpenTime { get; set; }

    [JsonPropertyName("closeTime")]
    public TimeOnly CloseTime { get; set; }

    [JsonPropertyName("twoPersonTables")]
    public int TwoPersonTables { get; set; }

    [JsonPropertyName("fourPersonTables")]
    public int FourPersonTables { get; set; }

    [JsonPropertyName("sixPersonTables")]
    public int SixPersonTables { get; set; }

    [JsonPropertyName("barChairs")]
    public int BarChairs { get; set; }



    public Restaurant(string name, TimeOnly openTime, TimeOnly closeTime, int twoPersonTables, int fourPersonTables, int sixPersonTables, int barChairs)
    {
        Name = name;
        OpenTime = openTime;
        CloseTime = closeTime;
        TwoPersonTables = twoPersonTables;
        FourPersonTables = fourPersonTables;
        SixPersonTables = sixPersonTables;
        BarChairs = barChairs;
    }

    public override string ToString()
    {
        return $"Name: {Name}\nOpen: {OpenTime} - {CloseTime}\n2-person tables: {TwoPersonTables}\n4-person tables: {FourPersonTables}\n6-person tables: {SixPersonTables}\nBar chairs: {BarChairs} ";
    }
}
