using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.ListView;
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

namespace MoriaDesktop.Views.Dictionary.ListView
{
    /// <summary>
    /// Interaction logic for MotorGearListView.xaml
    /// </summary>
    public partial class MotorGearListView : Page
    {
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
        }
    }
}
