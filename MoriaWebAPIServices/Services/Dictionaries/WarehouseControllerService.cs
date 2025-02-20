using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModels.Models.Warehouses;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class WarehouseControllerService : IWarehouseControllerService
{
    private readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public WarehouseControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<WarehouseDo> CreateWarehouse(WarehouseDo warehouse)
    {
        var createdWarehouse = await _creator.CreateWarehouse(warehouse);

        _context.Warehouses.Add(createdWarehouse);
        await _context.SaveChangesAsync();

        warehouse.Id = createdWarehouse.Id;
        return warehouse;
    }

    public async Task<WarehouseDo?> GetWarehouseById(int id)
    {
        var searchWarehouse = await _context.Warehouses.FindAsync(id);
        if (searchWarehouse == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        return _creator.GetWarehouse(searchWarehouse);
    }

    public async Task<List<WarehouseDo>> GetAllWarehouses()
    {
        return await _context.Warehouses
            .Select(warehouse => _creator.GetWarehouse(warehouse))
            .ToListAsync();
    }

    public async Task<WarehouseDo?> EditWarehouse(WarehouseDo warehouse)
    {
        var searchWarehouse = await _context.Warehouses.FindAsync(warehouse.Id);
        if (searchWarehouse == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateWarehouse(searchWarehouse, warehouse);
        await _context.SaveChangesAsync();
        return _creator.GetWarehouse(searchWarehouse);
    }

    public async Task<bool> DeleteWarehouse(int id)
    {
        var warehouse = await _context.Warehouses.FindAsync(id);
        if (warehouse == null) return false;

        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync();
        return true;
    }
}
