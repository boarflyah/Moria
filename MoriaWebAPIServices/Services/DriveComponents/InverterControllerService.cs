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
public class InverterControllerService : IInverterControllerService
{
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;
    public InverterControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<InverterDo> CreateInverter(InverterDo inverterDo)
    {
        var createdInverter = await _creator.CreateInverter(inverterDo);

        _context.Inverters.Add(createdInverter);
        await _context.SaveChangesAsync();
        inverterDo.Id = createdInverter.Id;

        return inverterDo;
    }

    public async Task<bool> DeleteInverter(int id)
    {
        var external = await _context.Inverters.FindAsync(id);
        if (external == null) return false;

        _context.Inverters.Remove(external);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<InverterDo> EditInverter(InverterDo inverterDo)
    {
        if (inverterDo == null) throw new ArgumentNullException(nameof(inverterDo));

        var existing = await _context.Inverters.FindAsync(inverterDo.Id);


        if (existing == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateInverter(existing, inverterDo);

        await _context.SaveChangesAsync();

        return _creator.GetInverterDo(existing);
    }

    public async Task<IEnumerable<InverterDo>> GetAllInverters()
    {
        List<InverterDo> result = new();
        foreach (var inverter in _context.Inverters)
        {
            result.Add(_creator.GetInverterDo(inverter));
        }

        return result;
    }

    public async Task<InverterDo> GetInverterById(int id)
    {
        var inverter = await _context.Inverters.FindAsync(id);
        if (inverter == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);
        return _creator.GetInverterDo(inverter);
    }
}
