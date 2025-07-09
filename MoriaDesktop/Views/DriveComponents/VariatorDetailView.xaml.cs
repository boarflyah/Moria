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
using MoriaDesktop.ViewModels.DriveComponents;

namespace MoriaDesktop.Views.DriveComponents
{
    /// <summary>
    /// Logika interakcji dla klasy VariatorDetailView.xaml
    /// </summary>
    public partial class VariatorDetailView : Page
    {
        private VariatorDetailViewModel vm;
        public VariatorDetailView(VariatorDetailViewModel variatorDetailViewModel)
        {
            InitializeComponent();
            vm = variatorDetailViewModel;
            this.DataContext = vm;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await (DataContext as BaseDetailViewModel)!.Load();
        }
    }
}
