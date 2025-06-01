using System.Xml.Linq;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using MoriaDesktop.Attributes;
using MoriaDesktop.Models;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModels.Models.Orders;
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
        
        WeakReferenceMessenger.Default.Register<NavigationMessage<(OrderDo order, bool isLocked)>>(this, OnMessageReceived);
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

    PermissionDo _Permission_OrderNumberSymbol;
    public PermissionDo Permission_OrderNumberSymbol
    {
        get => _Permission_OrderNumberSymbol;
        set
        {
            _Permission_OrderNumberSymbol = value;
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

    PermissionDo _Permission_ClientSymbol;
    public PermissionDo Permission_ClientSymbol
    {
        get => _Permission_ClientSymbol;
        set
        {
            _Permission_ClientSymbol = value;
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

    PermissionDo _Permission_Remarks;
    public PermissionDo Permission_Remarks
    {
        get => _Permission_Remarks;
        set
        {
            _Permission_Remarks = value;
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

    PermissionDo _Permission_CatalogLink;
    public PermissionDo Permission_CatalogLink
    {
        get => _Permission_CatalogLink;
        set
        {
            _Permission_CatalogLink = value;
            RaisePropertyChanged(value);
        }
    }

    private string _SalesOfferLink;
    [ObjectChangedValidate]
    public string SalesOfferLink
    {
        get => _SalesOfferLink;
        set
        {
            _SalesOfferLink = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_SalesOfferLink;
    public PermissionDo Permission_SalesOfferLink
    {
        get => _Permission_SalesOfferLink;
        set
        {
            _Permission_SalesOfferLink = value;
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

    PermissionDo _Permission_OrderingContact;
    public PermissionDo Permission_OrderingContact
    {
        get => _Permission_OrderingContact;
        set
        {
            _Permission_OrderingContact = value;
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

    PermissionDo _Permission_ReceivingContact;
    public PermissionDo Permission_ReceivingContact
    {
        get => _Permission_ReceivingContact;
        set
        {
            _Permission_ReceivingContact = value;
            RaisePropertyChanged(value);
        }
    }


    // Rysunek techniczny
    private bool _TechnicalDrawingCompleted;
    public bool TechnicalDrawingCompleted
    {
        get => _TechnicalDrawingCompleted;
        set
        {
            _TechnicalDrawingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_TechnicalDrawingCompleted;
    public PermissionDo Permission_TechnicalDrawingCompleted
    {
        get => _Permission_TechnicalDrawingCompleted;
        set
        {
            _Permission_TechnicalDrawingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _CuttingCompleted;
    public bool CuttingCompleted
    {
        get => _CuttingCompleted;
        set
        {
            _CuttingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_CuttingCompleted;
    public PermissionDo Permission_CuttingCompleted
    {
        get => _Permission_CuttingCompleted;
        set
        {
            _Permission_CuttingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _WeldingCompleted;
    public bool Weldingompleted
    {
        get => _WeldingCompleted;
        set
        {
            _WeldingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_WeldingCompleted;
    public PermissionDo Permission_WeldingCompleted
    {
        get => _Permission_WeldingCompleted;
        set
        {
            _Permission_WeldingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _MetalCliningCompleted;
    public bool MetalCliningCompleted
    {
        get => _MetalCliningCompleted;
        set
        {
            _MetalCliningCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_MetalCliningCompleted;
    public PermissionDo Permission_MetalCliningCompleted
    {
        get => _Permission_MetalCliningCompleted;
        set
        {
            _Permission_MetalCliningCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _PaintingCompleted;
    public bool PaintingCompleted
    {
        get => _PaintingCompleted;
        set
        {
            _PaintingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_PaintingCompleted;
    public PermissionDo Permission_PaintingCompleted
    {
        get => _Permission_PaintingCompleted;
        set
        {
            _Permission_PaintingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _ElectricaCabinetCompleted;
    public bool ElectricaCabinetCompleted
    {
        get => _ElectricaCabinetCompleted;
        set
        {
            _ElectricaCabinetCompleted = value;
            RaisePropertyChanged(value);
        }
    }


    private PermissionDo _Permission_ElectricaCabinetCompleted;
    public PermissionDo Permission_ElectricaCabinetCompleted
    {
        get => _Permission_ElectricaCabinetCompleted;
        set
        {
            _Permission_ElectricaCabinetCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _MachineAssembled;
    public bool MachineAssembled
    {
        get => _MachineAssembled;
        set
        {
            _MachineAssembled = value;
            RaisePropertyChanged(value);
        }
    }


    private PermissionDo _Permission_MachineAssembled;
    public PermissionDo Permission_MachineAssembled
    {
        get => _Permission_MachineAssembled;
        set
        {
            _Permission_MachineAssembled = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _MachineWiredAndTested;
    public bool MachineWiredAndTested
    {
        get => _MachineWiredAndTested;
        set
        {
            _MachineWiredAndTested = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_MachineWiredAndTested;
    public PermissionDo Permission_MachineWiredAndTested
    {
        get => _Permission_MachineWiredAndTested;
        set
        {
            _Permission_MachineWiredAndTested = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _MachineReleased;
    public bool MachineReleased
    {
        get => _MachineReleased;
        set
        {
            _MachineReleased = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_MachineReleased;
    public PermissionDo Permission_MachineReleased
    {
        get => _Permission_MachineReleased;
        set
        {
            _Permission_MachineReleased = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _TransportOrdered;
    public bool TransportOrdered
    {
        get => _TransportOrdered;
        set
        {
            _TransportOrdered = value;
            RaisePropertyChanged(value);
        }
    }


    private PermissionDo _Permission_TransportOrdered;
    public PermissionDo Permission_TransportOrdered
    {
        get => _Permission_TransportOrdered;
        set
        {
            _Permission_TransportOrdered = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime _DueDate;
    public DateTime DueDate
    {
        get => _DueDate;
        set
        {
            _DueDate = value;
            RaisePropertyChanged(value);
        }
    }


    private PermissionDo _Permission_DueDate;
    public PermissionDo Permission_DueDate
    {
        get => _Permission_DueDate;
        set
        {
            _Permission_DueDate = value;
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
            HasObjectChanged = IsLocked ? false : true;
            objectId = onRenavigationReturned?.Id ?? -1;
            isNew = objectId <= 0 ? true : false;

            UnlockCommand.NotifyCanExecuteChanged();
            EditCommand.NotifyCanExecuteChanged();
        }
    }

    public List<PermissionDo> GetUserPersssion() => _appStateService.LoggedUser.Position.Permissions.ToList();

    public override Task<bool> OnNavigatingFrom()
    {
        WeakReferenceMessenger.Default.Unregister<NavigationMessage<(OrderDo order, bool isLocked)>>(this);
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
        isRenavigated = false;
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

        HasObjectChanged = false;
        _navigationService.NavigateTo(typeof(OrderItemDetailViewModel), true, orderItem, order, IsLocked);
    }

    public override void Clear()
    {
        ClientSymbol = default;
        CatalogLink = default;
        OrderingContact = default;
        ReceivingContact = default;
        SalesOfferLink = default;
        Remarks = default;
        OrderNumberSymbol = default;
        TechnicalDrawingCompleted = default;
        CuttingCompleted = default;
        Weldingompleted = default;
        MetalCliningCompleted = default;
        PaintingCompleted = default;
        ElectricaCabinetCompleted = default;
        MachineAssembled = default;
        MachineWiredAndTested = default;
        MachineReleased = default;
        TransportOrdered = default;
        DueDate = default;
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
            SalesOfferLink = this.SalesOfferLink,
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
        SalesOfferLink = order.SalesOfferLink;
        LastModified = order.LastModified;

        if (order.OrderItems != null && order.OrderItems.Any())
        {
            foreach (var orderItem in order.OrderItems)
                Objects.Add(orderItem);

            TechnicalDrawingCompleted = !order.OrderItems.Any(x => x.TechnicalDrawingCompleted == null);
            Weldingompleted = !order.OrderItems.Any(x => x.WeldingCompleted == null);
            CuttingCompleted = !order.OrderItems.Any(y => y.CuttingCompleted == null);
            MetalCliningCompleted = !order.OrderItems.Any(x => x.MetalCliningCompleted == null);
            PaintingCompleted = !order.OrderItems.Any(y => y.PaintingCompleted == null);
            ElectricaCabinetCompleted = !order.OrderItems.Any( x => x.ElectricaCabinetCompleted == null);
            MachineAssembled = !order.OrderItems.Any(y => y.MachineAssembled == null);
            MachineWiredAndTested = !order.OrderItems.Any(x => x.MachineWiredAndTested == null);
            MachineReleased = !order.OrderItems.Any( x => x.MachineReleased == null );
            TransportOrdered = !order.OrderItems.Any( y => y.TransportOrdered == null );
            DueDate = order.OrderItems.Max( x=> x.DueDate );
        }
        
    }

    void OnMessageReceived(object recipient, NavigationMessage<(OrderDo order, bool isLocked)> message)
    {
        if (message.Value.order != null)
            isRenavigated = true;
        IsLocked = message.Value.isLocked;
        onRenavigationReturned = message.Value.order;
    }

    protected override Type GetOnCloseNavigationTarget() => typeof(OrderListViewModel);

    #endregion
}
