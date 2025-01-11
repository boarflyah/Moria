using System.Windows.Controls;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;
public partial class PositionDetailView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public PositionDetailView(PositionDetailViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}

