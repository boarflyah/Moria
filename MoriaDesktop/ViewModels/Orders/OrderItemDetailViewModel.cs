using System.Reflection;
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
    

    private bool _PrintedNamePlate;
    [ObjectChangedValidate]
    public bool PrintedNamePlate
    {
        get => _PrintedNamePlate;
        set
        {
            _PrintedNamePlate = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_PrintedNamePlate;
    public PermissionDo Permission_PrintedNamePlate
    {
        get => _Permission_PrintedNamePlate;
        set
        {
            _Permission_PrintedNamePlate = value;
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

    private DateTime? _TechnicalDrawingPlanned;
    [ObjectChangedValidate]
    public DateTime? TechnicalDrawingPlanned
    {
        get => _TechnicalDrawingPlanned;
        set
        {
            _TechnicalDrawingPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_TechnicalDrawingPlanned;
    public PermissionDo Permission_TechnicalDrawingPlanned
    {
        get => _Permission_TechnicalDrawingPlanned;
        set
        {
            _Permission_TechnicalDrawingPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _TechnicalDrawingStarted;
    [ObjectChangedValidate]
    public DateTime? TechnicalDrawingStarted
    {
        get => _TechnicalDrawingStarted;
        set
        {
            _TechnicalDrawingStarted = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_TechnicalDrawingStarted;
    public PermissionDo Permission_TechnicalDrawingStarted
    {
        get => _Permission_TechnicalDrawingStarted;
        set
        {
            _Permission_TechnicalDrawingStarted = value;
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

    private DateTime? _CuttingPlanned;
    [ObjectChangedValidate]
    public DateTime? CuttingPlanned
    {
        get => _CuttingPlanned;
        set
        {
            _CuttingPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_CuttingPlanned;
    public PermissionDo Permission_CuttingPlanned
    {
        get => _Permission_CuttingPlanned;
        set
        {
            _Permission_CuttingPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _CuttingStarted;
    [ObjectChangedValidate]
    public DateTime? CuttingStarted
    {
        get => _CuttingStarted;
        set
        {
            _CuttingStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_CuttingStarted;
    public PermissionDo Permission_CuttingStarted
    {
        get => _Permission_CuttingStarted;
        set
        {
            _Permission_CuttingStarted = value;
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

    private DateTime? _WeldingPlanned;
    [ObjectChangedValidate]
    public DateTime? WeldingPlanned
    {
        get => _WeldingPlanned;
        set
        {
            _WeldingPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_WeldingPlanned;
    public PermissionDo Permission_WeldingPlanned
    {
        get => _Permission_WeldingPlanned;
        set
        {
            _Permission_WeldingPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _WeldingStarted;
    [ObjectChangedValidate]
    public DateTime? WeldingStarted
    {
        get => _WeldingStarted;
        set
        {
            _WeldingStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_WeldingStarted;
    public PermissionDo Permission_WeldingStarted
    {
        get => _Permission_WeldingStarted;
        set
        {
            _Permission_WeldingStarted = value;
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

    private DateTime? _MetalCliningPlanned;
    [ObjectChangedValidate]
    public DateTime? MetalCliningPlanned
    {
        get => _MetalCliningPlanned;
        set
        {
            _MetalCliningPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_MetalCliningPlanned;
    public PermissionDo Permission_MetalCliningPlanned
    {
        get => _Permission_MetalCliningPlanned;
        set
        {
            _Permission_MetalCliningPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _MetalCliningStarted;
    [ObjectChangedValidate]
    public DateTime? MetalCliningStarted
    {
        get => _MetalCliningStarted;
        set
        {
            _MetalCliningStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_MetalCliningStarted;
    public PermissionDo Permission_MetalCliningStarted
    {
        get => _Permission_MetalCliningStarted;
        set
        {
            _Permission_MetalCliningStarted = value;
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

    private DateTime? _PaintingStarted;
    [ObjectChangedValidate]
    public DateTime? PaintingStarted
    {
        get => _PaintingStarted;
        set
        {
            _PaintingStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_PaintingStarted;
    public PermissionDo Permission_PaintingStarted
    {
        get => _Permission_PaintingStarted;
        set
        {
            _Permission_PaintingStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _PaintingPlanned;
    [ObjectChangedValidate]
    public DateTime? PaintingPlanned
    {
        get => _PaintingPlanned;
        set
        {
            _PaintingPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_PaintingPlanned;
    public PermissionDo Permission_PaintingPlanned
    {
        get => _Permission_PaintingPlanned;
        set
        {
            _Permission_PaintingPlanned = value;
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

    private DateTime? _ElectricaCabinetPlanned;
    [ObjectChangedValidate]
    public DateTime? ElectricaCabinetPlanned
    {
        get => _ElectricaCabinetPlanned;
        set
        {
            _ElectricaCabinetPlanned = value;
            RaisePropertyChanged(value);
        }
    }


    private PermissionDo _Permission_ElectricaCabinetPlanned;
    public PermissionDo Permission_ElectricaCabinetPlanned
    {
        get => _Permission_ElectricaCabinetPlanned;
        set
        {
            _Permission_ElectricaCabinetPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _ElectricaCabinetStarted;
    [ObjectChangedValidate]
    public DateTime? ElectricaCabinetStarted
    {
        get => _ElectricaCabinetStarted;
        set
        {
            _ElectricaCabinetStarted = value;
            RaisePropertyChanged(value);
        }
    }


    private PermissionDo _Permission_ElectricaCabinetStarted;
    public PermissionDo Permission_ElectricaCabinetStarted
    {
        get => _Permission_ElectricaCabinetStarted;
        set
        {
            _Permission_ElectricaCabinetStarted = value;
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

    //private DateTime? _MachineAssembledPlanned;
    //[ObjectChangedValidate]
    //public DateTime? MachineAssembledPlanned
    //{
    //    get => _MachineAssembledPlanned;
    //    set
    //    {
    //        _MachineAssembledPlanned = value;
    //        RaisePropertyChanged(value);
    //    }
    //}


    //private PermissionDo _Permission_MachineAssembledPlanned;
    //public PermissionDo Permission_MachineAssembledPlanned
    //{
    //    get => _Permission_MachineAssembledPlanned;
    //    set
    //    {
    //        _Permission_MachineAssembledPlanned = value;
    //        RaisePropertyChanged(value);
    //    }
    //}

    private DateTime? _MachineAssembledStarted;
    [ObjectChangedValidate]
    public DateTime? MachineAssembledStarted
    {
        get => _MachineAssembledStarted;
        set
        {
            _MachineAssembledStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _MachineAssembledAllStarted;
    [ObjectChangedValidate]
    public DateTime? MachineAssembledAllStarted
    {
        get => _MachineAssembledAllStarted;
        set
        {
            _MachineAssembledAllStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _MachineAssembledAllCompleted;
    [ObjectChangedValidate]
    public DateTime? MachineAssembledAllCompleted
    {
        get => _MachineAssembledAllCompleted;
        set
        {
            _MachineAssembledAllCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _PlannedMachineAssembledAll;
    [ObjectChangedValidate]
    public DateTime? PlannedMachineAssembledAll
    {
        get => _PlannedMachineAssembledAll;
        set
        {
            _PlannedMachineAssembledAll = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_MachineAssembledStarted;
    public PermissionDo Permission_MachineAssembledStarted
    {
        get => _Permission_MachineAssembledStarted;
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

    //private DateTime? _MachineWiredAndTestedPlanned;
    //[ObjectChangedValidate]
    //public DateTime? MachineWiredAndTestedPlanned
    //{
    //    get => _MachineWiredAndTestedPlanned;
    //    set
    //    {
    //        _MachineWiredAndTestedPlanned = value;
    //        RaisePropertyChanged(value);
    //    }
    //}

    //private PermissionDo _Permission_MachineWiredAndTestedPlanned;
    //public PermissionDo Permission_MachineWiredAndTestedPlanned
    //{
    //    get => _Permission_MachineWiredAndTestedPlanned;
    //    set
    //    {
    //        _Permission_MachineWiredAndTestedPlanned = value;
    //        RaisePropertyChanged(value);
    //    }
    //}

    private DateTime? _MachineWiredAndTestedStarted;
    [ObjectChangedValidate]
    public DateTime? MachineWiredAndTestedStarted
    {
        get => _MachineWiredAndTestedStarted;
        set
        {
            _MachineWiredAndTestedStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_MachineWiredAndTestedStarted;
    public PermissionDo Permission_MachineWiredAndTestedStarted
    {
        get => _Permission_MachineWiredAndTestedStarted;
        set
        {
            _Permission_MachineWiredAndTestedStarted = value;
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

    private DateTime? _MachineReleasedPlanned;
    [ObjectChangedValidate]
    public DateTime? MachineReleasedPlanned
    {
        get => _MachineReleasedPlanned;
        set
        {
            _MachineReleasedPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_MachineReleasedStarted;
    public PermissionDo Permission_MachineReleasedStarted
    {
        get => _Permission_MachineReleasedStarted;
        set
        {
            _Permission_MachineReleasedStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _MachineReleasedStarted;
    [ObjectChangedValidate]
    public DateTime? MachineReleasedStarted
    {
        get => _MachineReleasedStarted;
        set
        {
            _MachineReleasedStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private PermissionDo _Permission_MachineReleasedPlanned;
    public PermissionDo Permission_MachineReleasedPlanned
    {
        get => _Permission_MachineReleasedPlanned;
        set
        {
            _Permission_MachineReleasedPlanned = value;
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

    private DateTime? _ElectricalDiagramPlanned;
    public DateTime? ElectricalDiagramPlanned
    {
        get => _ElectricalDiagramPlanned;
        set
        {
            _ElectricalDiagramPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _ElectricalDiagramStarted;
    public DateTime? ElectricalDiagramStarted
    {
        get => _ElectricalDiagramStarted;
        set
        {
            _ElectricalDiagramStarted = value;
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
        TechnicalDrawingPlanned = default;
        TechnicalDrawingStarted = default;
        CuttingCompleted = default;
        CuttingPlanned = default;
        CuttingStarted = default;
        WeldingCompleted = default;
        WeldingPlanned = default;
        WeldingStarted = default;
        MetalCliningCompleted = default;
        MetalCliningPlanned = default;
        MetalCliningStarted = default;
        PaintingCompleted = default;
        PaintingPlanned = default;
        PaintingStarted = default;
        ElectricaCabinetCompleted = default;
        ElectricaCabinetPlanned = default;
        ElectricaCabinetStarted = default;
        MachineAssembled = default;
        MachineAssembledStarted = default;
        MachineWiredAndTested = default;
        MachineWiredAndTestedStarted = default;
        MachineReleased = default;
        MachineReleasedStarted = default;
        MachineReleasedPlanned = default;
        TransportOrdered = default;
        PlannedMachineAssembled = default;
        PlannedMachineWiredAndTested = default;
        PlannedTransport = default;
        DueDate = default;
        Electrician = default;
        ElectricalDiagramCompleted = default;
        ElectricaCabinetPlanned = default;
        ElectricaCabinetStarted = default;
        ElectricalCabinet = default;
        ControlCabinetWorkStartDate = default;
        MachineAssembledAllCompleted = default;
        MachineAssembledAllStarted = default;
        PlannedMachineAssembledAll = default;
        PrintedNamePlate = default;

    }

    void Setup(OrderItemDo oi)
    {
        Index = oi.Index;
        Description = oi.Description;
        Notes = oi.Notes;
        MachineWeight = oi.MachineWeight;
        TechnicalDrawingLink = oi.TechnicalDrawingLink;
        Quantity = oi.Quantity;
        PrintedNamePlate = oi.PrintedNamePlate;
        Product = oi.Product;
        Component = oi.Component;
        Drive = oi.Drive;
        Warehouse = oi.Warehouse;
        Designer = oi.Designer;
        Power = oi.Power;
        MainColor = oi.MainColor;
        SecondColor = oi.DetailsColor;
        TechnicalDrawingCompleted = oi.TechnicalDrawingCompleted;
        TechnicalDrawingPlanned = oi.TechnicalDrawingPlanned;
        TechnicalDrawingStarted = oi.TechnicalDrawingStarted;
        WeldingCompleted = oi.WeldingCompleted;
        WeldingPlanned = oi.WeldingPlanned;
        WeldingStarted = oi.WeldingStarted;
        CuttingCompleted = oi.CuttingCompleted;
        CuttingPlanned = oi.CuttingPlanned;
        CuttingStarted = oi.CuttingStarted;
        MetalCliningCompleted = oi.MetalCliningCompleted;
        MetalCliningPlanned = oi.MetalCliningPlanned;
        MetalCliningStarted = oi.MetalCliningStarted;
        PaintingCompleted = oi.PaintingCompleted;
        PaintingPlanned = oi.PaintingPlanned;
        PaintingStarted = oi.PaintingStarted;
        ElectricaCabinetCompleted = oi.ElectricaCabinetCompleted;
        ElectricaCabinetPlanned = oi.ElectricaCabinetPlanned;
        ElectricaCabinetStarted = oi.ElectricaCabinetStarted;
        MachineAssembled = oi.MachineAssembled;
        MachineAssembledStarted = oi.MachineAssembledStarted;
        MachineAssembledAllCompleted = oi.MachineAssembledAllCompleted;
        MachineAssembledAllStarted = oi.MachineAssembledAllStarted;
        PlannedMachineAssembledAll = oi.PlannedMachineAssembledAll;
        MachineWiredAndTested = oi.MachineWiredAndTested;
        MachineWiredAndTestedStarted = oi.MachineWiredAndTestedStarted;
        MachineReleased = oi.MachineReleased;
        MachineReleasedStarted = oi.MachineReleasedStarted;
        MachineReleasedPlanned = oi.MachineReleasedPlanned;
        TransportOrdered = oi.TransportOrdered;
        PlannedMachineAssembled = oi.PlannedMachineAssembled;
        PlannedMachineWiredAndTested = oi.PlannedMachineWiredAndTested;
        PlannedTransport = oi.PlannedTransport;
        ElectricalCabinet = oi.ElectricalCabinet;
        Electrician = oi.Electrician;
        ElectricalDiagramCompleted = oi.ElectricalDiagramCompleted;
        ElectricalDiagramPlanned = oi.ElectricalDiagramPlanned;
        ElectricalDiagramStarted = oi.ElectricalDiagramStarted;
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
        currentOrderItem.TechnicalDrawingPlanned = TechnicalDrawingPlanned;
        currentOrderItem.TechnicalDrawingStarted = TechnicalDrawingStarted;
        currentOrderItem.WeldingCompleted = WeldingCompleted;
        currentOrderItem.WeldingPlanned = WeldingPlanned;
        currentOrderItem.WeldingStarted = WeldingStarted;
        currentOrderItem.CuttingCompleted = CuttingCompleted;
        currentOrderItem.CuttingPlanned = CuttingPlanned;
        currentOrderItem.CuttingStarted = CuttingStarted;
        currentOrderItem.MetalCliningCompleted = MetalCliningCompleted;
        currentOrderItem.MetalCliningPlanned = MetalCliningPlanned;
        currentOrderItem.MetalCliningStarted = MetalCliningStarted;
        currentOrderItem.PaintingCompleted = PaintingCompleted;
        currentOrderItem.PaintingPlanned  = PaintingPlanned;
        currentOrderItem.PaintingStarted = PaintingStarted;
        currentOrderItem.ElectricaCabinetCompleted = ElectricaCabinetCompleted;
        currentOrderItem.ElectricaCabinetPlanned = ElectricaCabinetPlanned;
        currentOrderItem.ElectricaCabinetStarted = ElectricaCabinetStarted;
        currentOrderItem.MachineAssembled = MachineAssembled;
        currentOrderItem.MachineAssembledStarted = MachineAssembledStarted;
        currentOrderItem.MachineWiredAndTested = MachineWiredAndTested;
        currentOrderItem.MachineWiredAndTestedStarted = MachineWiredAndTestedStarted;
        currentOrderItem.MachineReleased = MachineReleased;
        currentOrderItem.MachineReleasedPlanned = MachineReleasedPlanned;
        currentOrderItem.MachineReleasedStarted = MachineReleasedStarted;
        currentOrderItem.TransportOrdered = TransportOrdered;
        currentOrderItem.PlannedTransport = PlannedTransport;
        currentOrderItem.PlannedMachineAssembled = PlannedMachineAssembled;
        currentOrderItem.PlannedMachineWiredAndTested = PlannedMachineWiredAndTested;
        currentOrderItem.MachineAssembledAllStarted = MachineAssembledAllStarted;
        currentOrderItem.MachineAssembledAllCompleted = MachineAssembledAllCompleted;
        currentOrderItem.PlannedMachineAssembledAll = PlannedMachineAssembledAll;
        currentOrderItem.PrintedNamePlate = PrintedNamePlate;
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
