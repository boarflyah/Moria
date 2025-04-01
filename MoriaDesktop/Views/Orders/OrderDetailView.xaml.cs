using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Orders;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.ViewModels;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Orders;

namespace MoriaDesktop.Views.Orders;

public partial class OrderDetailView : Page, IViewModelContent
{
    readonly ILookupService _lookupService;

    public object GetViewModel() => DataContext; 

    public OrderDetailView(OrderDetailViewModel vm, ILookupService lookupService)
    {
        InitializeComponent();
        DataContext = vm;
        _lookupService = lookupService;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as BaseDetailViewModel).Load();

    }

    private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.OriginalSource is FrameworkElement fe && fe.DataContext is OrderItemDo oido)
            (DataContext as OrderDetailViewModel).OnOrderItemSelected(oido);
    }

    private async void OrderingLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var contact = await _lookupService.ShowLookup<ContactDo>(false);
        if (contact != null)
            (DataContext as OrderDetailViewModel).OrderingContact = contact;
    }

    private async void ReceivingLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var contact = await _lookupService.ShowLookup<ContactDo>(false);
        if (contact != null)
            (DataContext as OrderDetailViewModel).ReceivingContact = contact;
    }

    private void CatalogLinkTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        if (textBox == null) return;

        string path = textBox.Text;

        if (Directory.Exists(path))
        {
            textBox.Foreground = Brushes.Blue; 
        }
        else
        {
            textBox.Foreground = Brushes.Black; 
        }
    }

    private void CatalogLinkTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        if (textBox == null) return;

        string path = textBox.Text;

        if (Directory.Exists(path))
        {
            Process.Start("explorer.exe", path);
        }
        else
        {
            MessageBox.Show("Podany katalog nie istnieje!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void SalesOfferLinkTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        if (textBox == null) return;

        string path = textBox.Text;

        if (File.Exists(path) && Path.GetExtension(path).ToLower() == ".pdf")
        {
            textBox.Foreground = Brushes.Green; 
        }
        else
        {
            textBox.Foreground = Brushes.Black;
        }
    }

    private void SalesOfferLinkTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        if (textBox == null) return;

        string path = textBox.Text;

        if (File.Exists(path) && Path.GetExtension(path).ToLower() == ".pdf")
        {
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        }
        else
        {
            MessageBox.Show("Podany plik PDF nie istnieje!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
