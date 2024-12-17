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

        return color;   
    }
}
