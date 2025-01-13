using MoriaModelsDo.Models.Dictionaries;

namespace MoriaWebAPIServices.Services.Interfaces.Dictionaries
{
    public interface IWarehouseControllerService
    {
        Task<WarehouseDo> CreateWarehouse(WarehouseDo warehouse);
        Task<bool> DeleteWarehouse(int id);
        Task<WarehouseDo> EditWarehouse(WarehouseDo warehouse);
        Task<List<WarehouseDo>> GetAllWarehouses();
        Task<WarehouseDo> GetWarehouseById(int id);
    }
}