using MoriaDTObjects.Models;
using System.Collections.Generic;
using System;

namespace MoriaServices.Interfaces
{
    public interface ISalesOrderService
    {
        List<MoriaSalesOrder> GetClosedSalesOrdersDetailed(DateTime dateFrom);
        List<MoriaSalesOrder> GetClosedSalesOrdersSimplified(DateTime dateFrom);
        List<MoriaSalesOrder> GetSalesOrdersSimplified(DateTime dateFrom);
        IEnumerable<MoriaSalesOrder> GetDetailedSalesOrders(IEnumerable<int> ids);
        MoriaSalesOrder GetSalesOrder(int id);

    }
}
