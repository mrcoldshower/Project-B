using System.Text.Json.Serialization;

namespace Library;
public class Reservation : IHasID, IHasName, IEquatable<Reservation>
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("quantityPeople")]
    public int QuantityPeople { get; set; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("time")]
    public TimeOnly Time { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("reservationCode")]
    public string ReservationCode { get; set; }



    public Reservation(int id, int quantityPeople, DateOnly date, TimeOnly time, string name, string phoneNumber, string email, string reservationCode)
    {
        Id = id;
        QuantityPeople = quantityPeople;
        Date = date;
        Time = time;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        ReservationCode = reservationCode;
    }

    public override string ToString()
    {
        return $"ID: {Id}, QuantityPeople: {QuantityPeople}, Date: {Date}, Time: {Time}, CustomerName: {Name}, ReservationCode: {ReservationCode}";
    }

    public bool Equals(Reservation? r)
    {
        if (r == null) return false;
        return Id == r.Id && QuantityPeople == r.QuantityPeople && Date == r.Date
            && Time == r.Time && Name == r.Name && PhoneNumber == r.PhoneNumber
            && Email == r.Email && ReservationCode == r.ReservationCode;
    }
}
