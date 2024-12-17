using Microsoft.Extensions.Logging;
using MoriaBaseServices;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;
using System.Collections.ObjectModel;

namespace MoriaDesktop.ViewModels.Contacts;
public sealed class EmployeeListViewModel : BaseListViewModel
{
    readonly IApiEmployeeService _employeeService;

    public EmployeeListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, IApiEmployeeService employeeService) : base(logger, appStateService)
    {
        _employeeService = employeeService;

        Employees = new();
        Title = "Pracownicy";
    }

    #region properties

    public ObservableCollection<EmployeeDo> Employees { get; set; }

    #endregion

    #region commands

    public override async Task OnLoaded()
    {
        try
        {
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
        {
            //TODO navigate to EmployeeDetailViewModel
        }
    }

    #endregion

    #region methods


    #endregion
}
