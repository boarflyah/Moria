using System.Collections.Generic;
using System;
using MoriaDTObjects.Models;
using System.ServiceModel;

namespace MoriaWCFContracts.Interfaces
{
    [ServiceContract]
    public interface ISalesOrderContract
    {
        [OperationContract]
        List<MoriaSalesOrder> GetClosedSalesOrdersDetailed(DateTime dateFrom);
        
        [OperationContract]
        List<MoriaSalesOrder> GetClosedSalesOrdersSimplified(DateTime dateFrom);
        
        [OperationContract]
        List<MoriaSalesOrder> GetSalesOrdersSimplified(DateTime dateFrom);

        [OperationContract]
        IEnumerable<MoriaSalesOrder> GetDetailedSalesOrders(IEnumerable<int> ids);

        [OperationContract]
        MoriaSalesOrder GetSalesOrder(int id);

        [OperationContract]
        bool UpdateSalesOrder(MoriaSalesOrder so);

    }
}
