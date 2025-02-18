using MoriaModelsDo.Models.DriveComponents;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.DriveComponents;

namespace MoriaWebAPIServices.Services.DriveComponents;
public class DriveControllerService : IDriveControllerService
{
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public DriveControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public Task<DriveDo> CreateDrive(DriveDo drive) => throw new NotImplementedException();
    public Task<bool> DeleteDrive(int id) => throw new NotImplementedException();
    public Task<DriveDo> GetDrive(int id) => throw new NotImplementedException();
    public Task<IEnumerable<DriveDo>> GetDrives() => throw new NotImplementedException();
    public Task<DriveDo> UpdateDrive(DriveDo drive) => throw new NotImplementedException();
}
