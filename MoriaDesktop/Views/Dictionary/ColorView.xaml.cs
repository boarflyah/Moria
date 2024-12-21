using System.Windows.Controls;
using MoriaDesktop.ViewModels.Dictionary;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;
public partial class ColorView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public ColorView( ColorViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}
