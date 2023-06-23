namespace Library;

public static class ReservationLogic
{
    public static Reservation CreateReservation(int quantityPeople, DateOnly date, TimeOnly time, string name, string phoneNumber, string email)
    {
        return new Reservation(GetNextId(), quantityPeople, date, time, name, phoneNumber, email, CreateReservationCode());
    }

    private static int GetNextId()
    {
        List<Reservation> reservationsList = Data.Reservations;
        int nextId;
        if (reservationsList.Count == 0) nextId = 1;
        else nextId = reservationsList[reservationsList.Count - 1].Id + 1;
        return nextId;
    }

    public static string CreateReservationCode()
    {
        Random random = new Random();
        int lengthOfReservationCode = 5;
        char[] keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890".ToCharArray();

        List<Reservation> reservations = Data.Reservations;
        List<string> allResCodes = new List<string>();
        foreach (Reservation reservation in reservations)
        {
            allResCodes.Add(reservation.ReservationCode);
        }
        while (true)
        {
            string resCode = "";
            for (int i = 0; i < lengthOfReservationCode; i++)
            {
                resCode += keys[random.Next(0, keys.Length - 1)];
            }
            if (!allResCodes.Contains(resCode)) return resCode;
        }
    }

    public static void AddReservation(Reservation reservation)
    {
        List<Reservation> reservations = Data.Reservations;
        reservations.Add(reservation);
        Data.ReservationAccess.WriteAll(reservations);
        Data.Reservations = Data.ReservationAccess.LoadAll();
    }

    public static void RemoveReservation(Reservation reservation)
    {
        List<Reservation> reservations = Data.ReservationAccess.LoadAll();
        reservations.Remove(reservation);
        Data.ReservationAccess.WriteAll(reservations);
        Data.Reservations = reservations;
    }

    public static List<Reservation> TodaysReservations()
    {
        return Data.Reservations.FindAll(x => x.Date == DateOnly.FromDateTime(DateTime.Now));
    }

    public static void FillTodayWithTestReservations(int reservationsQuantity, TimeOnly time)
    {
        Random random = new Random();
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);

        for (int i = 0; i < reservationsQuantity; i++)
        {
            AddReservation(CreateReservation(random.Next(1, 8), date, time, "test", "123", "test@test.com"));
        }
    }

    public static Reservation? FindReservation(Predicate<Reservation> predicate)
    {
        return Data.Reservations.Find(predicate);
    }

    public static void RemoveOldReservations()
    {
        List<Reservation> reservations = Data.Reservations;
        reservations = reservations.Where(x => x.Date > DateOnly.FromDateTime(DateTime.Now).AddMonths(-1)).ToList();
        Data.ReservationAccess.WriteAll(reservations);
        Data.Reservations = reservations;
    }
}