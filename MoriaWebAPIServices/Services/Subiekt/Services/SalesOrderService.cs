using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsERT.Moria.ModelDanych;
using InsERT.Moria.Sfera;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MoriaDTObjects.Models;
using MoriaWebAPIServices.Services.Subiekt.Core;
using MoriaWebAPIServices.Services.Subiekt.Interfaces;

namespace MoriaWebAPIServices.Services.Subiekt.Services;

public class SalesOrderService : ISalesOrderService
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
                var currentOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
                var dt = new DateTimeOffset(dateFrom.Ticks, currentOffset);

                return GetSalesOrders(moriaHandler, _queriesService.GetClosedSalesOrdersSimplifiedQuery(), new Dictionary<string, object>
                {
                    { "@p1", dt },
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
                var currentOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
                var dt = new DateTimeOffset(dateFrom.Ticks, currentOffset);

                return GetSalesOrders(moriaHandler, _queriesService.GetSalesOrdersSimplifiedQuery(), new Dictionary<string, object>
                {
                    { "@p1", dt },
                    { "@p2", "ZK" }
                });
            }
            //catch (Exception ex)
            //{
            //    _logger.LogCritical(ex, "Metoda pobierająca listę wszystkich zamówień");
            //}
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
                if (parameter.Value is DateTimeOffset)
                    cmd.Parameters.Add(parameter.Key, SqlDbType.DateTimeOffset).Value = parameter.Value;
                else
                    cmd.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));

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

    public IEnumerable<MoriaSalesOrder> GetDetailedSalesOrders(IEnumerable<int> ids, ref List<int> failed)
    {
        var moriaHandler = _handlerService.GetHandler();
        if (_handlerService?.Login(moriaHandler) == true)
        {
            var result = new List<MoriaSalesOrder>();
            foreach (var id in ids)
            {
                var order = GetSalesOrder(moriaHandler, id);
                if (order != null)
                    result.Add(order);
                else
                    failed.Add(id);
            }
            return result;
        }
        else
            throw new ArgumentException("Nie udało się zalogować do Sfery");
    }

    public bool UpdateOrdersToUpdateValue(IEnumerable<int> ids)
    {
        var moriaHandler = _handlerService.GetHandler();
        if (_handlerService?.Login(moriaHandler) == true)
        {
            if (ids.Any())
                using (var conn = moriaHandler.PodajPolaczenie())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = GetUpdateQuery(ids);
                    conn.Open();
                    var updated = cmd.ExecuteNonQuery();

                    return updated == ids.Count();
                }

            return true;
        }
        else
            throw new ArgumentException("Nie udało się zalogować do Sfery");
    }

    string GetUpdateQuery(IEnumerable<int> ids)
    {
        string query = $"UPDATE ModelDanychContainer.Dokumenty_PolaWlasneDokumentZK_Adv2 SET B0 = 0 " +
            $"WHERE Id IN ({string.Join(",", ids)});";

        return query;
    }

    public MoriaSalesOrder GetSalesOrder(int id)
    {
        var moriaHandler = _handlerService.GetHandler();
        if (_handlerService?.Login(moriaHandler) == true)
        {
            return GetSalesOrder(moriaHandler, id);
        }
        else
            throw new ArgumentException("Nie udało się zalogować do Sfery");
    }

    public bool UpdateSalesOrder(MoriaSalesOrder model)
    {
        var moriaHandler = _handlerService.GetHandler();
        if (_handlerService?.Login(moriaHandler) == true)
        {
            var document = moriaHandler.PodajObiektTypu<InsERT.Moria.Dokumenty.Logistyka.IZamowieniaOdKlientow>().Dane.Pierwszy(x => x.Id == model.Id);
            if (document != null)
            {
                //using (var documentToUpdate = moriaHandler.ZamowieniaOdKlientow().Znajdz(document))
                //{
                foreach (var soi in model.SalesOrderItems)
                {
                    var toUpdate = document.Pozycje.FirstOrDefault(x => x.Id == soi.Id);
                    if (toUpdate != null)
                    {
                        UpdateSalesOrderItem(toUpdate, soi, moriaHandler);
                    }
                }
                //    documentToUpdate.Przelicz();
                //    documentToUpdate.Zapisz();
                //}
            }
        }

        return true;
    }

    MoriaSalesOrder GetSalesOrder(Uchwyt handler, int id)
    {
        var document = handler.PodajObiektTypu<InsERT.Moria.Dokumenty.Logistyka.IZamowieniaOdKlientow>().Dane.Pierwszy(x => x.Id == id);
        if (document != null)
        {
            try
            {
                return CreateSalesOrder(document);
            }
            catch
            {
            }
        }

        return null;
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
            OfferNumber = document.Podtytul,
            ClientNumber = document.NumerZewnetrzny,
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

    void UpdateSalesOrderItem(PozycjaDokumentu item, MoriaSalesOrderItem model, Uchwyt moriaHandler)
    {
        //item.PolaWlasneAdv2.S0 = model.SerialNumber;
        //item.PolaWlasneAdv2.S1 = model.ProductionYear;
        //item.PolaWlasneAdv2.D0 = model.Power;
        //item.PolaWlasneAdv2.I0 = model.Weight;

        using (var conn = moriaHandler.PodajPolaczenie())
        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "UPDATE ModelDanychContainer.PozycjeDokumentu_PolaWlasnePozycjaDokumentu_Adv2 SET S0 = @p1, S1 = @p2, D0 = @p3, I0 = @p4 WHERE Id = @p5";
            cmd.Parameters.Add(new SqlParameter("@p1", model.SerialNumber));
            cmd.Parameters.Add(new SqlParameter("@p2", model.ProductionYear));
            cmd.Parameters.Add(new SqlParameter("@p3", model.Power));
            cmd.Parameters.Add(new SqlParameter("@p4", model.Weight));
            cmd.Parameters.Add(new SqlParameter("@p5", item.Id));

            conn.Open();
            var affected = cmd.ExecuteNonQuery();
        }
    }
}
