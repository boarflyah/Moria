using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Dictionaries;

public class WarehouseDo : BaseDo
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
}
