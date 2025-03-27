using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaBaseServices;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;

public partial class MotorDetailView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public MotorDetailView(MotorDetailViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !(sender as TextBox).Text.IsNumber(e.Text);
    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true;
        }
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await(DataContext as BaseDetailViewModel)!.Load();
    }
}
