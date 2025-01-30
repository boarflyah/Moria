using Microsoft.Extensions.Logging;
using MoriaBaseServices;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.ViewModels.Contacts;
public sealed class EmployeeDetailViewModel : BaseDetailViewModel
{
    readonly IApiEmployeeService _employeeService;

    public EmployeeDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiEmployeeService employeeService, INavigationService navigationService, IApiLockService apiLockService)
        : base(logger, appStateService, apiLockService, navigationService)
    {
        _employeeService = employeeService;
    }

    #region properties

    string _FirstName;
    [ObjectChangedValidate]
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
    [ObjectChangedValidate]
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
    [ObjectChangedValidate]
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
    [ObjectChangedValidate]
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


    private PositionDo _Position;
    [ObjectChangedValidate]
    public PositionDo Position
    {
        get => _Position;
        set
        {
            _Position = value;
            RaisePropertyChanged(value);
        }
    }


    private bool _Admin;
    [ObjectChangedValidate]
    public bool Admin
    {
        get => _Admin;
        set
        {
            _Admin = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion


    #region methods

    public async Task SaveEmployee(string password)
    {
        try
        {
            _appStateService.SetupLoading(true);
            var succeeded = false;
            if (isNew)
            {
                var employee = GetDo() as EmployeeDo;
                employee.Password = password;

                var created = await ExecuteApiRequest(_employeeService.CreateEmployee, _appStateService.LoggedUser.Username, employee);
                if (created != null)
                {
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Success, "Utworzono użytkownika", true);
                    succeeded = true;
                    isNew = false;
                    objectId = created.Id;
                    Clear();
                    Setup(created);
                }
                else
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Niepowodzenie przy zapisie", true);
            }
            else
            {
                var employee = GetDo() as EmployeeDo;
                if (PasswordChanged)
                    employee.Password = password;

                var updated = await ExecuteApiRequest(_employeeService.UpdateEmployee, _appStateService.LoggedUser.Username, employee);
                if (updated != null)
                {
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Success, "Zapisano zmiany", true);
                    succeeded = true;
                    Clear();
                    Setup(updated);
                }
                else
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Niepowodzenie przy zapisie", true);
            }

            if (succeeded)
            {
                IsLocked = true;
                HasObjectChanged = false;
                PasswordChanged = false;
                await Load();
            }
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
        }
        catch (MoriaApiException apiException)
        {
            if(apiException.Reason == MoriaApiExceptionReason.ValueIsNotUnique)
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "NAZWA UŻYTKOWNIKA musi być unikalna", true);
            else
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, apiException.Reason.ToString(), true);
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

    public async Task SaveAndCloseEmployee(string password)
    {
        try
        {
            _appStateService.SetupLoading(true);
            var succeeded = false;
            if (isNew)
            {
                var employee = GetDo() as EmployeeDo;
                employee.Password = password;

                var created = await ExecuteApiRequest(_employeeService.CreateEmployee, _appStateService.LoggedUser.Username, employee);
                if (created != null)
                {
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Success, "Zapisano zmiany", true);
                    succeeded = true;
                }
                else
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Niepowodzenie przy zapisie", true);
            }
            else
            {
                var employee = GetDo() as EmployeeDo;
                if (PasswordChanged)
                    employee.Password = password;

                var updated = await ExecuteApiRequest(_employeeService.UpdateEmployee, _appStateService.LoggedUser.Username, employee);
                if (updated != null)
                {
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Success, "Zapisano zmiany", true);
                    succeeded = true;
                }
                else
                    _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Niepowodzenie przy zapisie", true);
            }

            if (succeeded)
            {
                if (_navigationService.CanGoBack)
                    _navigationService.GoBack();
                else
                    _navigationService.NavigateTo(typeof(EmployeeListViewModel), true);
            }
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
        }
        catch (MoriaApiException apiException)
        {
            if (apiException.Reason == MoriaApiExceptionReason.ValueIsNotUnique)
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "NAZWA UŻYTKOWNIKA musi być unikalna", true);
            else
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, apiException.Reason.ToString(), true);
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

    public override Type GetModelType() => typeof(EmployeeDo);

    protected async override Task LoadObject()
    {
        Clear();

        var employee = await ExecuteApiRequest(_employeeService.GetEmployee, _appStateService.LoggedUser.Username, objectId);
        if (employee != null)
            Setup(employee);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    //overriden as empty method, because we need to pass password from control directly as parameter (safety issues)
    protected async override Task<bool> SaveNewObject() => true;

    //overriden as empty methods, because we need to pass password from control directly as parameter (safety issues)
    protected async override Task<bool> UpdateExistingObject() => true;

    //overriden as empty methods, because we need to pass password from control directly as parameter (safety issues)
    protected async override Task Save()
    {
    }

    //overriden as empty methods, because we need to pass password from control directly as parameter (safety issues)
    protected async override Task SaveAndClose()
    {
    }

    void Setup(EmployeeDo employee)
    {
        FirstName = employee.FirstName;
        LastName = employee.LastName;
        PhoneNumber = employee.PhoneNumber;
        Username = employee.Username;
        Position = employee.Position;
        Admin = employee.Admin;
    }

    public override void Clear()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        PhoneNumber = string.Empty;
        Username = string.Empty;
        Position = null;
        Admin = false;
    }

    public override BaseDo GetDo()
    {
        return new EmployeeDo()
        {
            Id = objectId,
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            Username = Username,
            LastModified = _appStateService.LoggedUser.Username,
            Admin = Admin,
            Position = Position
        };
    }

    #endregion
}
