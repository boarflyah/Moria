using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Dictionaries;
public class ColorDo: BaseDo
{
    int _Id;
    public int Id
    {
        get => _Id;
        set
        {
            _Id = value;
            RaisePropertyChanged(value);
        }
    }

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

    private string _Code;
    public string Code
    {
        get => _Code;
        set
        {
            _Code = value;
            RaisePropertyChanged(value);
        }
    }
}
