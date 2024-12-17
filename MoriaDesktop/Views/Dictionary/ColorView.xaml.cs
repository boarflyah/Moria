using MoriaDesktop.ViewModels.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MoriaDesktop.Views.Dictionary;
public partial class ColorView : Page
{
    public ColorView( ColorViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}
