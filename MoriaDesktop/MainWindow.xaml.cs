using System.Windows;
using MoriaDesktop.Models;
using MoriaDesktop.ViewModels.Base;

namespace MoriaDesktop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(null);
    }

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        (DataContext as MainWindowViewModel).OnNavigationSelectionChanged(e.NewValue);
    }
}