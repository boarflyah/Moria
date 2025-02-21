using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.DriveComponents;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.DriveComponents;

public partial class DriveListView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public DriveListView(DriveListViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void DriveDatagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as BaseListViewModel).OnLoaded();

        var vm = DataContext as DriveListViewModel;
        if (vm != null && !vm.Permission_Motor.CanRead)
        {
            DriveDatagrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Quantity.CanRead)
        {
            DriveDatagrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Inverter.CanRead)
        {
            DriveDatagrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Variator.CanRead)
        {
            DriveDatagrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
