using System.Windows.Controls;
using MoriaDesktop.ViewModels.Dictionary;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;
public partial class ContactView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public ContactView(ContactViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}

