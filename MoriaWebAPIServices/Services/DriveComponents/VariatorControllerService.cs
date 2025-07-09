using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaBaseServices;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.DriveComponents;

namespace MoriaWebAPIServices.Services.DriveComponents;

public class VariatorControllerService : IVariatorControllerService
{
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public VariatorControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<VariatorDo> CreateVariator(VariatorDo variator)
    {
        var createdVariator = await _creator.CreateVariator(variator);

        _context.Variators.Add(createdVariator);
        await _context.SaveChangesAsync();
        variator.Id = createdVariator.Id;
         
        return variator;
    }

    public async Task<bool> DeleteVariator(int id)
    {
        var external = await _context.Variators.FindAsync(id);
        if (external == null) return false;

        _context.Variators.Remove(external);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<VariatorDo> EditVariator(VariatorDo variatorDo)
    {

        if (variatorDo == null) throw new ArgumentNullException(nameof(variatorDo));

        var existing = await _context.Variators.FindAsync(variatorDo.Id);


        if (existing == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateVariator(existing, variatorDo);

        await _context.SaveChangesAsync();

        return _creator.GetVariatortDo(existing);
    }

    public async Task<IEnumerable<VariatorDo>> GetAllVariators()
    {
        List<VariatorDo> result = new();
        foreach (var supplement in _context.Variators)
        {
            result.Add(_creator.GetVariatortDo(supplement));
        }

        return result;
    }

    public async Task<VariatorDo> GetVariatorsById(int id)
    {
        var variator = await _context.Variators.FindAsync(id);
        if (variator == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);
        return _creator.GetVariatortDo(variator);
    }
}
