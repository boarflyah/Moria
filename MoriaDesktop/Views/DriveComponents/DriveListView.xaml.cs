using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.DriveComponents;
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

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as BaseListViewModel).OnLoaded();
    }

    private void DriveDatagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }
}
