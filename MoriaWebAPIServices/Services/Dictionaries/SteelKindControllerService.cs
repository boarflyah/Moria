
using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.Products;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class SteelKindControllerService : ISteelKindControllerService
{
    private readonly ApplicationDbContext _context;

    public SteelKindControllerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SteelKindDo> CreateSteelKind(SteelKindDo steelKind)
    {
        var createdSteelKind = new SteelKind
        {
            Name = steelKind.Name,
            Symbol = steelKind.Symbol
        };

        _context.SteelKinds.Add(createdSteelKind);
        await _context.SaveChangesAsync();

        steelKind.Id = createdSteelKind.Id;
        return steelKind;
    }

    public async Task<SteelKindDo?> GetSteelKindById(int id)
    {
        var steelKind = await _context.SteelKinds.FindAsync(id);
        if (steelKind == null) return null;

        return new SteelKindDo
        {
            Id = steelKind.Id,
            Name = steelKind.Name,
            Symbol = steelKind.Symbol
        };
    }

    public async Task<List<SteelKindDo>> GetAllSteelKinds()
    {
        return await _context.SteelKinds
            .Select(steelKind => new SteelKindDo
            {
                Id = steelKind.Id,
                Name = steelKind.Name,
                Symbol = steelKind.Symbol
            })
            .ToListAsync();
    }

    public async Task<SteelKindDo?> EditSteelKind(SteelKindDo steelKind)
    {
        var searchSteelKind = await _context.SteelKinds.FindAsync(steelKind.Id);
        if (searchSteelKind == null) return null;

        searchSteelKind.Name = steelKind.Name;
        searchSteelKind.Symbol = steelKind.Symbol;

        await _context.SaveChangesAsync();

        return steelKind;
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
