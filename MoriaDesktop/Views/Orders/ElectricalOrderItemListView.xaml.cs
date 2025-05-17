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
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Orders;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Orders
{
    public partial class ElectricalOrderItemListView : Page, IViewModelContent
    {
        public object GetViewModel() => DataContext;

        public ElectricalOrderItemListView(ElectricalOrderItemListViewModel electricalOrderItemListViewModel)
        {
            InitializeComponent();
            DataContext = electricalOrderItemListViewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await (DataContext as BaseListViewModel).OnLoaded();          

        }

        private void OrderDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is DataGrid dg)
            {
                (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
            }
        }
    }
}
