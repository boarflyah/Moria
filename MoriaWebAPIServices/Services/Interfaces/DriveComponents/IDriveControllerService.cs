using MoriaModelsDo.Models.DriveComponents;

namespace MoriaWebAPIServices.Services.Interfaces.DriveComponents;
public interface IDriveControllerService
{
    Task<IEnumerable<DriveDo>> GetDrives();

    Task<DriveDo> GetDrive(int id);

    Task<DriveDo> CreateDrive(DriveDo drive);

    Task<DriveDo> UpdateDrive(DriveDo drive);

    Task<bool> DeleteDrive(int id);
}
