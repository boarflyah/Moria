using Microsoft.EntityFrameworkCore;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Products;

namespace MoriaWebAPIServices.Services.Products;
public class ComponentControllerService: IComponentControllerService
{
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public ComponentControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<ComponentDo> GetComponent(int id)
    {
        var component = await _context.Components.Include(x => x.ComponentColor).Include(x => x.ComponentProduct).Include(x => x.DriveToComponents).ThenInclude(x => x.Drive).FirstOrDefaultAsync(x => x.Id == id);
        if (component == null)
            return null;

        return _creator.GetComponentDo(component);
    }

}
