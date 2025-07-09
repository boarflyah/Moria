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

public class PumpControllerService : IPumpControllerService
{

    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public PumpControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<PumpDo> CreatePump(PumpDo pump)
    {
        var createdPump = await _creator.CreatePump(pump);

        _context.Pumps.Add(createdPump);
        await _context.SaveChangesAsync();
        pump.Id = createdPump.Id;

        return pump;
    }

    public async Task<bool> DeletePump(int id)
    {
        var external = await _context.Pumps.FindAsync(id);
        if (external == null) return false;

        _context.Pumps.Remove(external);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<PumpDo> EditPump(PumpDo pumpDo)
    {
        if (pumpDo == null) throw new ArgumentNullException(nameof(pumpDo));

        var existing = await _context.Pumps.FindAsync(pumpDo.Id);


        if (existing == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdatePump(existing, pumpDo);

        await _context.SaveChangesAsync();

        return _creator.GetPumpDo(existing);
    }

    public async Task<IEnumerable<PumpDo>> GetAllPumps()
    {
        List<PumpDo> result = new();
        foreach (var pump in _context.Pumps)
        {
            result.Add(_creator.GetPumpDo(pump));
        }

        return result;
    }

    public async Task<PumpDo> GetPumpById(int id)
    {
        var pump = await _context.Pumps.FindAsync(id);
        if (pump == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);
        return _creator.GetPumpDo(pump);
    }
}
