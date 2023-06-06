namespace Library;

public static class AlgorithmLogic
{
    public static bool AlgorithmIsPossible(Reservation reservation)
    {
        List<Reservation> reservations = Data.Reservations.Where(x => x.Time == reservation.Time).ToList(); // Linq
        reservations.Add(reservation);
        reservations.Sort((x, y) => y.QuantityPeople.CompareTo(x.QuantityPeople));
        Restaurant restaurant = Data.Restaurant;
        int currentTwoPT = restaurant.TwoPersonTables;
        int currentFourPT = restaurant.FourPersonTables;
        int currentSixPT = restaurant.SixPersonTables;
        int currentBarChair = restaurant.BarChairs;
        int currentQuantity;
        int counter = 0;
        Dictionary<Reservation, List<int>> ReservationTables = new();
        for (int i = 0; i < reservations.Count; i++)
        {
            counter += reservations[i].QuantityPeople;
            ReservationTables.Add(reservations[i], new List<int>());
            currentQuantity = reservations[i].QuantityPeople;
            // Console.Write($"|{currentQuantity} : ");
            while (currentSixPT > 0 && currentQuantity >= 5)
            {
                if (currentSixPT * 6 + currentFourPT * 4 + currentTwoPT * 2 - currentQuantity < 0 && currentQuantity > 6) break;
                currentSixPT -= 1;
                if (currentQuantity == 5) currentQuantity -= 5;
                else currentQuantity -= 6;
                ReservationTables[reservations[i]].Add(6);
            }
            while (currentFourPT > 0 && currentQuantity >= 3)
            {
                if (currentFourPT * 4 + currentTwoPT * 2 - currentQuantity < 0 && currentQuantity > 4) break;
                currentFourPT -= 1;
                if (currentQuantity == 3) currentQuantity -= 3;
                else currentQuantity -= 4;
                ReservationTables[reservations[i]].Add(4);
            }
            while (currentTwoPT > 0 && currentQuantity >= 1 && !ReservationTables[reservations[i]].Contains(1)) // does have other tables
            {
                if (currentTwoPT * 2 - currentQuantity < 0 && currentQuantity > 2) break;
                currentTwoPT -= 1;
                if (currentQuantity == 1) currentQuantity -= 1;
                else currentQuantity -= 2;
                ReservationTables[reservations[i]].Add(2);
            }
            while (currentBarChair > 0 && currentQuantity >= 1 && !new[] { 2, 4, 6 }.Any(x => ReservationTables[reservations[i]].Contains(x))) // no other type tables, so can only choose barchairs type
            {
                if (currentBarChair - currentQuantity < 0) break;
                currentBarChair -= 1;
                currentQuantity -= 1;
                ReservationTables[reservations[i]].Add(1);
            }
            // Console.Write($"{currentQuantity}| ");
            // if (!ReservationTables[reservations[i]].Any()) Console.Write("-");
            // foreach (var item in ReservationTables[reservations[i]]) Console.Write($"{item}, ");
            // Console.WriteLine();
            if (currentQuantity != 0) return false;
        }
        // Console.WriteLine(counter);
        return true;
    }

    public static Dictionary<Reservation, List<int>>? AlgorithmReturnTables(Reservation reservation)
    {
        List<Reservation> reservations = Data.Reservations.Where(x => x.Time == reservation.Time).ToList(); // Linq
        reservations.Add(reservation);
        reservations.Sort((x, y) => y.QuantityPeople.CompareTo(x.QuantityPeople));
        Restaurant restaurant = Data.Restaurant;
        int currentTwoPT = restaurant.TwoPersonTables;
        int currentFourPT = restaurant.FourPersonTables;
        int currentSixPT = restaurant.SixPersonTables;
        int currentBarChair = restaurant.BarChairs;
        int currentQuantity;
        int counter = 0;
        Dictionary<Reservation, List<int>> ReservationTables = new();
        for (int i = 0; i < reservations.Count; i++)
        {
            counter += reservations[i].QuantityPeople;
            ReservationTables.Add(reservations[i], new List<int>());
            currentQuantity = reservations[i].QuantityPeople;
            while (currentSixPT > 0 && currentQuantity >= 5)
            {
                if (currentSixPT * 6 + currentFourPT * 4 + currentTwoPT * 2 - currentQuantity < 0 && currentQuantity > 6) break;
                currentSixPT -= 1;
                if (currentQuantity == 5) currentQuantity -= 5;
                else currentQuantity -= 6;
                ReservationTables[reservations[i]].Add(6);
            }
            while (currentFourPT > 0 && currentQuantity >= 3)
            {
                if (currentFourPT * 4 + currentTwoPT * 2 - currentQuantity < 0 && currentQuantity > 4) break;
                currentFourPT -= 1;
                if (currentQuantity == 3) currentQuantity -= 3;
                else currentQuantity -= 4;
                ReservationTables[reservations[i]].Add(4);
            }
            while (currentTwoPT > 0 && currentQuantity >= 1 && !ReservationTables[reservations[i]].Contains(1)) // does have other tables
            {
                if (currentTwoPT * 2 - currentQuantity < 0 && currentQuantity > 2) break;
                currentTwoPT -= 1;
                if (currentQuantity == 1) currentQuantity -= 1;
                else currentQuantity -= 2;
                ReservationTables[reservations[i]].Add(2);
            }
            while (currentBarChair > 0 && currentQuantity >= 1 && !new[] { 2, 4, 6 }.Any(x => ReservationTables[reservations[i]].Contains(x))) // no other type tables, so can only choose barchairs type
            {
                if (currentBarChair - currentQuantity < 0) break;
                currentBarChair -= 1;
                currentQuantity -= 1;
                ReservationTables[reservations[i]].Add(1);
            }
            if (currentQuantity != 0) return null;
        }
        return ReservationTables;
    }



    public static List<TimeOnly> AlgorithmSuggestions(Reservation reservation)
    {

        List<TimeOnly> existingBookings = ChooseTimePage.GetTimes().ToList();
        TimeOnly openingTime = Data.Restaurant.OpenTime;
        TimeOnly closingTime = Data.Restaurant.CloseTime;
        TimeOnly pickedTimeSlot = reservation.Time;

        // Find the closest available time slots
        List<TimeOnly> suggestions = new List<TimeOnly>();

        foreach (TimeOnly existingBooking in existingBookings)
        {
            if (suggestions.Count >= 4)
                break;

            TimeOnly previousSlot = existingBooking.AddHours(-1);
            TimeOnly nextSlot = existingBooking.AddHours(1);

            if (previousSlot >= openingTime && !suggestions.Contains(previousSlot) && previousSlot != reservation.Time && AlgorithmIsPossible(reservation))
                suggestions.Add(previousSlot);

            if (nextSlot <= closingTime && !suggestions.Contains(nextSlot) && nextSlot != reservation.Time && AlgorithmIsPossible(reservation))
                suggestions.Add(nextSlot);
        }

        suggestions.Sort();

        return suggestions;
    }

    public static List<int> GetTableIdsFromAlgo(Reservation reservation)
    {
        Dictionary<Reservation, List<int>>? dict = AlgorithmReturnTables(reservation);
        if (dict == null) throw new NullReferenceException("Its null");
        return dict[reservation];
    }
}