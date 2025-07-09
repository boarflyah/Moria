using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.DriveComponents;

namespace MoriaDesktop.Views.DriveComponents;

public partial class VariatorListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => ColorDataGrid;
    public VariatorListView(VariatorListViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
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
        await (DataContext as VariatorListViewModel).OnLoaded();

        var vm = DataContext as VariatorListViewModel;
        if (vm != null && !vm.Permission_Type.CanRead)
        {
            ColorDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }

        await (this as IListViewModelContent).SetColumnsOrder();
    }

    private async void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await (this as IListViewModelContent).SaveColumnsOrder();
    }
}
