using System.Windows;
using System.Windows.Controls;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktop.ViewModels.Dictionary.ListView;
using MoriaDesktop.Views.Base;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Contacts;
public partial class EmployeeListView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public EmployeeListView(EmployeeListViewModel viewModel): this()
    {
        DataContext = viewModel;
    }

    public EmployeeListView()
    {
        InitializeComponent();
    }

    private void EmployeeDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }
    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as EmployeeListViewModel).OnLoaded();

        var vm = DataContext as EmployeeListViewModel;
        if (vm != null && vm.Permission_Username?.CanRead != true)
        {
            EmployeeDataGrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        if (vm != null && vm.Permission_FirstName?.CanRead != true)
        {
            EmployeeDataGrid.Columns[1].Visibility = Visibility.Collapsed;
        }

        if (vm != null && vm.Permission_LastName?.CanRead != true)
        {
            EmployeeDataGrid.Columns[2].Visibility = Visibility.Collapsed;
        }

        if (vm != null && vm.Permission_PhoneNumber?.CanRead != true)
        {
            EmployeeDataGrid.Columns[3].Visibility = Visibility.Collapsed;
        }
    }
}
