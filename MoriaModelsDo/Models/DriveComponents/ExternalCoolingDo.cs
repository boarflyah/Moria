using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.DriveComponents;

public class ExternalCoolingDo : BaseDo
{
    private string _Type;
    public string Type
    {
        get => _Type;
        set
        {
            _Type = value;
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

    public string FullName => $"{Type} {Power}";

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Type = lookup.Property1;
        decimal.TryParse(lookup.Property2, out _Power);
    }
}
