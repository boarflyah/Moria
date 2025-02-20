using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class MotorGearControllerService : IMotorGearControllerService
{
    private readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public MotorGearControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<MotorGearDo> CreateMotorGear(MotorGearDo motorGear)
    {
        var createdMotorGear = await _creator.CreateMotorGear(motorGear);      

        _context.MotorGears.Add(createdMotorGear);
        await _context.SaveChangesAsync();

        motorGear.Id = createdMotorGear.Id;
        return motorGear;
    }

    public async Task<MotorGearDo?> GetMotorGearById(int id)
    {
        var searchMotorGear = await _context.MotorGears.FindAsync(id);
        if (searchMotorGear == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        return _creator.GetMotorGear(searchMotorGear);
    }

    public async Task<List<MotorGearDo>> GetAllMotorGears()
    {
        return await _context.MotorGears
            .Select(motorGear => _creator.GetMotorGear(motorGear))            
            .ToListAsync();
    }

    public async Task<MotorGearDo?> EditMotorGear(MotorGearDo motorGear)
    {
        var searchMotorGear = await _context.MotorGears.FindAsync(motorGear.Id);
        if (searchMotorGear == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateMotorGear(searchMotorGear, motorGear);
        await _context.SaveChangesAsync();
        return _creator.GetMotorGear(searchMotorGear);
    }

    public async Task<bool> DeleteMotorGear(int id)
    {
        var motorGear = await _context.MotorGears.FindAsync(id);
        if (motorGear == null) return false;

        _context.MotorGears.Remove(motorGear);
        await _context.SaveChangesAsync();
        return true;
    }
}
