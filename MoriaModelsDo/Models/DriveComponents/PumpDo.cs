using System.Text.Json.Serialization;
using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.DriveComponents;

public class PumpDo : BaseDo
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
    
    private string _Size;
    public string Size
    {
        get => _Size;
        set
        {
            _Size = value;
            RaisePropertyChanged(value);
        }
    }

    private string _IProperty;
    public string IProperty
    {
        get => _IProperty;
        set
        {
            _IProperty = value;
            RaisePropertyChanged(value);
        }
    }

    [JsonIgnore]
    public string FullName => $"{Type} {Size} {IProperty}";

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Type = lookup.Property1;
        Size = lookup.Property2;
        IProperty = lookup.Property3;
    }
}
