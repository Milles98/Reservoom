﻿using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Reservoom.DbContexts;
using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Services.ReservationConflictValidators;
using Reservoom.Services.ReservationCreators;
using Reservoom.Services.ReservationProviders;
using Reservoom.Stores;
using Reservoom.ViewModels;

namespace Reservoom;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private const string CONNECTION_STRING = "Data Source=reservoom.db";
    private readonly Hotel _hotel;
    private readonly NavigationStore _navigationStore;
    private readonly ReservoomDbContextFactory _reservoomDbContextFactory;

    public App()
    {
        _reservoomDbContextFactory = new ReservoomDbContextFactory(CONNECTION_STRING);

        IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservoomDbContextFactory);
        IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservoomDbContextFactory);
        IReservationConflictValidator reservationConflictValidator =
            new DatabaseReservationConflictValidator(_reservoomDbContextFactory);
        
        ReservationBook reservationBook =
            new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);
        
        _hotel = new Hotel("Milles Hotel", reservationBook);
        _navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
        using (ReservoomDbContext dbContext = _reservoomDbContextFactory.CreateDbContext())
        {
            dbContext.Database.Migrate();
        }


        _navigationStore.CurrentViewModel = CreateReservationViewModel();

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }

    private MakeReservationViewModel CreateMakeReservationViewModel()
    {
        return new MakeReservationViewModel(_hotel,
            new NavigationService(_navigationStore, CreateReservationViewModel));
    }

    private ReservationListingViewModel CreateReservationViewModel()
    {
        return ReservationListingViewModel.LoadViewModel(_hotel,
            new NavigationService(_navigationStore, CreateMakeReservationViewModel));
    }
}