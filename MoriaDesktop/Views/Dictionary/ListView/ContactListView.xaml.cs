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
    /// Interaction logic for ContactListView.xaml
    /// </summary>
    public partial class ContactListView : Page
    {
        public ContactListView(ContactListViewModel listViewModel) : this()
        {
            DataContext = listViewModel;
        }

        public ContactListView()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await(DataContext as ContactListViewModel).OnLoaded();
        }

        private void ContactDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is DataGrid dg)
            {
                (DataContext as BaseListViewModel).OnRowSelected(dg.CurrentItem);
            }
        }
    }
}
