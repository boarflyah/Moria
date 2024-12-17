using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Contacts;
public class PositionDo: BaseDo
{
    int _Id;
    public int Id
    {
        get => _Id;
        set
        {
            _Id= value;
            RaisePropertyChanged(nameof(Id), value);
        }
    }

    string _Name;
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(nameof(Name), value);
        }
    }

    string _Code;
    public string Code
    {
        get => _Code;
        set
        {
            _Code = value;
            RaisePropertyChanged(nameof(Code), value);
        }
    }
}
