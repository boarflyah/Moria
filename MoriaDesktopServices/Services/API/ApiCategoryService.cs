using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktopServices.Services.API;
public class ApiCategoryService : IApiCatergoryService
{
    readonly IApiService _apiService;

    public ApiCategoryService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<CategoryDo> CreateCategory(string username, CategoryDo category)
    {
        return await _apiService.Post<CategoryDo>(username, WebAPIEndpointsProvider.PostCategoryPath, null, category);
    }

    public async Task<bool> DeleteCategory(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteCategoryPath, null, id);
    }

    public async Task<IEnumerable<CategoryDo>> GetCategories(string username)
    {
        var result = await _apiService.Get<IEnumerable<CategoryDo>>(username, WebAPIEndpointsProvider.GetCategoriesPath, null, null);
        if (result == null)
            return new List<CategoryDo>();

        return result;
    }

    public async Task<CategoryDo> GetCategory(string username, int id)
    {
        return await _apiService.Get<CategoryDo>(username, WebAPIEndpointsProvider.GetCategoryPath, null, null, id);
    }

    public async Task<CategoryDo> UpdateCategory(string username, CategoryDo category)
    {
        return await _apiService.Put<CategoryDo>(username, WebAPIEndpointsProvider.PutCategoryPath, null, category);
    }
}
