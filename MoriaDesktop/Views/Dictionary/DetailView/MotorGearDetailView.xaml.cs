using System.Windows.Controls;
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
}
