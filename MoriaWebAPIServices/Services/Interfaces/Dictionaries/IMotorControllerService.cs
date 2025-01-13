using MoriaModelsDo.Models.DriveComponents;

namespace MoriaWebAPIServices.Services.Interfaces.Dictionaries
{
    public interface IMotorControllerService
    {
        Task<MotorDo> CreateMotor(MotorDo motor);
        Task<bool> DeleteMotor(int id);
        Task<MotorDo> EditMotor(MotorDo motor);
        Task<List<MotorDo>> GetAllMotors();
        Task<MotorDo> GetMotorById(int id);
    }
}