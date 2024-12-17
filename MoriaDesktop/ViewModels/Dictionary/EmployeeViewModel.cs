using Microsoft.Extensions.Logging;
using MoriaDesktop.ViewModels.Base;

namespace MoriaDesktop.ViewModels.Dictionary;

public class EmployeeViewModel : ViewModelBase
{
    public EmployeeViewModel(ILogger<ViewModelBase> logger) : base(logger)
    {
    }

    #region properties

    string _FirstName;
    public string FirstName
    {
        get => _FirstName;
        set
        {
            _FirstName = value;
            RaisePropertyChanged(nameof(FirstName));
        }
    }

    string _LastName;
    public string LastName
    {
        get => _LastName;
        set
        {
            _LastName = value;
            RaisePropertyChanged(nameof(LastName));
        }
    }

    string _Username;
    public string Username
    {
        get => _Username;
        set
        {
            _Username = value;
            RaisePropertyChanged(nameof(Username));
        }
    }

    string _Password;
    public string Password
    {
        get => _Password;
        set
        {
            _Password = value;
            RaisePropertyChanged(nameof(Password));
        }
    }

    string _PhoneNumber;
    public string PhoneNumber
    {
        get => _PhoneNumber;
        set
        {
            _PhoneNumber = value;
            RaisePropertyChanged(nameof(PhoneNumber));
        }
    }

    #endregion
}
