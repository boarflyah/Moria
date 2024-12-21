using System.Windows.Controls;
using MoriaDesktop.ViewModels.Dictionary;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;

public partial class EmployeeView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public EmployeeView( EmployeeViewModel viewModel )
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
