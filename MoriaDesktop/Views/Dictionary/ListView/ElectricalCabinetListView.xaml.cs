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
using MoriaDesktop.ViewModels.Dictionary.ListView;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary.ListView
{
    /// <summary>
    /// Logika interakcji dla klasy ElectricalCabinetListView.xaml
    /// </summary>
    public partial class ElectricalCabinetListView : Page, IViewModelContent
    {
        public object GetViewModel() => DataContext;
        public ElectricalCabinetListView(ElectricalCabinetListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
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
            await (DataContext as ElectricalCabinetListViewModel).OnLoaded();

            var vm = DataContext as ElectricalCabinetListViewModel;
            if (vm != null && !vm.Permission_Symbol.CanRead)
            {
                ColorDataGrid.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            }

        }
    }
}
