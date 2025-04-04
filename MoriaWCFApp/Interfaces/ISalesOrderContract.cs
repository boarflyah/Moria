﻿using System.Collections.Generic;
using System;
using MoriaDTObjects.Models;
using System.ServiceModel;

namespace MoriaWCFApp.Interfaces
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
        MoriaSalesOrder GetSalesOrder(int id);

    }
}
