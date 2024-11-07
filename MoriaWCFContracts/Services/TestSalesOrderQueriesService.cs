using MoriaServices.Core;
using MoriaServices.Interfaces;

namespace MoriaWcfContracts.Services
{
    public class TestSalesOrderQueriesService : ISalesOrderQueriesService
    {
        public string GetClosedSalesOrdersSimplifiedQuery() => $"SELECT dok.Id as {ColumnAliasesProvider.ID}, dok.NumerWewnetrzny_PelnaSygnatura as {ColumnAliasesProvider.Symbol}, dok.DataWprowadzenia as {ColumnAliasesProvider.Date}, " +
                        $"dok.KwotaDoZaplaty as {ColumnAliasesProvider.GrossValue}, sd.Nazwa as {ColumnAliasesProvider.StateName} " +
                        $"FROM ModelDanychContainer.Dokumenty dok " +
                        $"LEFT JOIN ModelDanychContainer.StatusyDokumentow sd ON sd.Id = dok.StatusDokumentuId " +
                        $"WHERE dok.DataWprowadzenia >= @p1 AND dok.Symbol LIKE @p2 AND dok.Zamkniety = 1;";
        public string GetSalesOrdersSimplifiedQuery() => $"SELECT dok.Id as {ColumnAliasesProvider.ID}, dok.NumerWewnetrzny_PelnaSygnatura as {ColumnAliasesProvider.Symbol}, dok.DataWprowadzenia as {ColumnAliasesProvider.Date}, " +
                        $"dok.KwotaDoZaplaty as {ColumnAliasesProvider.GrossValue}, sd.Nazwa as {ColumnAliasesProvider.StateName} " +
                        $"FROM ModelDanychContainer.Dokumenty dok " +
                        $"LEFT JOIN ModelDanychContainer.StatusyDokumentow sd ON sd.Id = dok.StatusDokumentuId " +
                        $"WHERE dok.DataWprowadzenia >= @p1 AND dok.Symbol LIKE @p2;";
    }
}
