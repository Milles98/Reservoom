using System.Windows.Input;
using Reservoom.Commands;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Stores;

namespace Reservoom.ViewModels;

public class MakeReservationViewModel : ViewModelBase
{
    private readonly Func<ViewModelBase> _createViewModel;
    private string _username;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    private int _roomNumber;

    public int RoomNumber
    {
        get => _roomNumber;
        set
        {
            _roomNumber = value;
            OnPropertyChanged(nameof(RoomNumber));
        }
    }

    private int _floorNumber;

    public int FloorNumber
    {
        get => _floorNumber;
        set
        {
            _floorNumber = value;
            OnPropertyChanged(nameof(FloorNumber));
        }
    }

    private DateTime _startDate = new DateTime(2024, 6, 11);

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            OnPropertyChanged(nameof(StartDate));
        }
    }

    private DateTime _endDate = new DateTime(2024, 6, 18);

    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }

    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }

    public MakeReservationViewModel(Hotel hotel, NavigationService reservationViewNavigationService)
    {
        SubmitCommand = new MakeReservationCommand(this, hotel, reservationViewNavigationService);
        CancelCommand = new NavigateCommand(reservationViewNavigationService);
    }
}