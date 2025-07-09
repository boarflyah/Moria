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

public class SupplementControllerService : ISupplementControllerService
{
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public SupplementControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<SupplementDo> CreateSupplement(SupplementDo supplementDo)
    {
        var createdSupplement = await _creator.CreateSuplement(supplementDo);

        _context.Supplements.Add(createdSupplement);
        await _context.SaveChangesAsync();
        supplementDo.Id = createdSupplement.Id;

        return supplementDo;
    }

    public async Task<bool> DeleteSupplement(int id)
    {
        var external = await _context.Supplements.FindAsync(id);
        if (external == null) return false;

        _context.Supplements.Remove(external);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<SupplementDo> EditSupplement(SupplementDo supplementDo)
    {

        if (supplementDo == null) throw new ArgumentNullException(nameof(supplementDo));

        var existing = await _context.Supplements.FindAsync(supplementDo.Id);


        if (existing == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateSupplement(existing, supplementDo);

        await _context.SaveChangesAsync();

        return _creator.GetSupplementDo(existing);
    }

    public async Task<IEnumerable<SupplementDo>> GetAllSupplements()
    {
        List<SupplementDo> result = new();
        foreach (var supplement in _context.Supplements)
        {
            result.Add(_creator.GetSupplementDo(supplement));
        }

        return result;
    }

    public async Task<SupplementDo> GetSupplementById(int id)
    {
        var supplement = await _context.Supplements.FindAsync(id);
        if (supplement == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);
        return _creator.GetSupplementDo(supplement);
    }
}
