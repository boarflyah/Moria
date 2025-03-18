using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Orders;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Orders;
public class OrderListViewModel: BaseListViewModel
{
    readonly IApiOrderService _orderService;

    public OrderListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiOrderService orderService)
        : base(logger, appStateService, navigationService)
    {
        _orderService = orderService;

        Orders = new();
        Title = "Zamówienia";
    }

    #region properties

    public ObservableCollection<OrderDo> Orders
    {
        get; set;
    }

    #endregion

    #region commands

    protected async override Task LoadList()
    {
        Orders.Clear();

        var products = await ExecuteApiRequest(_orderService.GetOrders, _appStateService.LoggedUser.Username);
        if (products != null && products.Any())
        {
            foreach (var product in products)
                Orders.Add(product);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    public override void OnRowSelected(object row)
    {
        if (row is OrderDo odo)
            _navigationService.NavigateTo(typeof(OrderDetailViewModel), false, odo.Id);
    }

    protected override void New() =>
        _navigationService.NavigateTo(typeof(OrderDetailViewModel), false, null);

    protected async override Task<bool> SendDeleteRequest() =>
        await ExecuteApiRequest(_orderService.DeleteOrder, _appStateService.LoggedUser.Username, (Selected as OrderDo)?.Id ?? 0);


    #endregion

}
