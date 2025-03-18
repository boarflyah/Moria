using System.Xml.Linq;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Attributes;
using MoriaDesktop.Models;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.Orders;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Orders;
public class OrderDetailViewModel : BaseDetailWithNestedListViewModel
{
    readonly IApiOrderService _orderService;

    public OrderDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IApiOrderService orderService, IKeepAliveWorker worker)
        : base(logger, appStateService, apiLockService, navigationService, worker)
    {
        _orderService = orderService;

        Title = "Nowe zamówienie";

        WeakReferenceMessenger.Default.Register<NavigationMessage<OrderDo>>(this, OnMessageReceived);
    }

    #region properties

    bool isRenavigated;
    OrderDo onRenavigationReturned;

    private string _OrderNumberSymbol;
    [ObjectChangedValidate]
    [DefaultProperty]
    public string OrderNumberSymbol
    {
        get => _OrderNumberSymbol;
        set
        {
            _OrderNumberSymbol = value;
            RaisePropertyChanged(value);
        }
    }


    private string _ClientSymbol;
    [ObjectChangedValidate]
    public string ClientSymbol
    {
        get => _ClientSymbol;
        set
        {
            _ClientSymbol = value;
            RaisePropertyChanged(value);
        }
    }


    private string _Remarks;
    [ObjectChangedValidate]
    public string Remarks
    {
        get => _Remarks;
        set
        {
            _Remarks = value;
            RaisePropertyChanged(value);
        }
    }


    private string _CatalogLink;
    [ObjectChangedValidate]
    public string CatalogLink
    {
        get => _CatalogLink;
        set
        {
            _CatalogLink = value;
            RaisePropertyChanged(value);
        }
    }


    private ContactDo _OrderingContact;
    [ObjectChangedValidate]
    public ContactDo OrderingContact
    {
        get => _OrderingContact;
        set
        {
            _OrderingContact = value;
            RaisePropertyChanged(value);
        }
    }

    private ContactDo _ReceivingContact;
    [ObjectChangedValidate]
    public ContactDo ReceivingContact
    {
        get => _ReceivingContact;
        set
        {
            _ReceivingContact = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region nested listview

    protected async override Task NestedNew()
    {
        var newOrderItem = new OrderItemDo()
        {
            ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Added,
        };
        Objects.Add(newOrderItem);

        HasObjectChanged = true;

        CreateUpdateOrderItem(newOrderItem);
    }

    protected override string GetObjectsListViewTitle() => "Pozycje";

    #endregion

    #region methods

    public override void OnNavigatedTo(params object[] parameters)
    {
        base.OnNavigatedTo(parameters);

        if (isRenavigated)
        {
            Clear();
            Setup(onRenavigationReturned);
            HasObjectChanged = true;
            isNew = false;
            objectId = onRenavigationReturned?.Id ?? -1;
        }
    }

    public override Task OnNavigatingFrom()
    {
        WeakReferenceMessenger.Default.Unregister<NavigationMessage<ProductDo>>(this);
        return base.OnNavigatingFrom();
    }

    public override Type GetModelType() => typeof(OrderDo);
    protected async override Task LoadObject()
    {
        if (!isRenavigated)
        {
            Clear();

            var order = await ExecuteApiRequest(_orderService.GetOrder, _appStateService.LoggedUser.Username, objectId);
            if (order != null)
                Setup(order);
            else
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
        }

    }

    protected async override Task<bool> SaveNewObject()
    {
        var order = GetDo() as OrderDo;
        var newObject = await _orderService.CreateOrder(_appStateService.LoggedUser.Username, order);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var order = GetDo() as OrderDo;
        var updated = await _orderService.UpdateOrder(_appStateService.LoggedUser.Username, order);
        return updated != null;
    }

    public void OnOrderItemSelected(OrderItemDo orderItem)
    {
        CreateUpdateOrderItem(orderItem);
    }

    void CreateUpdateOrderItem(OrderItemDo orderItem)
    {
        var order = GetDo() as OrderDo;
        foreach (var oi in Objects.OfType<OrderItemDo>())
            if (!order.OrderItems.Any(x => x == oi))
                order.OrderItems.Add(oi);

        _navigationService.NavigateTo(typeof(OrderItemDetailViewModel), false, orderItem, order);
    }

    public override void Clear()
    {
        ClientSymbol = default;
        CatalogLink = default;
        OrderingContact = default;
        ReceivingContact = default;
        Remarks = default;
        OrderNumberSymbol = default;
        Objects.Clear();
    }

    public override BaseDo GetDo()
    {
        var result = new OrderDo()
        {
            CatalogLink = this.CatalogLink,
            OrderingContact = this.OrderingContact,
            ReceivingContact = this.ReceivingContact,
            OrderNumberSymbol = this.OrderNumberSymbol,
            Remarks = this.Remarks,
            LastModified = _appStateService.LoggedUser.Username,
            Id = objectId,
            ClientSymbol = this.ClientSymbol,
        };

        foreach (var orderItem in Objects.Where(x => x.ChangeType != MoriaModelsDo.Base.Enums.SystemChangeType.None).OfType<OrderItemDo>())
            result.OrderItems.Add(orderItem);

        return result;
    }

    void Setup(OrderDo order)
    {
        ClientSymbol = order.ClientSymbol;
        CatalogLink = order.CatalogLink;
        OrderingContact = order.OrderingContact;
        ReceivingContact = order.ReceivingContact;
        Remarks = order.Remarks;
        OrderNumberSymbol = order.OrderNumberSymbol;
        LastModified = order.LastModified;

        if (order.OrderItems != null && order.OrderItems.Any())
            foreach (var orderItem in order.OrderItems)
                Objects.Add(orderItem);
    }

    void OnMessageReceived(object recipient, NavigationMessage<OrderDo> message)
    {
        if (message.Value != null)
            isRenavigated = true;
        onRenavigationReturned = message.Value;
    }

    #endregion
}
