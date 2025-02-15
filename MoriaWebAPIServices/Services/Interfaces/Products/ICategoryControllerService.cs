using MoriaModelsDo.Models.Products;

namespace MoriaWebAPIServices.Services.Interfaces.Products;
public interface ICategoryControllerService
{
    Task<IEnumerable<CategoryDo>> GetCategories();

    Task<CategoryDo> GetCategory(int id);

    Task<CategoryDo> CreateCategory(CategoryDo category);

    Task<CategoryDo> UpdateCategory(CategoryDo category);

    Task<bool> DeleteCategory(int id);

}
