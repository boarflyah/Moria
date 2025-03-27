using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
}
