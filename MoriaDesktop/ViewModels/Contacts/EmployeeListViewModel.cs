using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.ViewModels.Contacts;
public sealed class EmployeeListViewModel : BaseListViewModel
{
    readonly IApiEmployeeService _employeeService;

    public EmployeeListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, IApiEmployeeService employeeService, INavigationService navigationService) : base(logger, appStateService, navigationService)
    {
        _employeeService = employeeService;

        Employees = new();
        Title = "Pracownicy";
    }

    #region properties

    public ObservableCollection<EmployeeDo> Employees { get; set; }

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

    #endregion

    #region methods


    #endregion
}
