
using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktopServices.Services.API;
/// <summary>
/// Used for reading enpoints in ColorController in MoriaApi
/// </summary>
public class ApiColorService : IApiColorService
{
    readonly IApiService _apiService;

    public ApiColorService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<ColorDo>> GetColors(string username)
    {
        var result = await _apiService.Get<IEnumerable<ColorDo>>(username, WebAPIEndpointsProvider.GetColorsPath, null, null);
        if (result == null)
            return new List<ColorDo>();

        return result;
    }

    public async Task<ColorDo> GetColor(string username, int id)
    {
        return await _apiService.Get<ColorDo>(username, WebAPIEndpointsProvider.GetColorPath, null, null, parameters: id);
    }

    public async Task<ColorDo> CreateColor(string username, ColorDo color)
    {
        return await _apiService.Post<ColorDo>(username, WebAPIEndpointsProvider.PostColorPath, null, color);
    }

    public async Task<ColorDo> UpdateColor(string username, ColorDo color)
    {
        return await _apiService.Put<ColorDo>(username, WebAPIEndpointsProvider.PutColorPath, null, color);
    }

    public async Task<bool> DeleteColor(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteColorPath, null, id);
    }
}
