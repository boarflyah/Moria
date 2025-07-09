using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.DriveComponents;

namespace MoriaWebAPIServices.Services.DriveComponents;
public class ExternalCoolingControllerService : IExternalCoolingControllerService
{
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;
    public ExternalCoolingControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<ExternalCoolingDo> CreateExternalCooling(ExternalCoolingDo externalDo)
    {
        var createdExternalCooling = await _creator.CreateExternalCooling(externalDo);

        _context.ExternalCoolings.Add(createdExternalCooling);
        await _context.SaveChangesAsync();
        externalDo.Id = createdExternalCooling.Id;

        return externalDo;
    }

    public async Task<bool> DeleteExternalCooling(int id)
    {
        var external = await _context.ExternalCoolings.FindAsync(id);
        if (external == null) return false;

        _context.ExternalCoolings.Remove(external);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<ExternalCoolingDo> EditExternalCooling(ExternalCoolingDo externalCoolingDo)
    {

        if (externalCoolingDo == null) throw new ArgumentNullException(nameof(externalCoolingDo));

        var existing = await _context.ExternalCoolings.FindAsync(externalCoolingDo.Id);


        if (existing == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateExternalCooling(existing, externalCoolingDo);

        await _context.SaveChangesAsync();

        return _creator.GetExternalCoolingDo(existing);
    }

    public async Task<IEnumerable<ExternalCoolingDo>> GetAllExternalCoolings()
    {
        List<ExternalCoolingDo> result = new();
        foreach (var externalCooling in _context.ExternalCoolings)
        {
            result.Add(_creator.GetExternalCoolingDo(externalCooling));
        }

        return result;
    }

    public async Task<ExternalCoolingDo> GetExternalCoolingById(int id)
    {
        var externalCooling = await _context.ExternalCoolings.FindAsync(id);
        if (externalCooling == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);
        return _creator.GetExternalCoolingDo(externalCooling);
    }
}
