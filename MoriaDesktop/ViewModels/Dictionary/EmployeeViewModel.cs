using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;

namespace MoriaDesktop.ViewModels.Dictionary;

public class EmployeeViewModel : ViewModelBase
{
    public EmployeeViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService) : base(logger, appStateService)
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

    string _Username;
    public string Username
    {
        get => _Username;
        set
        {
            _Username = value;
            RaisePropertyChanged(value);
        }
    }

    string _Password;
    public string Password
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
            _PhoneNumber = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion
}
