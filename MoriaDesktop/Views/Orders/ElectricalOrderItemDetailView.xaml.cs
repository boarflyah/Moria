using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Orders;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.ViewModels;
using MoriaModels.Models.EntityPersonel;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.Views.Orders;

public partial class ElectricalOrderItemDetailView : Page, IViewModelContent
{
    readonly ILookupService _lookupService;

    public object GetViewModel() => DataContext;
    public ElectricalOrderItemDetailView(ElectricalOrderItemDetailViewModel vm, ILookupService lookupService)
    {
        InitializeComponent();
        DataContext = vm;
        _lookupService = lookupService;
    }
    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as BaseDetailViewModel)!.Load();
    }

    private async void ElectricianLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var electrican = await _lookupService.ShowLookup<EmployeeDo>(false);
        if (electrican != null)
            (DataContext as ElectricalOrderItemDetailViewModel).Electrician = electrican;
    }

    private async void ElectricalCabinetLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var electricalCabinet = await _lookupService.ShowLookup<ElectricalCabinetDo>(false);
        if (electricalCabinet != null)
            (DataContext as ElectricalOrderItemDetailViewModel).ElectricalCabinet = electricalCabinet;
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
}
