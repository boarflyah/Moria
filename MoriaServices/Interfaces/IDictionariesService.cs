using InsERT.Moria.ModelDanych;
using MoriaDTObjects.Models;

namespace MoriaServices.Interfaces
{
    public interface IDictionariesService
    {
        MoriaEntity CreateEntity(Podmiot entity);
        MoriaProduct CreateProduct(Asortyment product);
        MoriaSalesOrderItem CreateSalesOrderItem(PozycjaDokumentu item);
        MoriaWarehouse CreateWarehouse(Magazyn warehouse);
    }
}