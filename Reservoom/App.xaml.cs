using System.Configuration;
using System.Data;
using System.Windows;
using Reservoom.Exceptions;
using Reservoom.Models;

namespace Reservoom;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Hotel hotel = new Hotel("Milles Rum");

        try
        {
            hotel.MakeReservation(new Reservation(
                new RoomID(1, 3),
                "Milles",
                new DateTime(2000, 1, 1),
                new DateTime(2000, 1, 1)));
        
            hotel.MakeReservation(new Reservation(
                new RoomID(1, 3),
                "Milles",
                new DateTime(2000, 1, 3),
                new DateTime(2000, 1, 4)));
        }
        catch (ReservationConflictException ex)
        {
            
        }

        IEnumerable<Reservation> reservations = hotel.GetReservationsForUser("Milles");
        
        base.OnStartup(e);
    }
}