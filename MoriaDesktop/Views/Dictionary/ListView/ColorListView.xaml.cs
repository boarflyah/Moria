using System.Windows.Controls;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktop.ViewModels.Dictionary.ListView;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary.ListView;

public partial class ColorListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => ColorDataGrid;

    public ColorListView(ColorListViewModel viewModel )
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void ColorDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await (DataContext as ColorListViewModel).OnLoaded();

        var vm = DataContext as ColorListViewModel;
        if (vm != null && !vm.Permission_Name.CanRead)
        {
            ColorDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Code.CanRead)
        {
            ColorDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        await (this as IListViewModelContent).SetColumnsOrder();
    }

    private async void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await(this as IListViewModelContent).SaveColumnsOrder();

    }
}
