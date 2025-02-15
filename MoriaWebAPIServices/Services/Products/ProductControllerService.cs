using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModelsDo.Models.Products;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Products;

namespace MoriaWebAPIServices.Services.Products;
public class ProductControllerService : IProductControllerService
{
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public ProductControllerService(ModelsCreator modelCreator, ApplicationDbContext dbContext)
    {
        _creator = modelCreator;
        _context = dbContext;
    }

    public async Task<ProductDo> GetProduct(int id)
    {
        //var product = await _context.Products.FindAsync(id);
        var product = await _context.Products.Include(x => x.SteelKind).Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
            return null;

        return _creator.GetProductDo(product);
    }

    public async Task<IEnumerable<ProductDo>> GetProducts()
    {
        List<ProductDo> result = new();
        foreach (var product in _context.Products.Include(x => x.Category))
            result.Add(_creator.GetProductDo(product));

        return result;
    }

    public async Task<ProductDo> CreateProduct(ProductDo product)
    {
        var entity = await _context.AddAsync(await _creator.CreateProduct(product));
        var created = await _context.SaveChangesAsync();
        return _creator.GetProductDo(entity.Entity);
    }

    public async Task<ProductDo> UpdateProduct(ProductDo product)
    {
        var searchProduct = await _context.Products.FindAsync(product.Id);
        if (searchProduct == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateProduct(searchProduct, product);

        var created = await _context.SaveChangesAsync();
        return _creator.GetProductDo(searchProduct);
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var searchProduct = await _context.Products.FindAsync(id);
        if (searchProduct == null)
            return false;

        if (searchProduct.IsLocked)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectIsLocked, 406, searchProduct.LockedBy);

        _context.Products.Remove(searchProduct);

        return await _context.SaveChangesAsync() == 1;
    }
}
