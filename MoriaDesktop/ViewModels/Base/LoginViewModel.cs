﻿using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.ViewModels.Base;
public class LoginViewModel : ViewModelBase
{
    #region services

    readonly IApiTokenService _tokenService;

    #endregion

    public LoginViewModel(ILogger<ViewModelBase> logger, INavigationService navigationService, IApiTokenService tokenService,
        AppStateService appStateService) : base(logger, appStateService, navigationService)
    {
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
            employee = await _tokenService.GetUserWithToken(username, password);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Logowanie do aplikacji");
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
        }
        finally
        {
            _appStateService.SetupLoading();
        }

        //TODO catch different exceptions

        if (employee != null)
        {
            _appStateService.OnLoggedIn(employee);
            _navigationService.NavigateTo(typeof(EmployeeListViewModel), true);
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Success, "Zalogowano", true);
            //TODO handle correct login and navigate to default view
        }
    }

    #endregion
}
