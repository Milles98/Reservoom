﻿using System.ComponentModel;
using System.Windows;
using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.ViewModels;

namespace Reservoom.Commands;

public class MakeReservationCommand : CommandBase
{
    private readonly MakeReservationViewModel _makeReservationViewModel;
    private readonly Hotel _hotel;

    public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, Hotel hotel)
    {
        _makeReservationViewModel = makeReservationViewModel;
        _hotel = hotel;

        _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_makeReservationViewModel.Username) &&
               _makeReservationViewModel.FloorNumber > 0 &&
               _makeReservationViewModel.RoomNumber > 0 &&
               base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        Reservation reservation = new Reservation(
            new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
            _makeReservationViewModel.Username,
            _makeReservationViewModel.StartDate,
            _makeReservationViewModel.EndDate);

        try
        {
            _hotel.MakeReservation(reservation);

            MessageBox.Show("Successfully reserved room", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (ReservationConflictException)
        {
            MessageBox.Show("Room already taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MakeReservationViewModel.Username) ||
            e.PropertyName == nameof(MakeReservationViewModel.FloorNumber) ||
            e.PropertyName == nameof(MakeReservationViewModel.RoomNumber))
        {
            OnCanExecutedChanged();
        }
    }
}