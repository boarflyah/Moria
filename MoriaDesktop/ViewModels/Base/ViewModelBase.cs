using Microsoft.Extensions.Logging;
using MoriaBaseServices;
using MoriaDesktop.Args;
using MoriaModelsDo.Base;

namespace MoriaDesktop.ViewModels.Base;

public abstract class ViewModelBase: BaseNotifyPropertyChanged
{
    protected readonly ILogger<ViewModelBase> _logger;

    protected ViewModelBase(ILogger<ViewModelBase> logger)
    {
        _logger = logger;
    }

    public event EventHandler<InvokeViewEventArgs> OnReAuthorizationNeeded;

    protected async Task<TResult> ExecuteApiRequest<TResult>(Func<Task<TResult>> request)
    {
        try
        {
            return await request();
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.AuthorizationTokenNotAvailable)
        {
            //TODO invoke event indicating that passing login and password is needed
            //after entering credentials call api for new token
            if (await InvokeOnReAuthorizationNeeded(this, new()))
            {
                //trying to execute api request once again, after loging in 
                return await request();
            }
            throw new MoriaAppException(mae.Reason, mae.Message, mae.InnerException);
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
            //TODO invoke event indicating that passing login and password is needed
            //after entering credentials call api for new token


            //trying to execute api request once again
            return await request(param1);
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
            if (await InvokeOnReAuthorizationNeeded(this, new()))
            {
                //trying to execute api request once again, after loging in
                return await request(param1, param2);
            }
            throw new MoriaAppException(mae.Reason, mae.Message, mae.InnerException);
        }
    }

    protected async Task<bool> InvokeOnReAuthorizationNeeded(object sender, InvokeViewEventArgs args)
    {
        return await InvokeEvent(OnReAuthorizationNeeded, sender, args);
    }

    protected async Task<bool> InvokeEvent(EventHandler<InvokeViewEventArgs> handler, object sender, InvokeViewEventArgs args)
    {
        args.CompletionSource = new();
        handler?.Invoke(sender, args);
        return await args.CompletionSource.Task;
    }
}
