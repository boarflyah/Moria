using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktop.Views.Dictionary.Window;
using MoriaModelsDo.Models.DriveComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MoriaDesktop.Views.Dictionary.DetailView;
public partial class ColorDetailView : Page
{

    private ColorDetailViewModel cdvm;
    public ColorDetailView( ColorDetailViewModel viewModel)
    {       
        InitializeComponent();
        this.DataContext = viewModel;
        cdvm = viewModel;
    }

    private void NewButton_Click(object sender, RoutedEventArgs e)
    {
        var colorWindow = new ColorWindowView(cdvm);

        // Wywołujemy okno w trybie modalnym
        bool? dialogResult = colorWindow.ShowDialog();

    }
}
