using System.Windows.Controls;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Base;

public partial class SecondView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public SecondView(SecondViewModel viewModel) : this()
    {
        DataContext = viewModel;
    }

    public SecondView()
    {
        InitializeComponent();
    }
}
