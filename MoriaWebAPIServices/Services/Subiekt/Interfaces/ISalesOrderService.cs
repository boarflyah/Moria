using MoriaDTObjects.Models;

namespace MoriaWebAPIServices.Services.Subiekt.Interfaces;

public interface ISalesOrderService
{
    List<MoriaSalesOrder> GetClosedSalesOrdersDetailed(DateTime dateFrom);
    List<MoriaSalesOrder> GetClosedSalesOrdersSimplified(DateTime dateFrom);
    List<MoriaSalesOrder> GetSalesOrdersSimplified(DateTime dateFrom);
    IEnumerable<MoriaSalesOrder> GetDetailedSalesOrders(IEnumerable<int> ids, ref List<(int, string)> failed);
    bool UpdateOrdersToUpdateValue(IEnumerable<int> ids);
    MoriaSalesOrder GetSalesOrder(int id, ref string error);
    bool UpdateSalesOrder(MoriaSalesOrder model);
    int UpdateEntities();

}
