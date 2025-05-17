using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Orders;

namespace MoriaDesktop.ViewModels.Orders;

public class ElectricalOrderItemListViewModel : BaseListViewModel
{
    readonly IApiOrderService _orderService;
    public ElectricalOrderItemListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiListViewService listViewService, IApiOrderService orderService) : base(logger, appStateService, navigationService, listViewService)
    {
        _orderService = orderService;
        OrderItems = new();
        Title = "Zamówienia";
    }

    #region properties

    public ObservableCollection<OrderItemDo> OrderItems
    {
        get; set;
    }

    #endregion

    public override void OnRowSelected(object row)
    {
        if (row is OrderDo odo)
            _navigationService.NavigateTo(typeof(ElectricalOrderItemDetailViewModel), true, odo.Id);
    }

    protected async override Task LoadList()
    {
        OrderItems.Clear();

        var products = await ExecuteApiRequest(_orderService.GetOrderItems, _appStateService.LoggedUser.Username);
        if (products != null && products.Any())
        {
            foreach (var product in products)
                OrderItems.Add(product);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected override void New()
    {
    }

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<OrderItemDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    OrderItems.Clear();
                    foreach (var item in result)
                    {
                        OrderItems.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, $"Brak danych do wczytania. {ex.Message}", true);
            }
        }
        else
            await LoadList();
    }

    protected override Task<bool> SendDeleteRequest()
    {
        throw new NotImplementedException();
    }

    protected override bool CanDelete() => false;
}
