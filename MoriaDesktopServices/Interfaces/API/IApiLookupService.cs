using MoriaBaseModels.Models;

namespace MoriaDesktopServices.Interfaces.API;
public interface IApiLookupService
{
    Task<PagedList<LookupModel>> GetObjects(string username, Type doType, int lastId, int pageSize);
    Task<PagedList<LookupModel>> GetFilteredObjects(string username, Type doType, string searchText);
}
