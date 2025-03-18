
using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModels.Models.Products;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class SteelKindControllerService : ISteelKindControllerService
{
    private readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public SteelKindControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<SteelKindDo> CreateSteelKind(SteelKindDo steelKind)
    {

        var createdSteelKind = await _creator.CreateSteelKind(steelKind);

        _context.SteelKinds.Add(createdSteelKind);
        await _context.SaveChangesAsync();

        steelKind.Id = createdSteelKind.Id;
        return steelKind;
    }

    public async Task<SteelKindDo?> GetSteelKindById(int id)
    {
        var steelKind = await _context.SteelKinds.FindAsync(id);
        if (steelKind == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        return _creator.GetSteelKindDo(steelKind);
    }

    public async Task<List<SteelKindDo>> GetAllSteelKinds()
    {
        //var sql = @"
        //SELECT * FROM Categories
        //WHERE Symbol ILIKE @p0 OR Name ILIKE @p0
        //ORDER BY Name
        //LIMIT 100";  

        //return await _context.Categories
        //    .FromSqlRaw(sql, $"%{keyword}%")
        //    .ToListAsync();

        return await _context.SteelKinds
            .Select(steelKind => _creator.GetSteelKindDo(steelKind))
            .ToListAsync();
    }

    public async Task<SteelKindDo?> EditSteelKind(SteelKindDo steelKind)
    {
        var searchSteelKind = await _context.SteelKinds.FindAsync(steelKind.Id);
        if (searchSteelKind == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateSteelKind(searchSteelKind, steelKind);
        await _context.SaveChangesAsync();

        return _creator.GetSteelKindDo(searchSteelKind);
    }
    public async Task<bool> DeleteSteelKind(int id)
    {
        var steelKind = await _context.SteelKinds.FindAsync(id);
        if (steelKind == null) return false;

        _context.SteelKinds.Remove(steelKind);
        await _context.SaveChangesAsync();

        return true;
    }
}
