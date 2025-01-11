using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktopServices.Interfaces.ViewModels;
using System.Windows;
using System.Windows.Controls;

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

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as EmployeeListViewModel).OnLoaded();
    }

    private void EmployeeDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }
}
