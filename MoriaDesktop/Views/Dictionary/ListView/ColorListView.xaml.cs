using System.Windows.Controls;
using MoriaDesktop.ViewModels.Dictionary.ListView;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary.ListView;

public partial class ColorListView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public ColorListView(ColorListViewModel viewModel )
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
