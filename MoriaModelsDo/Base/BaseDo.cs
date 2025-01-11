namespace MoriaModelsDo.Base;
public abstract class BaseDo: BaseNotifyPropertyChanged
{
    bool _IsLocked;
    public bool IsLocked
    {
        get => _IsLocked;
        set
        {
            _IsLocked = value;
            RaisePropertyChanged(value);
        }
    }


    private string _LockedBy;
    public string LockedBy
    {
        get => _LockedBy;
        set
        {
            _LockedBy = value;
            RaisePropertyChanged(value);
        }
    }

    string _LastModified;
    public string LastModified
    {
        get => _LastModified;
        set
        {
            _LastModified = value;
            RaisePropertyChanged(value);
        }
    }
}
