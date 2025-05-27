using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;

namespace MoriaModelsDo.Models.Orders;
public class OrderDo: BaseDo
{
    private string _OrderNumberSymbol;
    public string OrderNumberSymbol
    {
        get => _OrderNumberSymbol;
        set
        {
            _OrderNumberSymbol = value;
            RaisePropertyChanged(value);
        }
    }

    private string _Remarks;
    public string Remarks
    {
        get => _Remarks;
        set
        {
            _Remarks = value;
            RaisePropertyChanged(value);
        }
    }

    private string _OfferNumber;
    public string OfferNumber
    {
        get => _OfferNumber;
        set
        {
            _OfferNumber = value;
            RaisePropertyChanged(value);
        }
    }

    private string _CatalogLink;
    public string CatalogLink
    {
        get => _CatalogLink;
        set
        {
            _CatalogLink = value;
            RaisePropertyChanged(value);
        }
    }

    private string _SalesOfferLink;
    public string SalesOfferLink
    {
        get => _SalesOfferLink;
        set
        {
            _SalesOfferLink = value;
            RaisePropertyChanged(value);
        }
    }

    private string _ClientSymbol;
    public string ClientSymbol
    {
        get => _ClientSymbol;
        set
        {
            _ClientSymbol = value;
            RaisePropertyChanged(value);
        }
    }

    private ContactDo _OrderingContact;
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
    public ContactDo ReceivingContact
    {
        get => _ReceivingContact;
        set
        {
            _ReceivingContact = value;
            RaisePropertyChanged(value);
        }
    }

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

    private bool _ProductionOrderSymbol;
    public bool ProductionOrderSymbol
    {
        get => _ProductionOrderSymbol;
        set
        {
            _ProductionOrderSymbol = value;
            RaisePropertyChanged(value);
        }
    }


    public DateTime DueDate => OrderItems.Count != 0 ? OrderItems.Max(x => x.DueDate): DateTime.MinValue;



    public IList<OrderItemDo> OrderItems
    {
        get; set;
    } = new List<OrderItemDo>();
}
