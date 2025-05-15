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

    public string ItemName => Product != null ? Product.Name : Component != null ? Component.Name : Drive?.Name;

    public IList<ComponentToOrderItemDo> ComponentsToOrderItem
    {
        get; set;
    } = new List<ComponentToOrderItemDo>();
}
