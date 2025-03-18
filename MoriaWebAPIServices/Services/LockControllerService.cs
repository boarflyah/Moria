using System.Collections.Concurrent;
using MoriaBaseServices;
using MoriaModels.Models.Base;
using MoriaModelsDo.Base;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPIServices.Services;
public class LockControllerService : ILockControllerService
{
    private readonly LockService _lockService;
    readonly ModelTypeConverter _typeConverter;
    readonly ApplicationDbContext _context;

    public LockControllerService(ModelTypeConverter typeConverter, ApplicationDbContext context, LockService lockService)
    {
        _typeConverter = typeConverter;
        _context = context;
        _lockService = lockService;
    }

    public async Task<bool> Lock(LockHelper lockHelper)
    {
        var entityType = _typeConverter.GetModelType(Type.GetType(lockHelper.ModelDoType));
        if (entityType == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        var entity = await _context.FindAsync(entityType, lockHelper.Id) as BaseModel;

        if (entity == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        //different user is trying to open object
        if (entity.IsLocked && !entity.LockedBy.Equals(lockHelper.Username))
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectIsLocked, MoriaApiException.ApiExceptionThrownStatusCode, entity.LockedBy);
        //same user is trying to open object
        else if (entity.IsLocked && entity.LockedBy.Equals(lockHelper.Username))
            return true;

        entity.IsLocked = true;
        entity.LockedBy = lockHelper.Username;

        _lockService.LockObject(lockHelper);

        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<bool> Unlock(LockHelper lockHelper)
    {
        var entityType = _typeConverter.GetModelType(Type.GetType(lockHelper.ModelDoType));
        if (entityType == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        var entity = await _context.FindAsync(entityType, lockHelper.Id) as BaseModel;

        if (entity == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        entity.IsLocked = false;
        entity.LockedBy = string.Empty;

        _lockService.RemoveLock(lockHelper.Id);

        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<bool> KeepAlive(LockHelper lockHelper)
    {
        return await _lockService.RefreshLock(lockHelper.Id);
    }

    public async Task<bool> RemoveObjectKeepAlive(int id)
    {
        return await _lockService.RemoveLock(id);
    }
}
