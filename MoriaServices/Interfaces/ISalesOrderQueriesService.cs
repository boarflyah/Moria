namespace MoriaServices.Interfaces
{
    public interface ISalesOrderQueriesService
    {
        string GetSalesOrdersSimplifiedQuery();
        string GetClosedSalesOrdersSimplifiedQuery();
    }
}
