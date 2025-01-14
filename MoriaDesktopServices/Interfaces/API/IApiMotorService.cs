using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Interfaces.API
{
    public interface IApiMotorService
    {
        Task<MotorDo> CreateMotor(string username, MotorDo motor);
        Task<bool> DeleteMotor(string username, int id);
        Task<MotorDo> GetMotor(string username, int id);
        Task<IEnumerable<MotorDo>> GetMotors(string username);
        Task<MotorDo> UpdateMotor(string username, MotorDo motor);
    }
}