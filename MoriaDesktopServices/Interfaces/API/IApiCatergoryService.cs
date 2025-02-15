using MoriaModelsDo.Models.Products;

namespace MoriaDesktopServices.Interfaces.API;
public interface IApiCatergoryService
{
    Task<CategoryDo> CreateCategory(string username, CategoryDo category);
    Task<bool> DeleteCategory(string username, int id);
    Task<CategoryDo> GetCategory(string username, int id);
    Task<IEnumerable<CategoryDo>> GetCategories(string username);
    Task<CategoryDo> UpdateCategory(string username, CategoryDo category);

}
