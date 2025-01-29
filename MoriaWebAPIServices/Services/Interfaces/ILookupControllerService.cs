using MoriaBaseModels.Models;

namespace MoriaWebAPIServices.Services.Interfaces;
public interface ILookupControllerService
{
    Task<PagedList<LookupModel>> GetObjects(Type entityType, int lastId, int pageSize);
}
