using System.Text.Json.Serialization;
using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.DriveComponents;
public class MotorDo: BaseDo
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

    decimal _Power;
    public decimal Power
    {
        get => _Power;
        set
        {
            _Power = value;
            RaisePropertyChanged(value);
        }
    }


    private int _RPM;
    public int RPM
    {
        get => _RPM;
        set
        {
            _RPM = value;
            RaisePropertyChanged(value);
        }
    }

    [JsonIgnore]
    public string FullName => $"{Name} {Power}kW ({RPM})";

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Symbol = lookup.Property1;
        Name = lookup.Property2;
        decimal.TryParse(lookup.Property3, out _Power);
        var cleaned = new string(lookup.Property4.Where(c => char.IsDigit(c) || c == '-').ToArray());
        int.TryParse(cleaned, out _RPM);
    }
}