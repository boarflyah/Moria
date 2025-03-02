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

    bool _Variator;
    public bool Variator
    {
        get => _Variator;
        set
        {
            _Variator = value;
            RaisePropertyChanged(value);
        }
    }

    bool _Inverter;
    public bool Inverter
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

    public IEnumerable<MotorGearToDriveDo> Gearboxes { get; set; } = new List<MotorGearToDriveDo>();

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Name = lookup.Property1;
    }
}
