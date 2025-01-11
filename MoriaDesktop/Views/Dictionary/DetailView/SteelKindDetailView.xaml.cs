using System.Windows.Controls;
using MoriaDesktop.ViewModels.Dictionary;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;
public partial class SteelKindView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public SteelKindView(SteelKindViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}

