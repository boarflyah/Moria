using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.ListView;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Products;

public partial class CategoryListView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public CategoryListView(CategoryListViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void CategoryDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is DataGrid dg)
        {
            (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
        }
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as BaseListViewModel).OnLoaded();

        var vm = DataContext as CategoryListViewModel;
        if (vm != null && !vm.Permission_Name.CanRead)
        {
            CategoryDataGrid.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
