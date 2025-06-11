using MoriaServices.Core;
using MoriaServices.Interfaces;

namespace MoriaConsoleApp.Services
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
                        //$"LEFT JOIN ModelDanychContainer.Dokumenty_PolaWlasneDokumentZK_Adv2 pw ON pw.Id = dok.Id " +
                        $"LEFT JOIN ModelDanychContainer.NaglowkiEncji ne ON ne.Id = dok.Dokument_Naglowek_Id " +
                        //$"WHERE dok.DataWprowadzenia >= @p1 AND dok.Symbol LIKE @p2 AND pw.B0 = 1;";
                        $"WHERE ne.Zmieniono >= @p1 AND dok.Symbol LIKE @p2;";
    }
}
