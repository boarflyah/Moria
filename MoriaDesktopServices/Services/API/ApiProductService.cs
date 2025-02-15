using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktopServices.Services.API;
public class ApiProductService: IApiProductService
{
    readonly IApiService _apiService;

    public ApiProductService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<ProductDo>> GetProducts(string username)
    {
        var result = await _apiService.Get<IEnumerable<ProductDo>>(username, WebAPIEndpointsProvider.GetProductsPath, null, null);
        if (result != null)
            return result;

        return new List<ProductDo>();
    }

    public async Task<ProductDo> GetProduct(string username, int id)
    {
        return await _apiService.Get<ProductDo>(username, WebAPIEndpointsProvider.GetProductPath, null, null, id);
    }

    public async Task<ProductDo> CreateProduct(string username, ProductDo employee)
    {
        return await _apiService.Post<ProductDo>(username, WebAPIEndpointsProvider.PostProductPath, null, employee);
    }

    public async Task<ProductDo> UpdateProduct(string username, ProductDo employee)
    {
        return await _apiService.Put<ProductDo>(username, WebAPIEndpointsProvider.PutProductPath, null, employee);
    }

    public async Task<bool> DeleteProduct(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteProductPath, null, id);
    }
}
