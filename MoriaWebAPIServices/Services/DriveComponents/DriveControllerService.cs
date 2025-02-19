using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModels.Models.Products;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.Products;
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

    public async Task<DriveDo> CreateDrive(DriveDo drive)
    {
        var entity = await _context.AddAsync(await _creator.CreateDrive(product));
        var created = await _context.SaveChangesAsync();
        return _creator.GetProductDo(entity.Entity);
    }

    public async Task<DriveDo> UpdateDrive(DriveDo drive)
    {
        var searchProduct = await _context.Drives.Include(x => x.Motor).Include(x => x.MotorGearToDrives).ThenInclude(x => x.MotorGear).FirstOrDefaultAsync(x => x.Id == drive.Id);
        if (searchProduct == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateProduct(searchProduct, product);

        var created = await _context.SaveChangesAsync();
        return _creator.GetProductDo(searchProduct);
    }

    public async Task<bool> DeleteDrive(int id)
    {
        var searchDrive = await _context.Drives.FindAsync(id);
        if (searchDrive == null)
            return false;

        if (searchDrive.IsLocked)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectIsLocked, 406, searchDrive.LockedBy);

        _context.Drives.Remove(searchDrive);

        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<DriveDo> GetDrive(int id)
    {
        var drive = await _context.Drives.Include(x => x.Motor).Include(x => x.MotorGearToDrives).ThenInclude(x => x.MotorGear).FirstOrDefaultAsync(x => x.Id == id);
        if (drive == null)
            return null;

        return _creator.GetDriveDo(drive);
    }

    public async Task<IEnumerable<DriveDo>> GetDrives()
    {
        List<DriveDo> result = new();
        foreach (var drive in _context.Drives)
            result.Add(_creator.GetDriveDo(drive));

        return result;
    }
}
