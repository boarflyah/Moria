using System.Windows;
using System.Windows.Controls;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;

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

    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await(DataContext as BaseDetailViewModel)!.Load();
    }

}
