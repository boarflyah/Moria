using System.Windows.Controls;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;
public partial class ContactDetailView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public ContactDetailView(ContactDetailViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}

