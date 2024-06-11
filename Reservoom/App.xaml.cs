using System.Configuration;
using System.Data;
using System.Windows;
using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.ViewModels;

namespace Reservoom;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly Hotel _hotel;

    public App()
    {
        _hotel = new Hotel("Milles Hotel");
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_hotel)
        };
        MainWindow.Show();
        
        base.OnStartup(e);
    }
}