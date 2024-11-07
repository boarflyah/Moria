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
        MoriaSalesOrder GetSalesOrder(int id);

    }
}
