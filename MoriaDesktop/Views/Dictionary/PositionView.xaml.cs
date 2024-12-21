using System.Windows.Controls;
using MoriaDesktop.ViewModels.Dictionary;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;
public partial class PositionView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public PositionView( PositionViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}

