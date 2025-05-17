using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.ListView;

namespace MoriaDesktop.Views.Dictionary.ListView;

public partial class MotorGearListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => MotorGearDataGrid;

    public MotorGearListView(MotorGearListViewModel listViewModel) : this()
    {
        DataContext = listViewModel;
    }

    public MotorGearListView()
    {
        InitializeComponent();
    }


    private void MotorGearDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }
    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as MotorGearListViewModel).OnLoaded();

        var vm = DataContext as MotorGearListViewModel;
        if (vm != null && !vm.Permission_Symbol.CanRead)
        {
            MotorGearDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Name.CanRead)
        {
            MotorGearDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Ratio.CanRead)
        {
            MotorGearDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        await (this as IListViewModelContent).SetColumnsOrder();
    }

    private async void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        await(this as IListViewModelContent).SaveColumnsOrder();

    }
}
