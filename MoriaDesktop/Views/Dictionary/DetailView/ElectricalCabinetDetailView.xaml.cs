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
using MoriaDesktop.ViewModels.Dictionary.DetailView;

namespace MoriaDesktop.Views.Dictionary.DetailView
{
    /// <summary>
    /// Logika interakcji dla klasy ElectricalCabinetDetailView.xaml
    /// </summary>
    public partial class ElectricalCabinetDetailView : Page
    {
        private ElectricalCabinetDetailViewModel edvm;
        public ElectricalCabinetDetailView(ElectricalCabinetDetailViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            edvm = viewModel;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await (DataContext as BaseDetailViewModel)!.Load();
        }
    }
}
