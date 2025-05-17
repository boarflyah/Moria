using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaBaseServices;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class ElectricalCabinetControllerService : IElectricalCabinetControllerService
{    
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public ElectricalCabinetControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<bool> DeleteElectricalCabinet(int id)
    {
        var cabinet = await _context.ElectricalCabinets.FindAsync(id);
        if (cabinet == null) return false;

        _context.ElectricalCabinets.Remove(cabinet);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<ElectricalCabinetDo> CreateElectricalCabinet(ElectricalCabinetDo cabinet)
    {
        var createdCabinet = await _creator.CreateElectricalCabinet(cabinet);

        _context.ElectricalCabinets.Add(createdCabinet);
        await _context.SaveChangesAsync();
        cabinet.Id = createdCabinet.Id;

        return cabinet;
    }

    public async Task<ElectricalCabinetDo> GetElectricalCabinet(int id)
    {
        var cabinet = await _context.ElectricalCabinets.FindAsync(id);
        if (cabinet == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);
        return _creator.GetElectricalCabinet(cabinet);
    }

    public async Task<IEnumerable<ElectricalCabinetDo>> GetElectricalCabinets()
    {
        List<ElectricalCabinetDo> result = new();
        foreach (var cabinet in _context.ElectricalCabinets)
        {
            result.Add(_creator.GetElectricalCabinet(cabinet));
        }

        return result;
    }

    public async Task<ElectricalCabinetDo> UpdateElectricalCabinet(ElectricalCabinetDo cabinet)
    {
        if (cabinet == null) throw new ArgumentNullException(nameof(cabinet));

        var existing = await _context.ElectricalCabinets.FindAsync(cabinet.Id);


        if (existing == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateElectricalCabinet(existing, cabinet);

        await _context.SaveChangesAsync();

        return _creator.GetElectricalCabinet(existing);
    }
}

