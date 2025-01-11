using Microsoft.Extensions.Logging;
using MoriaBaseServices;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;
using System.Collections.ObjectModel;

namespace MoriaDesktop.ViewModels.Contacts;
public sealed class EmployeeListViewModel : BaseListViewModel
{
    readonly IApiEmployeeService _employeeService;
    readonly INavigationService _navigationService;

    public EmployeeListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, IApiEmployeeService employeeService, INavigationService navigationService) : base(logger, appStateService)
    {
        _employeeService = employeeService;
        _navigationService = navigationService;

        Employees = new();
        Title = "Pracownicy";
    }

    #region properties

    public ObservableCollection<EmployeeDo> Employees { get; set; }

    #endregion

    #region commands

    public async override Task OnLoaded()
    {
        try
        {
            Employees.Clear();
            _appStateService.SetupLoading(true);

            var employees = await ExecuteApiRequest(_employeeService.GetEmployees, _appStateService.LoggedUser.Username);
            if (employees != null)
            {
                foreach (var employee in employees)
                    Employees.Add(employee);
            }
            else
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
        }
        catch(MoriaAppException mae) when(mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
        }
        catch(Exception ex)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
        }
        finally
        {
            _appStateService.SetupLoading();
        }
    }

    public override void OnRowSelected(object row)
    {
        if (row is EmployeeDo edo)
                _navigationService.NavigateTo(typeof(EmployeeDetailViewModel), false, edo.Id);
        else
            _navigationService.NavigateTo(typeof(EmployeeDetailViewModel), false, null);
    }

    #endregion

    #region methods


    #endregion
}
