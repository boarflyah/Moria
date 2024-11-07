using System;
using System.Collections.Generic;
using System.ServiceModel;
using MoriaDTObjects.Models;

namespace Moria.MiddlewareService.Interfaces
{
    [ServiceContract]
    public interface ISalesOrder
    {
        [OperationContract]
        List<MoriaSalesOrder> GetClosedSalesOrdersSimplified(DateTime dateFrom);

        [OperationContract]
        List<MoriaSalesOrder> GetClosedSalesOrdersDetailed(DateTime dateFrom);

        [OperationContract]
        MoriaSalesOrder GetSalesOrder(int id);

        [OperationContract]
        List<MoriaSalesOrder> GetSalesOrdersSimplified(DateTime dateFrom);
    }
}
