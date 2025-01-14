using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Interfaces.API
{
    public interface IApiMotorGearService
    {
        Task<MotorGearDo> CreateMotorGear(string username, MotorGearDo motorGear);
        Task<bool> DeleteMotorGear(string username, int id);
        Task<MotorGearDo> GetMotorGear(string username, int id);
        Task<IEnumerable<MotorGearDo>> GetMotorGears(string username);
        Task<MotorGearDo> UpdateMotorGear(string username, MotorGearDo motorGear);
    }
}