using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.DriveComponents;

namespace MoriaDesktop.Views.DriveComponents;

public partial class ExternalCoolingListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => ColorDataGrid;
    public ExternalCoolingListView(ExternalCoolingListViewModel externalCoolingListView)
    {
        InitializeComponent();
        DataContext = externalCoolingListView;
    }

    private void ColorDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await (DataContext as ExternalCoolingListViewModel).OnLoaded();

        var vm = DataContext as ExternalCoolingListViewModel;
        if (vm != null && !vm.Permission_Type.CanRead)
        {
            ColorDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }
        if (vm != null && !vm.Permission_Power.CanRead)
        {
            ColorDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }


        await (this as IListViewModelContent).SetColumnsOrder();
    }

    private async void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await (this as IListViewModelContent).SaveColumnsOrder();

    }
}
