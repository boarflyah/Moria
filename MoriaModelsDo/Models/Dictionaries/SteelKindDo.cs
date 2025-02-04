using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Dictionaries;
public class SteelKindDo : BaseDo
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

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Name = lookup.Property1;
        Symbol = lookup.Property2;
    }
}
