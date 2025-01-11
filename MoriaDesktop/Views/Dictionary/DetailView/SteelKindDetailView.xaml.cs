using System.Windows.Controls;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;
public partial class SteelKindDetailView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public SteelKindDetailView(SteelKindDetailViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}

