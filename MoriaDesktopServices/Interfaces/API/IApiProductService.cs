using MoriaModelsDo.Models.Products;

namespace MoriaDesktopServices.Interfaces.API;
public interface IApiProductService
{
    Task<IEnumerable<ProductDo>> GetProducts(string username);
    Task<ProductDo> GetProduct(string username, int id);
    Task<ProductDo> CreateProduct(string username, ProductDo employee);
    Task<ProductDo> UpdateProduct(string username, ProductDo employee);
    Task<bool> DeleteProduct(string username, int id);
}
