using MoriaBaseModels.Models;
using MoriaModelsDo.Base.Enums;

namespace MoriaModelsDo.Base;
public abstract class BaseDo: BaseNotifyPropertyChanged
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

    public SystemChangeType ChangeType
    {
        get; set;
    }

    public virtual void SetObject(LookupModel lookup)
    {
        Id = lookup.Id;
    }
}
