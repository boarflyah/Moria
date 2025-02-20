using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Interfaces.API;
public interface IApiDriveService
{
    Task<DriveDo> CreateDrive(string username, DriveDo drive);
    Task<bool> DeleteDrive(string username, int id);
    Task<DriveDo> GetDrive(string username, int id);
    Task<IEnumerable<DriveDo>> GetDrives(string username);
    Task<DriveDo> UpdateDrive(string username, DriveDo drive);
}
