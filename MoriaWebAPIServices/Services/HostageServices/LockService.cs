using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using MoriaBaseServices;
using MoriaModels.Models.Base;
using MoriaModelsDo.Base;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services;

public class LockService : BackgroundWorker
{
    private readonly ConcurrentDictionary<int, (LockHelper LockData, DateTime ExpiryTime)> _lockedObjects = new();
    private readonly TimeSpan _expiryTime = TimeSpan.FromMinutes(3);
    private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(30);
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public LockService(IServiceScopeFactory serviceScopeFactory)
    {
        WorkerSupportsCancellation = true;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Start()
    {
        if (!IsBusy)
            RunWorkerAsync();
    }

    protected async override void OnDoWork(DoWorkEventArgs e)
    {
        while (!CancellationPending)
        {           
            var now = DateTime.UtcNow;
            foreach (var key in _lockedObjects.Keys.ToList())
            {
                if (_lockedObjects.TryGetValue(key, out var entry) && entry.ExpiryTime < now)
                {
                    _lockedObjects.TryRemove(key, out _);
                    OnLockExpired(entry.LockData);
                }
            }
            Thread.Sleep(_checkInterval);
        }
    }

    public void LockObject(LockHelper obj)
    {
        _lockedObjects[obj.Id] = (obj, DateTime.UtcNow + _expiryTime);
    }

    public async Task<bool> RefreshLock(int id)
    {
       if (_lockedObjects.TryGetValue(id, out var entry))
        {
            _lockedObjects[id] = (entry.LockData, DateTime.UtcNow + _expiryTime);
            return true;
        }
        return false;
    }

    public async Task<bool> RemoveLock(int id)
    {
        return _lockedObjects.TryRemove(id, out _);
    }

    private async void OnLockExpired(LockHelper obj)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var _modelConverter = scope.ServiceProvider.GetRequiredService<ModelTypeConverter>();

            var entityType = _modelConverter.GetModelType(Type.GetType(obj.ModelDoType));
            if (entityType == null)
                throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

            var entity = await _context.FindAsync(entityType, obj.Id) as BaseModel;

            if (entity == null)
                throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

            entity.IsLocked = false;
            entity.LockedBy = string.Empty;

            await _context.SaveChangesAsync();
        }
    }

    public void Stop()
    {
        CancelAsync();
    }
}
