using MoriaModelsDo.Models.DriveComponents;

namespace MoriaWebAPIServices.Services.Interfaces.Dictionaries
{
    public interface IMotorGearControllerService
    {
        Task<MotorGearDo> CreateMotorGear(MotorGearDo motorGear);
        Task<bool> DeleteMotorGear(int id);
        Task<MotorGearDo> EditMotorGear(MotorGearDo motorGear);
        Task<List<MotorGearDo>> GetAllMotorGears();
        Task<MotorGearDo> GetMotorGearById(int id);
    }
}