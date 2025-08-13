using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.Orders.Relations;
using MoriaModelsDo.Models.Products;

namespace MoriaModelsDo.Models.Orders;
public class OrderItemDo: BaseDo
{
    private int _Index;
    public int Index
    {
        get => _Index;
        set
        {
            _Index = value;
            RaisePropertyChanged(value);
        }
    }

    private string _Symbol;
    public string Symbol
    {
        get => _Symbol;
        set
        {
            _Symbol = value;
            RaisePropertyChanged(value);
        }
    }

    private string _Description;
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
    public string Notes
    {
        get => _Notes;
        set
        {
            _Notes = value;
            RaisePropertyChanged(value);
        }
    }

    private string _ElectricialDescription;
    public string ElectricialDescription
    {
        get => _ElectricialDescription;
        set
        {
            _ElectricialDescription = value;
            RaisePropertyChanged(value);
        }
    }


    private int _MachineWeight;
    public int MachineWeight
    {
        get => _MachineWeight;
        set
        {
            _MachineWeight = value;
            RaisePropertyChanged(value);
        }
    }

    private decimal _Power;
    public decimal Power
    {
        get => _Power;
        set
        {
            _Power = value;
            RaisePropertyChanged(value);
        }
    }

    private string _SerialNumber;
    public string SerialNumber
    {
        get => _SerialNumber;
        set
        {
            _SerialNumber = value;
            RaisePropertyChanged(value);
        }
    }


    private string _ProductionYear;
    public string ProductionYear
    {
        get => _ProductionYear;
        set
        {
            _ProductionYear = value;
            RaisePropertyChanged(value);
        }
    }

    private string _TechnicalDrawingLink;
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
    public ProductDo Product
    {
        get => _Product;
        set
        {
            _Product = value;
            RaisePropertyChanged(value);
        }
    }

    private ColorDo _MainColor;
    public ColorDo MainColor
    {
        get => _MainColor;
        set
        {
            _MainColor = value;
            RaisePropertyChanged(value);
        }
    }


    private ColorDo _DetailsColor;
    public ColorDo DetailsColor
    {
        get => _DetailsColor;
        set
        {
            _DetailsColor = value;
            RaisePropertyChanged(value);
        }
    }

    private ComponentDo _Component;
    public ComponentDo Component
    {
        get => _Component;
        set
        {
            _Component = value;
            RaisePropertyChanged(value);
        }
    }

    private DriveDo _Drive;
    public DriveDo Drive
    {
        get => _Drive;
        set
        {
            _Drive = value;
            RaisePropertyChanged(value);
        }
    }

    private WarehouseDo _Warehouse;
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
    public EmployeeDo Designer
    {
        get => _Designer;
        set
        {
            _Designer = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _TechnicalDrawingCompleted;
    public DateTime? TechnicalDrawingCompleted
    {
        get => _TechnicalDrawingCompleted;
        set
        {
            _TechnicalDrawingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _TechnicalDrawingStarted;
    public DateTime? TechnicalDrawingStarted
    {
        get => _TechnicalDrawingStarted;
        set
        {
            _TechnicalDrawingStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _TechnicalDrawingPlanned;
    public DateTime? TechnicalDrawingPlanned
    {
        get => _TechnicalDrawingPlanned;
        set
        {
            _TechnicalDrawingPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _CuttingCompleted;
    public DateTime? CuttingCompleted
    {
        get => _CuttingCompleted;
        set
        {
            _CuttingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _CuttingPlanned;
    public DateTime? CuttingPlanned
    {
        get => _CuttingPlanned;
        set
        {
            _CuttingPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _CuttingStarted;
    public DateTime? CuttingStarted
    {
        get => _CuttingStarted;
        set
        {
            _CuttingStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _WeldingCompleted;
    public DateTime? WeldingCompleted
    {
        get => _WeldingCompleted;
        set
        {
            _WeldingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _WeldingPlanned;
    public DateTime? WeldingPlanned
    {
        get => _WeldingPlanned;
        set
        {
            _WeldingPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _WeldingStarted;
    public DateTime? WeldingStarted
    {
        get => _WeldingStarted;
        set
        {
            _WeldingStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _MetalCliningCompleted;
    public DateTime? MetalCliningCompleted
    {
        get => _MetalCliningCompleted;
        set
        {
            _MetalCliningCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _MetalCliningPlanned;
    public DateTime? MetalCliningPlanned
    {
        get => _MetalCliningPlanned;
        set
        {
            _MetalCliningPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _MetalCliningStarted;
    public DateTime? MetalCliningStarted
    {
        get => _MetalCliningStarted;
        set
        {
            _MetalCliningStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _PaintingCompleted;
    public DateTime? PaintingCompleted
    {
        get => _PaintingCompleted;
        set
        {
            _PaintingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _PaintingPlanned;
    public DateTime? PaintingPlanned
    {
        get => _PaintingPlanned;
        set
        {
            _PaintingPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _PaintingStarted;
    public DateTime? PaintingStarted
    {
        get => _PaintingStarted;
        set
        {
            _PaintingStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _ElectricaCabinetCompleted;
    public DateTime? ElectricaCabinetCompleted
    {
        get => _ElectricaCabinetCompleted;
        set
        {
            _ElectricaCabinetCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _ElectricaCabinetPlanned;
    public DateTime? ElectricaCabinetPlanned
    {
        get => _ElectricaCabinetPlanned;
        set
        {
            _ElectricaCabinetPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _ElectricaCabinetStarted;
    public DateTime? ElectricaCabinetStarted
    {
        get => _ElectricaCabinetStarted;
        set
        {
            _ElectricaCabinetStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _MachineAssembled;
    public DateTime? MachineAssembled
    {
        get => _MachineAssembled;
        set
        {
            _MachineAssembled = value;
            RaisePropertyChanged(value);
        }
    }

    //private DateTime? _MachineAssembledPlanned;
    //public DateTime? MachineAssembledPlanned
    //{
    //    get => _MachineAssembledPlanned;
    //    set
    //    {
    //        _MachineAssembledPlanned = value;
    //        RaisePropertyChanged(value);
    //    }
    //}

    private DateTime? _MachineAssembledStarted;
    public DateTime? MachineAssembledStarted
    {
        get => _MachineAssembledStarted;
        set
        {
            _MachineAssembledStarted = value;
            RaisePropertyChanged(value);
        }
    }


    private DateTime? _MachineWiredAndTested;
    public DateTime? MachineWiredAndTested
    {
        get => _MachineWiredAndTested;
        set
        {
            _MachineWiredAndTested = value;
            RaisePropertyChanged(value);
        }
    }

    //private DateTime? _MachineWiredAndTestedPlanned;
    //public DateTime? MachineWiredAndTestedPlanned
    //{
    //    get => _MachineWiredAndTestedPlanned;
    //    set
    //    {
    //        _MachineWiredAndTestedPlanned = value;
    //        RaisePropertyChanged(value);
    //    }
    //}

    private DateTime? _MachineWiredAndTestedStarted;
    public DateTime? MachineWiredAndTestedStarted
    {
        get => _MachineWiredAndTestedStarted;
        set
        {
            _MachineWiredAndTestedStarted = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _MachineReleased;
    public DateTime? MachineReleased
    {
        get => _MachineReleased;
        set
        {
            _MachineReleased = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _MachineReleasedPlanned;
    public DateTime? MachineReleasedPlanned
    {
        get => _MachineReleasedPlanned;
        set
        {
            _MachineReleasedPlanned = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _MachineReleasedStarted;
    public DateTime? MachineReleasedStarted
    {
        get => _MachineReleasedStarted;
        set
        {
            _MachineReleasedStarted = value;
            RaisePropertyChanged(value);
        }
    }


    private DateTime? _TransportOrdered;
    public DateTime? TransportOrdered
    {
        get => _TransportOrdered;
        set
        {
            _TransportOrdered = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _ProductionOrderSymbol;
    public DateTime? ProductionOrderSymbol
    {
        get => _ProductionOrderSymbol;
        set
        {
            _ProductionOrderSymbol = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _PlannedTransport;
    public DateTime? PlannedTransport
    {
        get => _PlannedTransport;
        set
        {
            _PlannedTransport = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _PlannedMachineWiredAndTested;
    public DateTime? PlannedMachineWiredAndTested
    {
        get => _PlannedMachineWiredAndTested;
        set
        {
            _PlannedMachineWiredAndTested = value;
            RaisePropertyChanged(value);
        }
    }

    private DateTime? _PlannedMachineAssembled;
    public DateTime? PlannedMachineAssembled
    {
        get => _PlannedMachineAssembled;
        set
        {
            _PlannedMachineAssembled = value;
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

    //private DateTime? _ControlCabinetWorkEndDate;
    //public DateTime? ControlCabinetWorkEndDate
    //{
    //    get => _ControlCabinetWorkEndDate;
    //    set
    //    {
    //        _ControlCabinetWorkEndDate = value;
    //        RaisePropertyChanged(value);
    //    }
    //}

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

    private ElectricalCabinetDo _ElectricalCabinet;
    public ElectricalCabinetDo ElectricalCabinet
    {
        get => _ElectricalCabinet;
        set
        {
            _ElectricalCabinet = value;
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


    private OrderDo _Order;
    public OrderDo Order
    {
        get => _Order;
        set
        {
            _Order = value;
            RaisePropertyChanged(value);
        }
    }

    public string ItemName => Product != null ? Product.Name : Component != null ? Component.Name : Drive?.Name;

    public string ItemSymbol => Product != null ? Product.Symbol : Drive != null ? Drive.Motor.Symbol : string.Empty;

    public IList<ComponentToOrderItemDo> ComponentsToOrderItem
    {
        get; set;
    } = new List<ComponentToOrderItemDo>();
}
