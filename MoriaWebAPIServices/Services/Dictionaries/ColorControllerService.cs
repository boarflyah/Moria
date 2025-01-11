using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.Products;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Contexts;

namespace MoriaWebAPIServices.Services.Dictionaries;
public class ColorControllerService
{
    readonly ApplicationDbContext _context;

    public ColorControllerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ColorDo> CreateColor(ColorDo color)
    {
        Color created = new()
        {
            Name = color.Name,
            Code = color.Code
        };

        _context.Colors.Add(created);
        await _context.SaveChangesAsync();

        color.Id = created.Id;
        return color;
    }
    public async Task<ColorDo?> GetColorById(int id)
    {
        var color = await _context.Colors.FindAsync(id);
        if (color == null) return null;

        return new ColorDo
        {
            Id = color.Id,
            Name = color.Name,
            Code = color.Code
        };
    }

    public async Task<List<ColorDo>> GetAllColors()
    {
        return await _context.Colors
            .Select(color => new ColorDo
            {
                Id = color.Id,
                Name = color.Name,
                Code = color.Code
            })
            .ToListAsync();
    }

    public async Task<ColorDo?> EditColor(ColorDo color)
    {
        if (color == null) throw new ArgumentNullException(nameof(color));

        var existing = await _context.Colors.FindAsync(color.Id);
        if (existing == null) return null;

        existing.Name = color.Name;
        existing.Code = color.Code;

        await _context.SaveChangesAsync();

        return new ColorDo
        {
            Id = existing.Id,
            Name = existing.Name,
            Code = existing.Code
        };
    }
    public async Task<bool> DeleteColor(int id)
    {
        var color = await _context.Colors.FindAsync(id);
        if (color == null) return false;

        _context.Colors.Remove(color);
        await _context.SaveChangesAsync();

        return true;
    }
}
