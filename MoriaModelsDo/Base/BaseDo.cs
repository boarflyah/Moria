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


    private SystemChangeType _ChangeType;
    public SystemChangeType ChangeType
    {
        get => _ChangeType;
        set
        {
            _ChangeType = value;
            RaisePropertyChanged(value);
            RaisePropertyChanged(IsDeleted, nameof(IsDeleted));
        }
    }

    public bool IsDeleted => ChangeType == SystemChangeType.Deleted;

    public virtual void SetObject(LookupModel lookup)
    {
        Id = lookup.Id;
    }
}
