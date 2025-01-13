using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class MotorControllerService : IMotorControllerService
{
    private readonly ApplicationDbContext _context;

    public MotorControllerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MotorDo> CreateMotor(MotorDo motor)
    {

        var createdMotor = new Motor
        {
            Name = motor.Name,
            Symbol = motor.Symbol,
            Power = motor.Power
        };

        _context.Motors.Add(createdMotor);
        await _context.SaveChangesAsync();

        motor.Id = createdMotor.Id;
        return motor;
    }

    public async Task<MotorDo?> GetMotorById(int id)
    {
        var searchMotor = await _context.Motors.FindAsync(id);
        if (searchMotor == null) return null;

        return new MotorDo
        {
            Id = searchMotor.Id,
            Name = searchMotor.Name,
            Symbol = searchMotor.Symbol,
            Power = searchMotor.Power
        };
    }

    public async Task<List<MotorDo>> GetAllMotors()
    {
        return await _context.Motors
            .Select(motor => new MotorDo
            {
                Id = motor.Id,
                Name = motor.Name,
                Symbol = motor.Symbol,
                Power = motor.Power
            })
            .ToListAsync();
    }

    public async Task<MotorDo?> EditMotor(MotorDo motor)
    {
        var searchMotor = await _context.Motors.FindAsync(motor.Id);
        if (searchMotor == null) return null;

        searchMotor.Name = motor.Name;
        searchMotor.Symbol = motor.Symbol;
        searchMotor.Power = motor.Power;

        await _context.SaveChangesAsync();
        return motor;
    }

    public async Task<bool> DeleteMotor(int id)
    {
        var motor = await _context.Motors.FindAsync(id);
        if (motor == null) return false;

        _context.Motors.Remove(motor);
        await _context.SaveChangesAsync();
        return true;
    }
}
