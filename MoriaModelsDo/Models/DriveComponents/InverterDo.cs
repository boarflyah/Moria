using System.Text.Json.Serialization;
using MoriaBaseModels.Models;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.DriveComponents;

public class InverterDo : BaseDo
{
    private string _Type;
    [ObjectChangedValidate]
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

    [JsonIgnore]
    public string FullName => $"{Type} {Power.ToString()}";

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Type = lookup.Property1;
        decimal.TryParse(lookup.Property2, out _Power);
    }
}
