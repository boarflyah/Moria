using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.ListView;

namespace MoriaDesktop.Views.Dictionary.ListView;

public partial class MotorListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => MotorDataGrid;

    public MotorListView(MotorListViewModel listViewModel) : this()
    {
        DataContext = listViewModel;
    }
    public MotorListView()
    {
        InitializeComponent();
    }

    private void MotorDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as MotorListViewModel).OnLoaded();

        var vm = DataContext as MotorListViewModel;
        if (vm != null && !vm.Permission_Name.CanRead)
        {
            MotorDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Symbol.CanRead)
        {
            MotorDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Power.CanRead)
        {
            MotorDataGrid.Columns[2].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !(vm.Permission_RPM?.CanRead == true))
        {
            MotorDataGrid.Columns[3].Visibility = System.Windows.Visibility.Collapsed;
        }

        await (this as IListViewModelContent).SetColumnsOrder();
    }

    private async void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        await (this as IListViewModelContent).SaveColumnsOrder();
    }
}
