using Microsoft.Extensions.Logging;
using MoriaBaseServices;
using MoriaDesktop.Services;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.ViewModels.Base;
public class LoginViewModel : ViewModelBase
{
    #region services

    readonly INavigationService _navigationService;
    readonly IApiEmployeeService _employeeService;
    readonly IApiTokenService _tokenService;

    #endregion

    public LoginViewModel(ILogger<ViewModelBase> logger, INavigationService navigationService, IApiEmployeeService employeeService, IApiTokenService tokenService,
        AppStateService appStateService) : base(logger, appStateService)
    {
        _navigationService = navigationService;
        _employeeService = employeeService;
        _tokenService = tokenService;

        Title = "Zaloguj się";
    }

    #region properties

    #endregion

    #region commands

    #endregion

    #region commands methods



    #endregion

    #region methods

    public async Task Login(string username, string password)
    {
        EmployeeDo employee = null;
        _appStateService.SetupLoading(true);
        try
        {
            employee = await _employeeService.Login(username, password);
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.AuthorizationTokenNotAvailable)
        {
            try
            {
                employee = await _tokenService.GetUserWithToken(username, password);
            }
            catch (Exception innerEx)
            {
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, innerEx.Message, true);
            }
        }
        catch (Exception ex)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
        }
        finally
        {
            _appStateService.SetupLoading();
        }

        //TODO catch different exceptions

        if (employee != null)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Success, "Zalogowano", true);
            _navigationService.NavigateTo<SecondViewModel>(true);
            //TODO handle correct login and navigate to default view
        }
    }

    #endregion
}
