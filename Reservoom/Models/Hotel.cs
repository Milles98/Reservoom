namespace Reservoom.Models;

public class Hotel
{
    private readonly ReservationBook _reservationBook;
    
    public string Name { get; }

    public Hotel(string name)
    {
        Name = name;
        _reservationBook = new ReservationBook();
    }
}