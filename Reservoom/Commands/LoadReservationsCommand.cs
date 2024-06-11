using System.Windows;
using Reservoom.Models;
using Reservoom.ViewModels;
using Reservoom.Views;

namespace Reservoom.Commands;

public class LoadReservationsCommand : AsyncCommandBase
{
    private readonly Hotel _hotel;
    private readonly ReservationListingViewModel _viewModel;

    public LoadReservationsCommand(ReservationListingViewModel viewModel, Hotel hotel)
    {
        _viewModel = viewModel;
        _hotel = hotel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        try
        {
            IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

            _viewModel.UpdateReservations(reservations);
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to load reservations", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}