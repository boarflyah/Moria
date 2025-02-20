using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModels.Models.Products;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using Npgsql;

namespace MoriaWebAPIServices.Services.Dictionaries;
public class ColorControllerService : IColorControllerService
{
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public ColorControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<ColorDo> CreateColor(ColorDo color)
    {
        var createdColor = await _creator.CreateColor(color);

        _context.Colors.Add(createdColor);
        await _context.SaveChangesAsync();
        color.Id = createdColor.Id;

        return color;
    }

    public async Task<ColorDo?> GetColorById(int id)
    {
        var color = await _context.Colors.FindAsync(id);
        if (color == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);
        return _creator.GetColorDo(color);
    }

    public async Task<List<ColorDo>> GetAllColors()
    {
        /*return await _context.Colors
            .Select(color => new ColorDo
            {
                Id = color.Id,
                Name = color.Name,
                Code = color.Code
            })
            .ToListAsync();*/

        List<ColorDo> result = new();
        foreach (var color in _context.Colors)
        {
            result.Add(_creator.GetColorDo(color));
        }

        return result;
    }

    public async Task<ColorDo?> EditColor(ColorDo color)
    {
        if (color == null) throw new ArgumentNullException(nameof(color));

        var existing = await _context.Colors.FindAsync(color.Id);

        
        if (existing == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateColor(existing, color);

        await _context.SaveChangesAsync();

        return _creator.GetColorDo(existing);
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
