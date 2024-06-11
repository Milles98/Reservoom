using System.Collections.ObjectModel;
using System.Windows.Input;
using Reservoom.Commands;
using Reservoom.Models;

namespace Reservoom.ViewModels;

public class ReservationListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<ReservationViewModel> _reservations;

    public IEnumerable<ReservationViewModel> Reservations => _reservations;
    public ICommand MakeReservationCommand { get; }

    public ReservationListingViewModel()
    {
        _reservations = new ObservableCollection<ReservationViewModel>();

        MakeReservationCommand = new NavigateCommand();
        
        _reservations.Add(new ReservationViewModel(new Reservation(new RoomID(1, 2), "Mille", DateTime.Now, DateTime.Now)));
        _reservations.Add(new ReservationViewModel(new Reservation(new RoomID(3, 2), "Wilma", DateTime.Now, DateTime.Now)));
        _reservations.Add(new ReservationViewModel(new Reservation(new RoomID(2, 4), "Lexie", DateTime.Now, DateTime.Now)));
    }
}