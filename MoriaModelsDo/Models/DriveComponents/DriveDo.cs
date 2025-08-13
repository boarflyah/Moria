using MoriaBaseModels.Models;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.DriveComponents.Relations;

namespace MoriaModelsDo.Models.DriveComponents;
public class DriveDo : BaseDo
{

    private string _Name;
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(value);
        }
    }

    VariatorDo _Variator;
    public VariatorDo Variator
    {
        get => _Variator;
        set
        {
            _Variator = value;
            RaisePropertyChanged(value);
        }
    }

    InverterDo _Inverter;
    public InverterDo Inverter
    {
        get => _Inverter;
        set
        {
            _Inverter = value;
            RaisePropertyChanged(value);
        }
    }

    byte _Quantity;
    public byte Quantity
    {
        get => _Quantity;
        set
        {
            _Quantity = value;
            RaisePropertyChanged(value);
        }
    }

    MotorDo _Motor;
    public MotorDo Motor
    {
        get => _Motor;
        set
        {
            _Motor = value;
            RaisePropertyChanged(value);
        }
    }


    private PumpDo _Pump;
    public PumpDo Pump
    {
        get => _Pump;
        set
        {
            _Pump = value;
            RaisePropertyChanged(value);
        }
    }

    private ExternalCoolingDo _ExternalCooling;
    public ExternalCoolingDo ExternalCooling
    {
        get => _ExternalCooling;
        set
        {
            _ExternalCooling = value;
            RaisePropertyChanged(value);
        }
    }

    private BrakeDo _Brake;
    public BrakeDo Brake
    {
        get => _Brake;
        set
        {
            _Brake = value;
            RaisePropertyChanged(value);
        }
    }

    private SupplementDo _Supplement;
    public SupplementDo Supplement
    {
        get => _Supplement;
        set
        {
            _Supplement = value;
            RaisePropertyChanged(value);
        }
    }

    public IEnumerable<MotorGearToDriveDo> Gearboxes { get; set; } = new List<MotorGearToDriveDo>();

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Name = lookup.Property1;
    }
}
