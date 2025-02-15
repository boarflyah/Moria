using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModelsDo.Models.Products;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Products;

namespace MoriaWebAPIServices.Services.Products;
public class CategoryControllerService: ICategoryControllerService
{
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public CategoryControllerService(ModelsCreator creator, ApplicationDbContext context)
    {
        _creator = creator;
        _context = context;
    }
    public async Task<IEnumerable<CategoryDo>> GetCategories()
    {
        List<CategoryDo> result = new();
        foreach (var category in _context.Categories)
            result.Add(_creator.GetCategoryDo(category));

        return result;
    }

    public async Task<CategoryDo> GetCategory(int id)
    {
        var category = await _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return null;

        return _creator.GetCategoryDo(category);
    }

    public async Task<CategoryDo> CreateCategory(CategoryDo category)
    {
        var entity = await _context.AddAsync(await _creator.CreateCategory(category));
        var created = await _context.SaveChangesAsync();
        return _creator.GetCategoryDo(entity.Entity);
    }

    public async Task<CategoryDo> UpdateCategory(CategoryDo category)
    {
        var searchCategory = await _context.Categories.FindAsync(category.Id);
        if (searchCategory == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateCategory(searchCategory, category);

        var created = await _context.SaveChangesAsync();
        return _creator.GetCategoryDo(searchCategory);
    }

    public async Task<bool> DeleteCategory(int id)
    {
        var searchCategory = await _context.Categories.FindAsync(id);
        if (searchCategory == null)
            return false;

        if (searchCategory.IsLocked)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectIsLocked, 406, searchCategory.LockedBy);

        _context.Categories.Remove(searchCategory);

        return await _context.SaveChangesAsync() == 1;
    }
}
