using System.Collections.Concurrent;
using System.ComponentModel;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Services.API;
using MoriaModelsDo.Base;

public class KeepAliveWorker : BackgroundWorker,  IKeepAliveWorker
{
    private readonly IApiService _apiService;
    private readonly ConcurrentDictionary<int, LockHelper> _trackedObjects = new();
    private readonly TimeSpan _keepAliveInterval = TimeSpan.FromSeconds(50);

    public KeepAliveWorker(IApiService apiService)
    {
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        WorkerSupportsCancellation = true;
    }

    public void Start()
    {
        if (!IsBusy)
            RunWorkerAsync();
    }

    public void LockObject(LockHelper obj)
    {
        _trackedObjects[obj.Id] = obj;
    }

    public void RemoveLock(int id)
    {
        _trackedObjects.TryRemove(id, out _);
    }

    protected override void OnDoWork(DoWorkEventArgs e)
    {
        while (!CancellationPending)
        {
            foreach (var obj in _trackedObjects.Values.ToList())
            {
                SendKeepAlive(obj);
            }
            Thread.Sleep(_keepAliveInterval);
        }
    }

    private async void SendKeepAlive(LockHelper obj)
    {
        try
        {
            bool response = await _apiService.Put<bool>(
                                   obj.Username,
                                   WebAPIEndpointsProvider.KeepAlivePath,
                                   null,
                                   obj
                               );

        }
        catch (Exception ex)
        {
            Console.WriteLine($" KeepAlive: {ex.Message}");
        }
    }

    public void RemoveLock()
    {
        CancelAsync();
    }
}


//public class KeepAliveWorker : IKeepAliveWorker
//{
//    private readonly IApiService _apiService;
//    private readonly TimeSpan _keepAliveInterval;
//    private CancellationTokenSource _cts;
//    private Task _keepAliveTask;
//    private LockHelper? _lockHelper;

//    public KeepAliveWorker(IApiService apiService, IConfiguration configuration)
//    {
//        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
//        double intervalMinutes = 1; 
//        _keepAliveInterval = TimeSpan.FromMinutes(intervalMinutes);
//    }

//    public void Start(LockHelper lockHelper)
//    {
//        try
//        {
//            _cts = new CancellationTokenSource();
//            if (_keepAliveTask != null)
//                throw new InvalidOperationException("KeepAliveWorker jest już uruchomiony.");
//        }
//        catch (Exception ex)
//        {
//            ;
//        }       

//        _lockHelper = lockHelper ??  throw new ArgumentNullException(nameof(lockHelper));
//        _keepAliveTask = KeepAliveLoop(_cts.Token);
//    }

//    private async Task KeepAliveLoop(CancellationToken token)
//    {
//        using var timer = new PeriodicTimer(_keepAliveInterval);
//        try
//        {
//            while (await timer.WaitForNextTickAsync(token))
//            {
//                try
//                {
//                    bool response = await _apiService.Put<bool>(
//                        _lockHelper.Username,
//                        WebAPIEndpointsProvider.KeepAlivePath,
//                        null,
//                        _lockHelper
//                    );
//                }
//                catch (Exception ex)
//                {
//                    ;
//                }
//            }
//        }
//        catch (OperationCanceledException)
//        {
//            ;
//        }
//    }

//    public void Stop()
//    {

//        try
//        {
//            _cts.Cancel();
//        }
//        catch (AggregateException)
//        {
//            ;
//        }

//        _cts.Dispose();
//        _keepAliveTask = null;
//    }

//    void IDisposable.Dispose()
//    {
//        ;
//    }
//}
