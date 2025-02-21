namespace MoriaModelsDo.Base;

public class PermissionDo : BaseDo
{
    private bool _CanRead;
    public bool CanRead
    {
        get => _CanRead;
        set
        {
            _CanRead = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _CanWrite;
    public bool CanWrite
    {
        get => _CanWrite;
        set
        {
            _CanWrite = value;
            RaisePropertyChanged(value);
        }
    }

    private string _DisplayName;
    public string DisplayName
    {
        get => _DisplayName;
        set
        {
            _DisplayName = value;
            RaisePropertyChanged(value);
        }
    }

    private string _PropertyName;
    public string PropertyName
    {
        get => _PropertyName;
        set
        {
            _PropertyName = value;
            RaisePropertyChanged(value);
        }
    }
}
