using System.Text.Json;
using MoriaBaseModels.Models;
using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModels.Models.Helpers;

namespace MoriaDesktopServices.Services.API;
public class ApiLookupService : IApiLookupService
{
    readonly IApiService _apiService;

    public ApiLookupService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<PagedList<LookupModel>> GetObjects(string username, Type doType, int lastId, int pageSize)
    {
        var list = await _apiService.Get<IEnumerable<LookupModel>>(username, WebAPIEndpointsProvider.GetLookupPath, null,
            new LookupBodyHelper() { LastId = lastId, ModelDoType = doType.AssemblyQualifiedName, PageSize = pageSize });

        int headerlastId = -1;
        bool hasNext = false;
        LookupHeadersMetadata lookupMetadata = null;
        var responseHeaders = _apiService.GetCurrentResponseHeaders();
        if (responseHeaders != null)
        {
            var lastIdHeader = responseHeaders.GetValueOrDefault(WebAPIEndpointsProvider.PagedListLastId)?.FirstOrDefault();
            var hasNextHeader = responseHeaders.GetValueOrDefault(WebAPIEndpointsProvider.PagedListHasNextHeaderKey)?.FirstOrDefault();
            var lookupMetadataHeader = responseHeaders.GetValueOrDefault(WebAPIEndpointsProvider.LookupMetadataHeaderKey).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(lastIdHeader))
                headerlastId = JsonSerializer.Deserialize<int>(headerlastId);
            if (!string.IsNullOrWhiteSpace(hasNextHeader))
                hasNext = JsonSerializer.Deserialize<bool>(hasNextHeader);
            if (!string.IsNullOrWhiteSpace(lookupMetadataHeader))
                lookupMetadata = JsonSerializer.Deserialize<LookupHeadersMetadata>(lookupMetadataHeader);
        }

        return new(list, headerlastId, hasNext, lookupMetadata);
    }

    public async Task<PagedList<LookupModel>> GetFilteredObjects(string username, Type doType, string searchText)
    {
        var list = await _apiService.Put<IEnumerable<LookupModel>>(username, WebAPIEndpointsProvider.PutLookupFilteredPath, null,
            new SearchRequest() { ModelDoType = doType.AssemblyQualifiedName, SearchText = searchText });

        int headerlastId = -1;
        bool hasNext = false;
        LookupHeadersMetadata lookupMetadata = null;
        var responseHeaders = _apiService.GetCurrentResponseHeaders();
        if (responseHeaders != null)
        {
            var lastIdHeader = responseHeaders.GetValueOrDefault(WebAPIEndpointsProvider.PagedListLastId)?.FirstOrDefault();
            var hasNextHeader = responseHeaders.GetValueOrDefault(WebAPIEndpointsProvider.PagedListHasNextHeaderKey)?.FirstOrDefault();
            var lookupMetadataHeader = responseHeaders.GetValueOrDefault(WebAPIEndpointsProvider.LookupMetadataHeaderKey).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(lastIdHeader))
                headerlastId = JsonSerializer.Deserialize<int>(headerlastId);
            if (!string.IsNullOrWhiteSpace(hasNextHeader))
                hasNext = JsonSerializer.Deserialize<bool>(hasNextHeader);
            if (!string.IsNullOrWhiteSpace(lookupMetadataHeader))
                lookupMetadata = JsonSerializer.Deserialize<LookupHeadersMetadata>(lookupMetadataHeader);
        }

        return new(list, headerlastId, hasNext, lookupMetadata);
    }
}
