using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktopServices.Interfaces.API
{
    public interface IApiWarehouseService
    {
        Task<WarehouseDo> CreateWarehouse(string username, WarehouseDo warehouse);
        Task<bool> DeleteWarehouse(string username, int id);
        Task<WarehouseDo> GetWarehouse(string username, int id);
        Task<IEnumerable<WarehouseDo>> GetWarehouses(string username);
        Task<WarehouseDo> UpdateWarehouse(string username, WarehouseDo warehouse);
    }
}