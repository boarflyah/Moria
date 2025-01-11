using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Contexts;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class MotorGearControllerService
{
    private readonly ApplicationDbContext _context;

    public MotorGearControllerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MotorGearDo> CreateMotorGear(MotorGearDo motorGear)
    {
        var createdMotorGear = new MotorGear
        {
            Name = motorGear.Name,
            Symbol = motorGear.Symbol,
            Ratio = motorGear.Ratio
        };

        _context.MotorGears.Add(createdMotorGear);
        await _context.SaveChangesAsync();

        motorGear.Id = createdMotorGear.Id;
        return motorGear;
    }

    public async Task<MotorGearDo?> GetMotorGearById(int id)
    {
        var searchMotorGear = await _context.MotorGears.FindAsync(id);
        if (searchMotorGear == null) return null;

        return new MotorGearDo
        {
            Id = searchMotorGear.Id,
            Name = searchMotorGear.Name,
            Symbol = searchMotorGear.Symbol,
            Ratio = searchMotorGear.Ratio
        };
    }

    public async Task<List<MotorGearDo>> GetAllMotorGears()
    {
        return await _context.MotorGears
            .Select(motorGear => new MotorGearDo
            {
                Id = motorGear.Id,
                Name = motorGear.Name,
                Symbol = motorGear.Symbol,
                Ratio = motorGear.Ratio
            })
            .ToListAsync();
    }

    public async Task<MotorGearDo?> EditMotorGear(MotorGearDo motorGear)
    {
        var searchMotorGear = await _context.MotorGears.FindAsync(motorGear.Id);
        if (searchMotorGear == null) return null;

        searchMotorGear.Name = motorGear.Name;
        searchMotorGear.Symbol = motorGear.Symbol;
        searchMotorGear.Ratio = motorGear.Ratio;

        await _context.SaveChangesAsync();
        return motorGear;
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
