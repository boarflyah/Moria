using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Contacts;
public class ContactDo : BaseDo
{
    string _ShortName;
    public string ShortName
    {
        get => _ShortName;
        set
        {
            _ShortName = value;
            RaisePropertyChanged(value);
        }
    }

    string _LongName;
    public string LongName
    {
        get => _LongName;
        set
        {
            _LongName = value;
            RaisePropertyChanged(value);
        }
    }

    string _Symbol;
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
        Symbol = lookup.Property1;
        ShortName = lookup.Property2;
        LongName = lookup.Property3;
    }
}
