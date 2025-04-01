using MoriaBaseModels.Models;
using MoriaBaseServices;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPIServices.Services;
public class LookupControllerService : ILookupControllerService
{
    readonly ApplicationDbContext _context;

    public LookupControllerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<LookupModel>> GetObjects(Type entityType, int lastId, int pageSize)
    {
        if (entityType == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        var dbSet = _context.Get(entityType);

        var filtered = dbSet.OrderBy(x => x.Id)
            .Where(x => x.Id > lastId)
            //.SkipWhile(x => x.Id <= lastId)
            .Take(pageSize == int.MaxValue ? pageSize : pageSize + 1)
            .Select(x => x.GetLookupObject());

        var filteredToPageSize = filtered.Take(pageSize);

        return new(filteredToPageSize, filteredToPageSize.Any() ? filteredToPageSize.Last().Id : -1, filtered.Count() > pageSize);
    }
}
