using System.Windows.Controls;
using MoriaDesktop.ViewModels.Dictionary;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;
public partial class MotorGearView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public MotorGearView(MotorGearViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}
