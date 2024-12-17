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
            RaisePropertyChanged(nameof(IsLocked), value);
        }
    }

    string _LastModified;
    public string LastModified
    {
        get => _LastModified;
        set
        {
            _LastModified = value;
            RaisePropertyChanged(nameof(LastModified), value);
        }
    }
}
