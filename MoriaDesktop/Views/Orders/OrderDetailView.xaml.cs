using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Orders;
using MoriaDesktop.Views.Orders.Window;
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

    private void AddDate_Click(object sender, RoutedEventArgs e)
    {
        var selectedOrderItems = (OrderItemsDataGrid)?.SelectedItems
                         .Cast<OrderItemDo>()
                         .ToList();

        if (selectedOrderItems == null || selectedOrderItems.Count == 0)
            return;

        var userPermissions = (DataContext as OrderDetailViewModel).GetUserPersssion();

        var window = new SetProductionDateWindowView(userPermissions);
        bool? result = window.ShowDialog();
        if (result == true)
        {
            foreach (var item in selectedOrderItems)
            {
                bool modified = false;

                if (window.TechnicalDrawingPlanned.HasValue &&
                    item.TechnicalDrawingPlanned != window.TechnicalDrawingPlanned)
                {
                    item.TechnicalDrawingPlanned = window.TechnicalDrawingPlanned.Value;
                    modified = true;
                }

                if (window.TechnicalDrawingStarted.HasValue &&
                    item.TechnicalDrawingStarted != window.TechnicalDrawingStarted)
                {
                    item.TechnicalDrawingStarted = window.TechnicalDrawingStarted.Value;
                    modified = true;
                }

                if (window.TechnicalDrawingCompleted.HasValue &&
                    item.TechnicalDrawingCompleted != window.TechnicalDrawingCompleted)
                {
                    item.TechnicalDrawingCompleted = window.TechnicalDrawingCompleted.Value;
                    modified = true;
                }

                if (window.CuttingPlanned.HasValue &&
                   item.CuttingPlanned != window.CuttingPlanned)
                {
                    item.CuttingPlanned = window.CuttingPlanned.Value;
                    modified = true;
                }

                if (window.CuttingStarted.HasValue &&
                   item.CuttingStarted != window.CuttingStarted)
                {
                    item.CuttingStarted = window.CuttingStarted.Value;
                    modified = true;
                }

                if (window.CuttingCompleted.HasValue &&
                    item.CuttingCompleted != window.CuttingCompleted)
                {
                    item.CuttingCompleted = window.CuttingCompleted.Value;
                    modified = true;
                }

                if (window.WeldingPlanned.HasValue &&
                    item.WeldingPlanned != window.WeldingPlanned)
                {
                    item.WeldingPlanned = window.WeldingPlanned.Value;
                    modified = true;
                }

                if (window.WeldingStarted.HasValue &&
                    item.WeldingStarted != window.WeldingStarted)
                {
                    item.WeldingStarted = window.WeldingStarted.Value;
                    modified = true;
                }

                if (window.WeldingCompleted.HasValue &&
                 item.WeldingCompleted != window.WeldingCompleted)
                {
                    item.WeldingCompleted = window.WeldingCompleted.Value;
                    modified = true;
                }

                if (window.MetalCliningPlanned.HasValue &&
                    item.MetalCliningPlanned != window.MetalCliningPlanned)
                {
                    item.MetalCliningPlanned = window.MetalCliningPlanned.Value;
                    modified = true;
                }

                if (window.MetalCliningStarted.HasValue &&
                  item.MetalCliningStarted != window.MetalCliningStarted)
                {
                    item.MetalCliningStarted = window.MetalCliningStarted.Value;
                    modified = true;
                }

                if (window.MetalCliningCompleted.HasValue &&
                  item.MetalCliningCompleted != window.MetalCliningCompleted)
                {
                    item.MetalCliningCompleted = window.MetalCliningCompleted.Value;
                    modified = true;
                }

                if (window.PaintingPlanned.HasValue &&
                    item.PaintingPlanned != window.PaintingPlanned)
                {
                    item.PaintingPlanned = window.PaintingPlanned.Value;
                    modified = true;
                }

                if (window.PaintingStarted.HasValue &&
                   item.PaintingStarted != window.PaintingStarted)
                {
                    item.PaintingStarted = window.PaintingStarted.Value;
                    modified = true;
                }

                if (window.PaintingCompleted.HasValue &&
                   item.PaintingCompleted != window.PaintingCompleted)
                {
                    item.PaintingCompleted = window.PaintingCompleted.Value;
                    modified = true;
                }

                if (window.PlannedMachineAssembled.HasValue &&
                    item.PlannedMachineAssembled != window.PlannedMachineAssembled)
                {
                    item.PlannedMachineAssembled = window.PlannedMachineAssembled.Value;
                    modified = true;
                }

                if (window.MachineAssembledStarted.HasValue &&
                    item.MachineAssembledStarted != window.MachineAssembledStarted)
                {
                    item.MachineAssembledStarted = window.MachineAssembledStarted.Value;
                    modified = true;
                }

                if (window.MachineAssembled.HasValue &&
                    item.MachineAssembled != window.MachineAssembled)
                {
                    item.MachineAssembled = window.MachineAssembled.Value;
                    modified = true;
                }

                if (window.PlannedMachineAssembledAll.HasValue &&
               item.PlannedMachineAssembledAll != window.PlannedMachineAssembledAll)
                {
                    item.PlannedMachineAssembledAll = window.PlannedMachineAssembledAll.Value;
                    modified = true;
                }

                if (window.MachineAssembledAllStarted.HasValue &&
                    item.MachineAssembledAllStarted != window.MachineAssembledAllStarted)
                {
                    item.MachineAssembledAllStarted = window.MachineAssembledAllStarted.Value;
                    modified = true;
                }

                if (window.MachineAssembledAllCompleted.HasValue &&
                    item.MachineAssembledAllCompleted != window.MachineAssembledAllCompleted)
                {
                    item.MachineAssembledAllCompleted = window.MachineAssembledAllCompleted.Value;
                    modified = true;
                }

                if (window.PlannedMachineWiredAndTested.HasValue &&
                   item.PlannedMachineWiredAndTested != window.PlannedMachineWiredAndTested)
                {
                    item.PlannedMachineWiredAndTested = window.PlannedMachineWiredAndTested.Value;
                    modified = true;
                }

                if (window.MachineWiredAndTestedStarted.HasValue &&
                   item.MachineWiredAndTestedStarted != window.MachineWiredAndTestedStarted)
                {
                    item.MachineWiredAndTestedStarted = window.MachineWiredAndTestedStarted.Value;
                    modified = true;
                }

                if (window.MachineWiredAndTested.HasValue &&
                    item.MachineWiredAndTested != window.MachineWiredAndTested)
                {
                    item.MachineWiredAndTested = window.MachineWiredAndTested.Value;
                    modified = true;
                }

                if (window.MachineReleased.HasValue &&
                    item.MachineReleased != window.MachineReleased)
                {
                    item.MachineReleased = window.MachineReleased.Value;
                    modified = true;
                }

                if (window.TransportOrdered.HasValue &&
                    item.TransportOrdered != window.TransportOrdered)
                {
                    item.TransportOrdered = window.TransportOrdered.Value;
                    modified = true;
                }


                if (window.PlannedTransport.HasValue &&
                    item.PlannedTransport != window.PlannedTransport)
                {
                    item.PlannedTransport = window.PlannedTransport.Value;
                    modified = true;
                }

                if (window.DueDate.HasValue &&
                    item.DueDate != window.DueDate)
                {
                    item.DueDate = window.DueDate.Value;
                    modified = true;
                }

                if (modified)
                {
                    (DataContext as OrderDetailViewModel).HasObjectChanged = true;
                    item.ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Modified;
                }
            }
        }
    }

    private void OrderItemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
