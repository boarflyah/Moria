using System.Reflection;
using Microsoft.Extensions.Logging;
using MoriaBaseServices;
using MoriaDesktop.Args;
using MoriaDesktop.Services;
using MoriaDesktopServices.Interfaces;
using MoriaModelsDo.Base;

namespace MoriaDesktop.ViewModels.Base;

public abstract class ViewModelBase: BaseNotifyPropertyChanged
{
    #region services

    protected readonly ILogger<ViewModelBase> _logger;
    protected readonly AppStateService _appStateService;
    protected readonly INavigationService _navigationService;

    #endregion

    protected ViewModelBase(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService)
    {
        _logger = logger;
        _navigationService = navigationService;
        _appStateService = appStateService;

        InitializePermissions();

    }

    #region properties

    string _Title;
    public string Title
    {
        get => _Title;
        set
        {
            _Title = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region events

    public event EventHandler<InvokeViewEventArgs> OnReAuthorizationNeeded;

    protected async Task<bool?> InvokeOnReAuthorizationNeeded(object sender, InvokeViewEventArgs args)
    {
        return await InvokeEvent(OnReAuthorizationNeeded, sender, args);
    }

    protected async Task<bool?> InvokeEvent(EventHandler<InvokeViewEventArgs> handler, object sender, InvokeViewEventArgs args)
    {
        args.CompletionSource = new();
        handler?.Invoke(sender, args);
        return await args.CompletionSource.Task;
    }

    #endregion

    #region executeapirequest methods

    protected async Task<TResult> ExecuteApiRequest<TResult>(Func<Task<TResult>> request)
    {
        try
        {
            return await request();
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.AuthorizationTokenNotAvailable)
        {
            //invoking event indicating that passing login and password is needed
            //after entering credentials call api for new token
            InvokeViewEventArgs args = new();
            var invokeResult = await InvokeOnReAuthorizationNeeded(this, args);
            //login succesful, new token assigned
            if (invokeResult.HasValue && invokeResult.Value == true)
            {
                //trying to execute api request once again, after loging in
                return await request();
            }
            //login failed, invoking event once again
            else if (invokeResult.HasValue && invokeResult.Value == false)
                return await ExecuteApiRequest(request);
            //login cancelled
            else
                throw new MoriaAppException(MoriaAppExceptionReason.ReAuthorizationCancelled, mae.Message, mae.InnerException);
        }
    }

    protected async Task<TResult> ExecuteApiRequest<TResult, T1>(Func<T1, Task<TResult>> request, T1 param1)
    {
        try
        {
            return await request(param1);
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.AuthorizationTokenNotAvailable)
        {
            //invoking event indicating that passing login and password is needed
            //after entering credentials call api for new token
            InvokeViewEventArgs args = new();
            var invokeResult = await InvokeOnReAuthorizationNeeded(this, args);
            //login succesful, new token assigned
            if (invokeResult.HasValue && invokeResult.Value == true)
            {
                //trying to execute api request once again, after loging in
                return await request(param1);
            }
            //login failed, invoking event once again
            else if (invokeResult.HasValue && invokeResult.Value == false)
                return await ExecuteApiRequest(request, param1);
            //login cancelled
            else
                throw new MoriaAppException(MoriaAppExceptionReason.ReAuthorizationCancelled, mae.Message, mae.InnerException);
        }
    }

    protected async Task<TResult> ExecuteApiRequest<TResult, T1, T2>(Func<T1, T2, Task<TResult>> request, T1 param1, T2 param2)
    {
        try
        {
            return await request(param1, param2);
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.AuthorizationTokenNotAvailable)
        {
            //invoking event indicating that passing login and password is needed
            //after entering credentials call api for new token
            InvokeViewEventArgs args = new();
            var invokeResult = await InvokeOnReAuthorizationNeeded(this, args);
            //login succesful, new token assigned
            if (invokeResult.HasValue && invokeResult.Value == true)
            {
                //trying to execute api request once again, after loging in
                return await request(param1, param2);
            }
            //login failed, invoking event once again
            else if (invokeResult.HasValue && invokeResult.Value == false)
                return await ExecuteApiRequest(request, param1, param2);
            //login cancelled
            else
                throw new MoriaAppException(MoriaAppExceptionReason.ReAuthorizationCancelled, mae.Message, mae.InnerException);
        }
    }

    protected async Task<TResult> ExecuteApiRequest<TResult, T1, T2, T3>(Func<T1, T2, T3, Task<TResult>> request, T1 param1, T2 param2, T3 param3)
    {
        try
        {
            return await request(param1, param2, param3);
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.AuthorizationTokenNotAvailable)
        {
            //invoking event indicating that passing login and password is needed
            //after entering credentials call api for new token
            InvokeViewEventArgs args = new();
            var invokeResult = await InvokeOnReAuthorizationNeeded(this, args);
            //login succesful, new token assigned
            if (invokeResult.HasValue && invokeResult.Value == true)
            {
                //trying to execute api request once again, after loging in
                return await request(param1, param2, param3);
            }
            //login failed, invoking event once again
            else if (invokeResult.HasValue && invokeResult.Value == false)
                return await ExecuteApiRequest(request, param1, param2, param3);
            //login cancelled
            else
                throw new MoriaAppException(MoriaAppExceptionReason.ReAuthorizationCancelled, mae.Message, mae.InnerException);
        }
    }

    protected async Task<TResult> ExecuteApiRequest<TResult, T1, T2, T3, T4>(Func<T1, T2, T3, T4, Task<TResult>> request, T1 param1, T2 param2, T3 param3, T4 param4)
    {
        try
        {
            return await request(param1, param2, param3, param4);
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.AuthorizationTokenNotAvailable)
        {
            //invoking event indicating that passing login and password is needed
            //after entering credentials call api for new token
            InvokeViewEventArgs args = new();
            var invokeResult = await InvokeOnReAuthorizationNeeded(this, args);
            //login succesful, new token assigned
            if (invokeResult.HasValue && invokeResult.Value == true)
            {
                //trying to execute api request once again, after loging in
                return await request(param1, param2, param3, param4);
            }
            //login failed, invoking event once again
            else if (invokeResult.HasValue && invokeResult.Value == false)
                return await ExecuteApiRequest(request, param1, param2, param3, param4);
            //login cancelled
            else
                throw new MoriaAppException(MoriaAppExceptionReason.ReAuthorizationCancelled, mae.Message, mae.InnerException);
        }
    }

    #endregion

    #region Permission
    protected void InitializePermissions()
    {        
        string modelName = this.GetType().Name
            .Replace("DetailViewModel", "")
            .Replace("ListViewModel", "");
               
        var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            if (prop.PropertyType == typeof(PermissionDo))
            {
                string permissionPropertyName = $"{modelName}_{prop.Name.Substring(11)}";

                var permission = _appStateService.LoggedUser.Position?.Permissions
                    .FirstOrDefault(x => x.PropertyName.Equals(permissionPropertyName));

                if (permission != null)
                    prop.SetValue(this, permission);
                
            }
        }
    }
    #endregion
}
