using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.Interfaces;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.ListView;

namespace MoriaDesktop.Views.Dictionary.ListView;

public partial class ContactListView : Page, IListViewModelContent
{
    public object GetViewModel() => DataContext;
    public DataGrid DataGrid => ContactDataGrid;

    public ContactListView(ContactListViewModel listViewModel) : this()
    {
        DataContext = listViewModel;
    }

    public ContactListView()
    {
        InitializeComponent();
    }

    private void ContactDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as ContactListViewModel).OnLoaded();

        var vm = DataContext as ContactListViewModel;
        if (vm != null && !vm.Permission_ShortName.CanRead)
        {
            ContactDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_LongName.CanRead)
        {
            ContactDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        if (vm != null && !vm.Permission_Symbol.CanRead)
        {
            ContactDataGrid.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
        }

        await (this as IListViewModelContent).SetColumnsOrder();
    }

    private async void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        await (this as IListViewModelContent).SaveColumnsOrder();
    }
}
