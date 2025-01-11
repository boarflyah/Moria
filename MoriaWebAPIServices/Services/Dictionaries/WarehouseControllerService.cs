using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.Warehouses;
using MoriaModelsDo.Models.Dictionaries;
using MoriaWebAPIServices.Contexts;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class WarehouseControllerService
{
    private readonly ApplicationDbContext _context;

    public WarehouseControllerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WarehouseDo> CreateWarehouse(WarehouseDo warehouse)
    {
        var createdWarehouse = new Warehouse
        {
            Name = warehouse.Name,
            Symbol = warehouse.Symbol
        };

        _context.Warehouses.Add(createdWarehouse);
        await _context.SaveChangesAsync();

        warehouse.Id = createdWarehouse.Id;
        return warehouse;
    }

    public async Task<WarehouseDo?> GetWarehouseById(int id)
    {
        var searchWarehouse = await _context.Warehouses.FindAsync(id);
        if (searchWarehouse == null) return null;

        return new WarehouseDo
        {
            Id = searchWarehouse.Id,
            Name = searchWarehouse.Name,
            Symbol = searchWarehouse.Symbol
        };
    }

    public async Task<List<WarehouseDo>> GetAllWarehouses()
    {
        return await _context.Warehouses
            .Select(warehouse => new WarehouseDo
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Symbol = warehouse.Symbol
            })
            .ToListAsync();
    }

    public async Task<WarehouseDo?> EditWarehouse(WarehouseDo warehouse)
    {
        var searchWarehouse = await _context.Warehouses.FindAsync(warehouse.Id);
        if (searchWarehouse == null) return null;

        searchWarehouse.Name = warehouse.Name;
        searchWarehouse.Symbol = warehouse.Symbol;

        await _context.SaveChangesAsync();
        return warehouse;
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
