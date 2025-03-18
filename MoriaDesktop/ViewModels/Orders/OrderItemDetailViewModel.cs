using System.Xml.Linq;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Models;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents.Relations;
using MoriaModelsDo.Models.Orders;
using MoriaModelsDo.Models.Orders.Relations;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Orders;
public class OrderItemDetailViewModel : BaseDetailWithNestedListViewModel
{
    public OrderItemDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService)
        : base(logger, appStateService, apiLockService, navigationService)
    {
    }

    #region properties

    OrderDo order;
    OrderItemDo currentOrderItem;

    private int _Index;
    [ObjectChangedValidate]
    public int Index
    {
        get => _Index;
        set
        {
            _Index = value;
            RaisePropertyChanged(value);
        }
    }

    private string _Description;
    [ObjectChangedValidate]
    public string Description
    {
        get => _Description;
        set
        {
            _Description = value;
            RaisePropertyChanged(value);
        }
    }

    private string _Notes;
    [ObjectChangedValidate]
    public string Notes
    {
        get => _Notes;
        set
        {
            _Notes = value;
            RaisePropertyChanged(value);
        }
    }

    private decimal _MachineWeight;
    [ObjectChangedValidate]
    public decimal MachineWeight
    {
        get => _MachineWeight;
        set
        {
            _MachineWeight = value;
            RaisePropertyChanged(value);
        }
    }

    private string _TechnicalDrawingLink;
    [ObjectChangedValidate]
    public string TechnicalDrawingLink
    {
        get => _TechnicalDrawingLink;
        set
        {
            _TechnicalDrawingLink = value;
            RaisePropertyChanged(value);
        }
    }

    private double _Quantity;
    [ObjectChangedValidate]
    public double Quantity
    {
        get => _Quantity;
        set
        {
            _Quantity = value;
            RaisePropertyChanged(value);
        }
    }

    private ProductDo _Product;
    [ObjectChangedValidate]
    public ProductDo Product
    {
        get => _Product;
        set
        {
            _Product = value;
            if (value != null)
            {
                Component = null;
                Drive = null;
            }
            RaisePropertyChanged(value);
        }
    }

    private ComponentDo _Component;
    [ObjectChangedValidate]
    public ComponentDo Component
    {
        get => _Component;
        set
        {
            _Component = value;
            if (value != null)
            {
                Product = null;
                Drive = null;
            }
            RaisePropertyChanged(value);
        }
    }

    private DriveDo _Drive;
    [ObjectChangedValidate]
    public DriveDo Drive
    {
        get => _Drive;
        set
        {
            _Drive = value;
            if (value != null)
            {
                Component = null;
                Product = null;
            }
            RaisePropertyChanged(value);
        }
    }

    private WarehouseDo _Warehouse;
    [ObjectChangedValidate]
    public WarehouseDo Warehouse
    {
        get => _Warehouse;
        set
        {
            _Warehouse = value;
            RaisePropertyChanged(value);
        }
    }

    private EmployeeDo _Designer;
    [ObjectChangedValidate]
    public EmployeeDo Designer
    {
        get => _Designer;
        set
        {
            _Designer = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region nestedlistview

    protected override string GetObjectsListViewTitle() => "Personalizacja";

    protected async override Task NestedNew()
    {
        var ctoi = new ComponentToOrderItemDo()
        {
            ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Added
        };
        Objects.Add(ctoi);
        HasObjectChanged = true;
    }

    #endregion

    #region methods

    public override void OnNavigatedTo(params object[] parameters)
    {
        if (parameters.Length > 1)
        {
            currentOrderItem = parameters.FirstOrDefault(x => x is OrderItemDo) as OrderItemDo;
            if (currentOrderItem?.Id > 0)
            {
                currentOrderItem.ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Modified;
                //objectId = currentComponent.Id;
            }
            else
            {
                isNew = true;
                currentOrderItem.ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Added;
                //Setup(currentComponent);
            }
            order = parameters.FirstOrDefault(x => x is OrderDo) as OrderDo;
        }
    }

    public async override Task OnNavigatingFrom()
    {
        _appStateService.CurrentDetailViewObjectId = 0;
        _appStateService.CurrentDetailViewObjectType = null;
    }

    #endregion
    public override Type GetModelType() => typeof(OrderItemDo);

    public override void Clear()
    {
        Objects?.Clear();
        Index = default;
        Description = default;
        Notes = default;
        MachineWeight = default;
        TechnicalDrawingLink = default;
        Quantity = default;
        Product = null;
        Component = null;
        Drive = null;
        Warehouse = null;
        Designer = null;
    }

    void Setup(OrderItemDo oi)
    {
        Index = oi.Index;
        Description = oi.Description;
        Notes = oi.Notes;
        MachineWeight = oi.MachineWeight;
        TechnicalDrawingLink = oi.TechnicalDrawingLink;
        Quantity = oi.Quantity;
        Product = oi.Product;
        Component = oi.Component;
        Drive = oi.Drive;
        Warehouse = oi.Warehouse;
        Designer = oi.Designer;

        if (oi.ComponentsToOrderItem != null)
            foreach (var ctoi in oi.ComponentsToOrderItem)
                Objects.Add(ctoi);
    }

    public override BaseDo GetDo()
    {
        currentOrderItem.LastModified = _appStateService.LoggedUser.Username;
        currentOrderItem.Component = Component;
        currentOrderItem.Product = Product;
        currentOrderItem.Drive = Drive;
        currentOrderItem.Index = Index;
        currentOrderItem.Description = Description;
        currentOrderItem.Notes = Notes;
        currentOrderItem.MachineWeight = MachineWeight;
        currentOrderItem.TechnicalDrawingLink = TechnicalDrawingLink;
        currentOrderItem.Quantity = Quantity;
        currentOrderItem.Warehouse = Warehouse;
        currentOrderItem.Designer = Designer;

        if (currentOrderItem.ComponentsToOrderItem == null)
            currentOrderItem.ComponentsToOrderItem = new List<ComponentToOrderItemDo>();
        foreach (var ctoi in Objects.OfType<ComponentToOrderItemDo>())
            if (!currentOrderItem.ComponentsToOrderItem.Any(x => x == ctoi))
                currentOrderItem.ComponentsToOrderItem.Add(ctoi);

        return currentOrderItem;
    }

    protected async override Task LoadObject()
    {
        Clear();
        Setup(currentOrderItem);
    }
    protected async override Task<bool> SaveNewObject() => false;
    protected async override Task<bool> UpdateExistingObject() => false;
    protected async override Task Save()
    {
        GetDo();
        _navigationService.NavigateTo(typeof(OrderDetailViewModel), true, null);
        var message = WeakReferenceMessenger.Default.Send(new NavigationMessage<OrderDo>(order));
    }
}
