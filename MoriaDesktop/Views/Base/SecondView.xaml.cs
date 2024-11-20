using System.Windows.Controls;
using MoriaDesktop.ViewModels.Base;

namespace MoriaDesktop.Views.Base;
/// <summary>
/// Logika interakcji dla klasy SecondView.xaml
/// </summary>
public partial class SecondView : Page
{
    public SecondView(SecondViewModel viewModel) : this()
    {
        DataContext = viewModel;
    }

    public SecondView()
    {
        InitializeComponent();
    }
}
