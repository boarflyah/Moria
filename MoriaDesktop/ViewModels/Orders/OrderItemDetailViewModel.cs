﻿using System.Reflection;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using MoriaBaseServices;
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
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents.Relations;
using MoriaModelsDo.Models.Orders;
using MoriaModelsDo.Models.Orders.Relations;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Orders;
public class OrderItemDetailViewModel : BaseDetailWithNestedListViewModel
{
    readonly IApiProductService _productService;

    public OrderItemDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, 
        INavigationService navigationService, IKeepAliveWorker worker, IApiProductService productService)
        : base(logger, appStateService, apiLockService, navigationService, worker)
    {
        _productService = productService;

        UpdateDrivesCommand = new(UpdateDrives, CanUpdateDrives);
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

    private PermissionDo _Permission_Index;
    public PermissionDo Permission_Index
    {
        get => _Permission_Index;
        set
        {
            _Permission_Index = value;
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


    private PermissionDo _Permission_Description;
    public PermissionDo Permission_Description
    {
        get => _Permission_Description;
        set
        {
            _Permission_Description = value;
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

    private PermissionDo _Permission_Notes;
    public PermissionDo Permission_Notes
    {
        get => _Permission_Notes;
        set
        {
            _Permission_Notes = value;
            RaisePropertyChanged(value);
        }
    }

    private int _MachineWeight;
    [ObjectChangedValidate]
    public int MachineWeight
    {
        get => _MachineWeight;
        set
        {
            _MachineWeight = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_MachineWeight;
    public PermissionDo Permission_MachineWeight
    {
        get => _Permission_MachineWeight;
        set
        {
            _Permission_MachineWeight = value;
            RaisePropertyChanged(value);
        }
    }

    private string _ProductionYear;
    [ObjectChangedValidate]
    public string ProductionYear
    {
        get => _ProductionYear;
        set
        {
            _ProductionYear = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_ProductionYear;
    public PermissionDo Permission_ProductionYear
    {
        get => _Permission_ProductionYear;
        set
        {
            _Permission_ProductionYear = value;
            RaisePropertyChanged(value);
        }
    }

    private string _SerialNumber;
    [ObjectChangedValidate]
    public string SerialNumber
    {
        get => _SerialNumber;
        set
        {
            _SerialNumber = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_SerialNumber;
    public PermissionDo Permission_SerialNumber
    {
        get => _Permission_SerialNumber;
        set
        {
            _Permission_SerialNumber = value;
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

    private PermissionDo _Permission_TechnicalDrawingLink;
    public PermissionDo Permission_TechnicalDrawingLink
    {
        get => _Permission_TechnicalDrawingLink;
        set
        {
            _Permission_TechnicalDrawingLink = value;
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

    private PermissionDo _Permission_Quantity;
    public PermissionDo Permission_Quantity
    {
        get => _Permission_Quantity;
        set
        {
            _Permission_Quantity = value;
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
            UpdateDrivesCommand.NotifyCanExecuteChanged();
            if (value != null)
            {
                Component = null;
                Drive = null;
            }
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_Product;
    public PermissionDo Permission_Product
    {
        get => _Permission_Product;
        set
        {
            _Permission_Product = value;
            RaisePropertyChanged(value);
        }
    }

    private ColorDo _MainColor;
    [ObjectChangedValidate]
    public ColorDo MainColor
    {
        get => _MainColor;
        set
        {
            _MainColor = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_MainColor;
    public PermissionDo Permission_MainColor
    {
        get => _Permission_MainColor;
        set
        {
            _Permission_MainColor = value;
            RaisePropertyChanged(value);
        }
    }

    private ColorDo _SecondColor;
    [ObjectChangedValidate]
    public ColorDo SecondColor
    {
        get => _SecondColor;
        set
        {
            _SecondColor = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_SecondColor;
    public PermissionDo Permission_SecondColor
    {
        get => _Permission_SecondColor;
        set
        {
            _Permission_SecondColor = value;
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

    private PermissionDo _Permission_Component;
    public PermissionDo Permission_Component
    {
        get => _Permission_Component;
        set
        {
            _Permission_Component = value;
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

    private PermissionDo _Permission_Drive;
    public PermissionDo Permission_Drive
    {
        get => _Permission_Drive;
        set
        {
            _Permission_Drive = value;
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

    private PermissionDo _Permission_Warehouse;
    public PermissionDo Permission_Warehouse
    {
        get => _Permission_Warehouse;
        set
        {
            _Permission_Warehouse = value;
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

    private PermissionDo _Permission_Designer;
    public PermissionDo Permission_Designer
    {
        get => _Permission_Designer;
        set
        {
            _Permission_Designer = value;
            RaisePropertyChanged(value);
        }
    }

    decimal _Power;
    [ObjectChangedValidate]
    public decimal Power
    {
        get => _Power;
        set
        {
            _Power = value;
            RaisePropertyChanged(value);
        }
    }
    private PermissionDo _Permission_Power;
    public PermissionDo Permission_Power
    {
        get => _Permission_Power;
        set
        {
            _Permission_Power = value;
            RaisePropertyChanged(value);
        }
    }

    // Rysunek techniczny
    private DateTime? _TechnicalDrawingCompleted;
    [ObjectChangedValidate]
    public DateTime? TechnicalDrawingCompleted
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

    private DateTime? _CuttingCompleted;
    [ObjectChangedValidate]
    public DateTime? CuttingCompleted
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

    private DateTime? _WeldingCompleted;
    [ObjectChangedValidate]
    public DateTime? WeldingCompleted
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

    private DateTime? _MetalCliningCompleted;
    [ObjectChangedValidate]
    public DateTime? MetalCliningCompleted
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

    private DateTime? _PaintingCompleted;
    [ObjectChangedValidate]
    public DateTime? PaintingCompleted
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

    private DateTime? _ElectricaCabinetCompleted;
    [ObjectChangedValidate]
    public DateTime? ElectricaCabinetCompleted
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

    private DateTime? _MachineAssembled;
    [ObjectChangedValidate]
    public DateTime? MachineAssembled
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

    private DateTime? _MachineWiredAndTested;
    [ObjectChangedValidate]
    public DateTime? MachineWiredAndTested
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

    private DateTime? _MachineReleased;
    [ObjectChangedValidate]
    public DateTime? MachineReleased
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

    private DateTime? _TransportOrdered;
    [ObjectChangedValidate]
    public DateTime? TransportOrdered
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

    private DateTime? _PlannedMachineAssembled;
    [ObjectChangedValidate]
    public DateTime? PlannedMachineAssembled
    {
        get => _PlannedMachineAssembled;
        set
        {
            _PlannedMachineAssembled = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_PlannedMachineAssembled;
    public PermissionDo Permission_PlannedMachineAssembled
    {
        get => _Permission_PlannedMachineAssembled;
        set
        {
            _Permission_PlannedMachineAssembled = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _PlannedMachineWiredAndTested;
    [ObjectChangedValidate]
    public DateTime? PlannedMachineWiredAndTested
    {
        get => _PlannedMachineWiredAndTested;
        set
        {
            _PlannedMachineWiredAndTested = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_PlannedMachineWiredAndTested;
    public PermissionDo Permission_PlannedMachineWiredAndTested
    {
        get => _Permission_PlannedMachineWiredAndTested;
        set
        {
            _Permission_PlannedMachineWiredAndTested = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _PlannedTransport;
    [ObjectChangedValidate]
    public DateTime? PlannedTransport
    {
        get => _PlannedTransport;
        set
        {
            _PlannedTransport = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_PlannedTransport;
    public PermissionDo Permission_PlannedTransport
    {
        get => _Permission_PlannedTransport;
        set
        {
            _Permission_PlannedTransport = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime _DueDate;
    [ObjectChangedValidate]
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

    private EmployeeDo _Electrician;
    public EmployeeDo Electrician
    {
        get => _Electrician;
        set
        {
            _Electrician = value;
            RaisePropertyChanged(value);
        }
    }


    private DateTime? _ControlCabinetWorkStartDate;
    public DateTime? ControlCabinetWorkStartDate
    {
        get => _ControlCabinetWorkStartDate;
        set
        {
            _ControlCabinetWorkStartDate = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _ElectricalDiagramCompleted;
    public DateTime? ElectricalDiagramCompleted
    {
        get => _ElectricalDiagramCompleted;
        set
        {
            _ElectricalDiagramCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private ElectricalCabinetDo _ElectricalCabinet;
    [ObjectChangedValidate]
    public ElectricalCabinetDo ElectricalCabinet
    {
        get => _ElectricalCabinet;
        set
        {
            _ElectricalCabinet = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region commands

    public AsyncRelayCommand UpdateDrivesCommand
    {
        get;
        init;
    }

    #endregion

    #region commands methods

    async Task UpdateDrives()
    {
        if (await _appStateService.ConfirmAsync("Zdefiniowane napędy zostaną zastąpione nowymi, wygenerowanymi na podstawie wybranego produktu.\r\n" +
            "Czy chcesz kontynuować?") == true)
        {
            if (Objects != null)
                foreach (var drive in Objects.OfType<ComponentToOrderItemDo>())
                {
                    drive.ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Deleted;
                    HasObjectChanged = true;
                }

            var ctois = await _productService.GetProductDrives(_appStateService.LoggedUser.Username, Product.Id);

            foreach (var ctoi in ctois)
            {
                Objects.Add(ctoi);
                HasObjectChanged = true;
            }

            //TODO pobrac z bazy gotowe obiekty ComponentToOrderItemDo - utworzone na podstawie produktu wskazanego w pozycji zamowienia i przypisac do kolekcji objects z changetype = added
        }
    }

    bool CanUpdateDrives() => Product != null && !IsLocked;

    #endregion

    #region nestedlistview

    protected override string GetObjectsListViewTitle() => "Napędy";

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
        if (parameters.Length > 2)
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

            if (parameters.FirstOrDefault(x => x is bool) is bool b)
                IsLocked = b;
            else
                IsLocked = true;
        }
    }

    public async override Task<bool> OnNavigatingFrom()
    {
        //_appStateService.CurrentDetailViewObjectId = 0;
        //_appStateService.CurrentDetailViewObjectType = null;

        var message = WeakReferenceMessenger.Default.Send(new NavigationMessage<(OrderDo order, bool isLocked)>(new(order, IsLocked)));

        return true;
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
        MainColor = default;
        SecondColor = default;
        Power = default;
        TechnicalDrawingCompleted = default;
        CuttingCompleted = default;
        WeldingCompleted = default;
        MetalCliningCompleted = default;
        PaintingCompleted = default;
        ElectricaCabinetCompleted = default;
        MachineAssembled = default;
        MachineWiredAndTested = default;
        MachineReleased = default;
        TransportOrdered = default;
        PlannedMachineAssembled = default;
        PlannedMachineWiredAndTested = default;
        PlannedTransport = default;
        DueDate = default;
        Electrician = default;
        ElectricalDiagramCompleted = default;
        ElectricalCabinet = default;
        ControlCabinetWorkStartDate = default;

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
        Power = oi.Power;
        MainColor = oi.MainColor;
        SecondColor = oi.DetailsColor;
        TechnicalDrawingCompleted = oi.TechnicalDrawingCompleted;
        WeldingCompleted = oi.WeldingCompleted;
        CuttingCompleted = oi.CuttingCompleted;
        MetalCliningCompleted = oi.MetalCliningCompleted;
        PaintingCompleted = oi.PaintingCompleted;
        ElectricaCabinetCompleted = oi.ElectricaCabinetCompleted;
        MachineAssembled = oi.MachineAssembled;
        MachineWiredAndTested = oi.MachineWiredAndTested;
        MachineReleased = oi.MachineReleased;
        TransportOrdered = oi.TransportOrdered;
        PlannedMachineAssembled = oi.PlannedMachineAssembled;
        PlannedMachineWiredAndTested = oi.PlannedMachineWiredAndTested;
        PlannedTransport = oi.PlannedTransport;
        ElectricalCabinet = oi.ElectricalCabinet;
        Electrician = oi.Electrician;
        ElectricalDiagramCompleted = oi.ElectricalDiagramCompleted;
        ControlCabinetWorkStartDate = oi.ControlCabinetWorkStartDate;
        DueDate = oi.DueDate == DateTime.MinValue ? DateTime.Now : oi.DueDate;
        ProductionYear = oi.ProductionYear == null ? DateTime.Now.Year.ToString() : oi.ProductionYear;
        SerialNumber = oi.SerialNumber;

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
        currentOrderItem.Power = Power;
        currentOrderItem.MainColor = MainColor;
        currentOrderItem.DetailsColor = SecondColor;
        currentOrderItem.TechnicalDrawingCompleted = TechnicalDrawingCompleted;
        currentOrderItem.WeldingCompleted = WeldingCompleted;
        currentOrderItem.CuttingCompleted = CuttingCompleted;
        currentOrderItem.MetalCliningCompleted = MetalCliningCompleted;
        currentOrderItem.PaintingCompleted = PaintingCompleted;
        currentOrderItem.ElectricaCabinetCompleted = ElectricaCabinetCompleted;
        currentOrderItem.MachineAssembled = MachineAssembled;
        currentOrderItem.MachineWiredAndTested = MachineWiredAndTested;
        currentOrderItem.MachineReleased = MachineReleased;
        currentOrderItem.TransportOrdered = TransportOrdered;
        currentOrderItem.PlannedTransport = PlannedTransport;
        currentOrderItem.PlannedMachineAssembled = PlannedMachineAssembled;
        currentOrderItem.PlannedMachineWiredAndTested = PlannedMachineWiredAndTested;
        currentOrderItem.DueDate = DueDate;
        currentOrderItem.ProductionYear = ProductionYear;
        currentOrderItem.SerialNumber = SerialNumber;

        if (currentOrderItem.ComponentsToOrderItem == null)
            currentOrderItem.ComponentsToOrderItem = new List<ComponentToOrderItemDo>();
        foreach (var ctoi in Objects.OfType<ComponentToOrderItemDo>())
            if (!currentOrderItem.ComponentsToOrderItem.Any(x => x == ctoi))
                currentOrderItem.ComponentsToOrderItem.Add(ctoi);

        return currentOrderItem;
    }

    protected async override Task LoadObject()
    {
    }

    public async override Task Load()
    {
        PropertyChanged -= BaseDetailViewModel_PropertyChanged;
        try
        {
            Clear();
            Setup(currentOrderItem);

            var property = GetType().GetProperties().FirstOrDefault(x => x.GetCustomAttribute<DefaultPropertyAttribute>() != null);
            if (property == null)
                property = GetType().GetProperties().FirstOrDefault();

            _appStateService.SetupTitle(property?.GetValue(this)?.ToString() ?? "");
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
        }
        catch (Exception ex)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
        }
        finally
        {
            PropertyChanged += BaseDetailViewModel_PropertyChanged;
        }
    }

    protected async override Task<bool> SaveNewObject() => false;
    protected async override Task<bool> UpdateExistingObject() => false;
    protected async override Task Save()
    {
        GetDo();
        _navigationService.NavigateTo(typeof(OrderDetailViewModel), true, null);
    }

    protected override Type GetOnCloseNavigationTarget() => typeof(OrderDetailViewModel);
}
