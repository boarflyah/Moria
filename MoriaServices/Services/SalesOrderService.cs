using System;
using System.Collections.Generic;
using InsERT.Moria.ModelDanych;
using InsERT.Moria.Sfera;
using Microsoft.Extensions.Logging;
using MoriaDTObjects.Models;
using MoriaServices.Core;
using MoriaServices.Interfaces;

namespace MoriaServices.Services
{
    public class SalesOrderService: ISalesOrderService
    {
        readonly IMoriaHandlerService _handlerService;
        readonly ISalesOrderQueriesService _queriesService;
        readonly IDictionariesService _dictionariesService;
        readonly ILogger<SalesOrderService> _logger;

        public SalesOrderService(IMoriaHandlerService handlerService, ISalesOrderQueriesService queriesService,
            IDictionariesService dictionariesService, ILogger<SalesOrderService> logger)
        {
            _handlerService = handlerService;
            _logger = logger;
            _queriesService = queriesService;
            _dictionariesService = dictionariesService;
        }

        public List<MoriaSalesOrder> GetClosedSalesOrdersDetailed(DateTime dateFrom)
        {
            var moriaHandler = _handlerService.GetHandler();
            if (moriaHandler != null)
            {
                try
                {
                    return GetSalesOrders(moriaHandler, _queriesService.GetClosedSalesOrdersSimplifiedQuery(), new Dictionary<string, object>
                    {
                        { "@p1", dateFrom },
                        { "@p2", "ZK" }
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Metoda pobierająca listę wszystkich zamówień");
                }
                finally
                {
                    moriaHandler?.Dispose();
                }
            }

            return new List<MoriaSalesOrder>();
        }
        public List<MoriaSalesOrder> GetClosedSalesOrdersSimplified(DateTime dateFrom) => throw new System.NotImplementedException();
        public List<MoriaSalesOrder> GetSalesOrdersSimplified(DateTime dateFrom)
        {
            var moriaHandler = _handlerService.GetHandler();
            if (moriaHandler != null)
            {
                try
                {
                    return GetSalesOrders(moriaHandler, _queriesService.GetSalesOrdersSimplifiedQuery(), new Dictionary<string, object>
                    {
                        { "@p1", dateFrom },
                        { "@p2", "ZK" }
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Metoda pobierająca listę wszystkich zamówień");
                }
                finally
                {
                    moriaHandler?.Dispose();
                }
            }

            return new List<MoriaSalesOrder>();
        }

        private List<MoriaSalesOrder> GetSalesOrders(Uchwyt moriaHandler, string query, Dictionary<string, object> queryParameters)
        {
            List<MoriaSalesOrder> result = new List<MoriaSalesOrder>();
            using (var conn = moriaHandler.PodajPolaczenie())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                foreach (var parameter in queryParameters)
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter(parameter.Key, parameter.Value));

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        int idColumn = reader.GetOrdinal(ColumnAliasesProvider.ID),
                            symbolColumn = reader.GetOrdinal(ColumnAliasesProvider.Symbol),
                            dateColumn = reader.GetOrdinal(ColumnAliasesProvider.Date),
                            grossValue = reader.GetOrdinal(ColumnAliasesProvider.GrossValue),
                            stateColumn = reader.GetOrdinal(ColumnAliasesProvider.StateName);
                        while (reader.Read())
                        {
                            result.Add(new MoriaSalesOrder()
                            {
                                Id = reader.GetInt32(idColumn),
                                DocumentDate = reader.GetDateTime(dateColumn),
                                Symbol = reader.GetString(symbolColumn),
                                StateName = reader.GetString(stateColumn)
                            });
                        }
                        reader.Close();
                    }
                }
            }

            return result;
        }

        public MoriaSalesOrder GetSalesOrder(int id)
        {
            var moriaHandler = _handlerService.GetHandler();
            if (_handlerService?.Login(moriaHandler) == true)
            {
                var document = moriaHandler.PodajObiektTypu<InsERT.Moria.Dokumenty.Logistyka.IZamowieniaOdKlientow>().Dane.Pierwszy(x => x.Id == id);
                if (document != null)
                    return CreateSalesOrder(document);

                return null;
            }
            else
                throw new ArgumentException("Nie udało się zalogować do Sfery");
        }



        MoriaSalesOrder CreateSalesOrder(DokumentZK document)
        {
            var mso = new MoriaSalesOrder()
            {
                Id = document.Id,
                Symbol = document.NumerWewnetrzny?.PelnaSygnatura ?? document.Symbol,
                DocumentDate = document.DataWprowadzenia,
                StateName = document.StatusDokumentu.Nazwa,
                Remarks = document.Uwagi,
                Warehouse = _dictionariesService.CreateWarehouse(document.Magazyn),
                Client = _dictionariesService.CreateEntity(document.PodmiotZamawiajacy()),
                Recipient = _dictionariesService.CreateEntity(document.PodmiotOdbiorca())
            };
            
            foreach (var item in document.Pozycje)
            {
                var soi = _dictionariesService.CreateSalesOrderItem(item);
                if (soi != null)
                    mso.SalesOrderItems.Add(soi);
            }

            return mso;
        }
    }
}
