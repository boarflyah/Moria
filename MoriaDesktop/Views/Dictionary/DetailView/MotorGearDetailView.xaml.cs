using System.Windows.Controls;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;
public partial class MotorGearDetailView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public MotorGearDetailView(MotorGearDetailViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await(DataContext as BaseDetailViewModel)!.Load();
    }
}
