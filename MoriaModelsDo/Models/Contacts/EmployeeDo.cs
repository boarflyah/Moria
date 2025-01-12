using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Contacts;
public class EmployeeDo: BaseDo
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

    string _FirstName;
    public string FirstName
    {
        get => _FirstName;
        set
        {
            _FirstName = value;
            RaisePropertyChanged(value);
        }
    }

    string _LastName;
    public string LastName
    {
        get => _LastName;
        set
        {
            _LastName = value;
            RaisePropertyChanged(value);
        }
    }

    string _UserName;
    public string Username
    {
        get => _UserName; 
        set
        {
            _UserName= value;
            RaisePropertyChanged(value);
        }
    }

    string? _Password;
    public string? Password
    {
        get => _Password;
        set
        {
            _Password = value;
            RaisePropertyChanged(value);
        }
    }

    string _PhoneNumber;
    public string PhoneNumber
    {
        get => _PhoneNumber;
        set
        {
         _PhoneNumber= value;
            RaisePropertyChanged(value);
        }
    }

    PositionDo _Position;
    public PositionDo Position
    {
        get => _Position;
        set
        {
            _Position = value;
            RaisePropertyChanged(value);
        }
    }

    public string Token
    {
        get; set;
    }

    public DateTime ValidTo
    {
        get; set;
    }

    //public ObservableCollection<Permission> Permissions { get; set; } = new();

}
