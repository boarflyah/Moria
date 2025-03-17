using System.CodeDom;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.ViewModels.Contacts;
public sealed class EmployeeListViewModel : BaseListViewModel
{
    readonly IApiEmployeeService _employeeService;

    public EmployeeListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, IApiEmployeeService employeeService, INavigationService navigationService, IListViewService listViewService) : base(logger, appStateService, navigationService, listViewService)
    {
        _employeeService = employeeService;

        Employees = new();
        Title = "Pracownicy";
    }

    #region properties

    public ObservableCollection<EmployeeDo> Employees { get; set; }

    PermissionDo _Permission_FirstName;
    public PermissionDo Permission_FirstName
    {
        get => _Permission_FirstName;
        set
        {
            _Permission_FirstName = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_LastName;
    public PermissionDo Permission_LastName
    {
        get => _Permission_LastName;
        set
        {
            _Permission_LastName = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Username;
    public PermissionDo Permission_Username
    {
        get => _Permission_Username;
        set
        {
            _Permission_Username = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_PhoneNumber;
    public PermissionDo Permission_PhoneNumber
    {
        get => _Permission_PhoneNumber;
        set
        {
            _Permission_PhoneNumber = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Position;
    public PermissionDo Permission_Position
    {
        get => _Permission_Position;
        set
        {
            _Permission_Position = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Admin;
    public PermissionDo Permission_Admin
    {
        get => _Permission_Admin;
        set
        {
            _Permission_Admin = value;
            RaisePropertyChanged(value);
        }
    }
    #endregion

    #region commands

    protected async override Task LoadList()
    {
        Employees.Clear();

        var employees = await ExecuteApiRequest(_employeeService.GetEmployees, _appStateService.LoggedUser.Username);
        if (employees != null)
        {
            foreach (var employee in employees)
                Employees.Add(employee);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    public override void OnRowSelected(object row)
    {
        if (row is EmployeeDo edo)
                _navigationService.NavigateTo(typeof(EmployeeDetailViewModel), false, edo.Id);
    }

    protected override void New() =>
        _navigationService.NavigateTo(typeof(EmployeeDetailViewModel), false, null);

    protected async override Task<bool> SendDeleteRequest() => 
        await ExecuteApiRequest(_employeeService.DeleteEmployee, _appStateService.LoggedUser.Username, (Selected as EmployeeDo)?.Id ?? 0);


    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<EmployeeDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    Employees.Clear();
                    foreach (var item in result)
                    {
                        Employees.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, $"Brak danych do wczytania. {ex.Message}", true);
            }
        }
        else
            await LoadList();
    }

    #endregion

    #region methods


    #endregion
}
