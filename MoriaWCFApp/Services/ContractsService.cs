using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MoriaDTObjects.Models;
using MoriaServices.Interfaces;
using MoriaWCFApp.Interfaces;

namespace MoriaWCFApp.Services
{
    public class ContractsService : ISalesOrderContract
    {
        readonly ISalesOrderService _salesOrderService;
        readonly ILogger<ContractsService> _logger;

        public ContractsService(ISalesOrderService salesOrderService, ILogger<ContractsService> logger)
        {
            _salesOrderService = salesOrderService;
            _logger = logger;
        }

        #region ISalesOrderContract

        public List<MoriaSalesOrder> GetClosedSalesOrdersDetailed(DateTime dateFrom)
        {
            return _salesOrderService.GetClosedSalesOrdersDetailed(dateFrom);
        }

        public List<MoriaSalesOrder> GetClosedSalesOrdersSimplified(DateTime dateFrom)
        {
            return _salesOrderService.GetClosedSalesOrdersSimplified(dateFrom);
        }
        public MoriaSalesOrder GetSalesOrder(int id)
        {
            return _salesOrderService.GetSalesOrder(id);
        }
        public List<MoriaSalesOrder> GetSalesOrdersSimplified(DateTime dateFrom)
        {
            return _salesOrderService.GetSalesOrdersSimplified(dateFrom);
        }

        #endregion
    }
}
