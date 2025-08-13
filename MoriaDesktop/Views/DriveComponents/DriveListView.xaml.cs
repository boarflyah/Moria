using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.DriveComponents;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.Views.DriveComponents;

public partial class DriveListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => DriveDatagrid;

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
        if (vm != null && !vm.Permission_Name?.CanRead == true)
        {
            var column = DriveDatagrid.Columns.FirstOrDefault(x => x.SortMemberPath.Equals(nameof(DriveDo.Name)));
            if (column != null)
                column.Visibility = Visibility.Collapsed;
            //DriveDatagrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Motor?.CanRead == true)
        {
            DriveDatagrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Quantity?.CanRead == true)
        {
            DriveDatagrid.Columns[2].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Inverter?.CanRead == true)
        {
            DriveDatagrid.Columns[3].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Variator?.CanRead == true)
        {
            DriveDatagrid.Columns[4].Visibility = System.Windows.Visibility.Collapsed;
        }
        await (this as IListViewModelContent).SetColumnsOrder();
    }

    private async void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        await(this as IListViewModelContent).SaveColumnsOrder();
    }
}
