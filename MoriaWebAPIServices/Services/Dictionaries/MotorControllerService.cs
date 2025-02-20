using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class MotorControllerService : IMotorControllerService
{
    private readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public MotorControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<MotorDo> CreateMotor(MotorDo motor)
    {
        var createdMotor = await _creator.CreateMotor(motor);
       
        _context.Motors.Add(createdMotor);
        await _context.SaveChangesAsync();

        motor.Id = createdMotor.Id;
        return motor;
    }

    public async Task<MotorDo?> GetMotorById(int id)
    {
        var searchMotor = await _context.Motors.FindAsync(id);
        if (searchMotor == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        return _creator.GetMotor(searchMotor);
    }

    public async Task<List<MotorDo>> GetAllMotors()
    {
        return await _context.Motors
            .Select(motor => _creator.GetMotor(motor))          
            .ToListAsync();
    }

    public async Task<MotorDo?> EditMotor(MotorDo motor)
    {
        var searchMotor = await _context.Motors.FindAsync(motor.Id);
        if (searchMotor == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

       await _creator.UpdateMotor(searchMotor, motor);

        await _context.SaveChangesAsync();
        return _creator.GetMotor(searchMotor);
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
