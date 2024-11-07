using System;
using System.Collections.Generic;
using Moria.MiddlewareService.Interfaces;
using MoriaDTObjects.Models;
using MoriaServices.Interfaces;

namespace Moria.MiddlewareService.Services
{
    public class ContractsService : ISalesOrder
    {
        //[Dependency]
        public IMoriaHandlerService _moriaHandlerService
        {
            get; set;
        }

        //readonly IMoriaHandlerService _moriaHandlerService;

        //public ContractsService()
        //{
        //}
        //public ContractsService(ILogger logger)
        //{
        //    ;
        //}

        public ContractsService(IMoriaHandlerService handlerService)
        {
            _moriaHandlerService = handlerService;
        }

        public List<MoriaSalesOrder> GetClosedSalesOrdersDetailed(DateTime dateFrom) => throw new System.NotImplementedException();
        public List<MoriaSalesOrder> GetClosedSalesOrdersSimplified(DateTime dateFrom) => throw new System.NotImplementedException();
        public List<MoriaSalesOrder> GetSalesOrdersSimplified(DateTime dateFrom)
        {
            var xx = _moriaHandlerService;
            var moriaHandler = _moriaHandlerService.GetHandler();

            List<MoriaSalesOrder> result = new List<MoriaSalesOrder>();

            using (var conn = moriaHandler.PodajPolaczenie())
            using (var cmd = conn.CreateCommand())
            {
                var query = $"SELECT dok.Id, dok.NumerWewnetrzny_PelnaSygnatura, dok.DataWprowadzenia, dok.KwotaDoZaplaty, " +
                    $"sd.Nazwa " +
                    $"FROM Dokumenty dok " +
                    $"LEFT JOIN StatusyDokumentow sd ON sd.Id = dok.StatusDokumentuId " +
                    $"WHERE dok.DataWprowadzenia >= @p1;";

                cmd.CommandText = query;
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@p1", dateFrom));
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MoriaSalesOrder()
                        {
                            Id = reader.GetInt32(0),
                            DocumentDate = reader.GetDateTime(2),
                            Symbol = reader.GetString(1)
                        });
                    }
                }
            }
            return result;
        }
        public MoriaSalesOrder GetSalesOrder(int id) => throw new System.NotImplementedException();


        //var uzytkownicy = new List<Uzytkownik>();
        //using (var conn = x.PodajPolaczenie())
        //using (var cmd = conn.CreateCommand())
        //{
        //    cmd.CommandText = "SELECT Id, Nazwa, Login FROM ModelDanychContainer.Uzytkownicy WHERE Ukryty = 0 AND NOT Osoba_Id IS NULL; ";
        //conn.Open();
        //    using (var reader = cmd.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            var u = new Uzytkownik();
        //            u.Id = reader.GetGuid(0);
        //            u.Nazwa = reader.GetString(1);
        //            u.Login = reader.GetString(2);
        //            uzytkownicy.Add(u);
        //        }
        //    }
        //}

    }
}
