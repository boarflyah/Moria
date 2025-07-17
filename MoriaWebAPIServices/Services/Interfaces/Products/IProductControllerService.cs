using MoriaModelsDo.Models.Orders.Relations;
using MoriaModelsDo.Models.Products;

namespace MoriaWebAPIServices.Services.Interfaces.Products;
public interface IProductControllerService
{
    Task<IEnumerable<ProductDo>> GetProducts();

    Task<ProductDo> GetProduct(int id);

    Task<ProductDo> CreateProduct(ProductDo product);

    Task<ProductDo> UpdateProduct(ProductDo product);

    Task<bool> DeleteProduct(int id);
    Task<IEnumerable<ComponentToOrderItemDo>> GetProductDrives(int id);
}
