using System.Collections.ObjectModel;
using System.Windows.Input;
using Reservoom.Commands;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Stores;

namespace Reservoom.ViewModels;

public class ReservationListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<ReservationViewModel> _reservations;
    private readonly Hotel _hotel;

    public IEnumerable<ReservationViewModel> Reservations => _reservations;
    public ICommand MakeReservationCommand { get; }

    public ReservationListingViewModel(Hotel hotel, NavigationService makeReservationNavigationService)
    {
        _hotel = hotel;
        _reservations = new ObservableCollection<ReservationViewModel>();

        MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);

        UpdateReservations();
    }

    private void UpdateReservations()
    {
        _reservations.Clear();

        foreach (Reservation reservation in _hotel.GetAllReservations())
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }
    }
}