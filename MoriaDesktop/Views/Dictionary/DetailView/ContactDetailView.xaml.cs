using MoriaDesktop.ViewModels.Dictionary.DetailView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

