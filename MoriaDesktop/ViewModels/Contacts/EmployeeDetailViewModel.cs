using Microsoft.Extensions.Logging;
using MoriaBaseServices;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.ViewModels.Contacts;
public sealed class EmployeeDetailViewModel : BaseDetailViewModel
{
    readonly IApiEmployeeService _employeeService;
    readonly INavigationService _navigationService;


    public EmployeeDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiEmployeeService employeeService, INavigationService navigationService) : base(logger, appStateService)
    {
        _employeeService = employeeService;
        _navigationService = navigationService;
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


    private bool _PasswordChanged;
    public bool PasswordChanged
    {
        get => _PasswordChanged;
        set
        {
            _PasswordChanged = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion


    #region methods

    public async override Task Load()
    {
        if (!isNew)
            try
            {
                _appStateService.SetupLoading(true);

                var employee = await ExecuteApiRequest(_employeeService.GetEmployee, _appStateService.LoggedUser.Username, objectId);
                if (employee != null)
                    Setup(employee);
                else
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
            }
            catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
            {
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
            }
            catch (Exception ex)
            {
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
            }
            finally
            {
                _appStateService.SetupLoading();
            }
    }

    public async Task SaveEmployee(string password)
    {
        try
        {
            _appStateService.SetupLoading(true);
            if (isNew)
            {
                var employee = GetEmployee();
                employee.Password = password;

                var created = await ExecuteApiRequest(_employeeService.CreateEmployee, _appStateService.LoggedUser.Username, employee);
                if(created)
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Success, "Utworzono użytkownika", true);
                else
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Niepowodzenie przy zapisie", true);
            }
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
        }
        catch (Exception ex)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
        }
        finally
        {
            _appStateService.SetupLoading();
        }
    }

    void Setup(EmployeeDo employee)
    {
        FirstName = employee.FirstName;
        LastName = employee.LastName;
        PhoneNumber = employee.PhoneNumber;
        Username = employee.Username;
    }

    EmployeeDo GetEmployee()
    {
        return new()
        {
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            Username = Username,
            LastModified = _appStateService.LoggedUser.Username,
        };
    }

    #endregion
}
