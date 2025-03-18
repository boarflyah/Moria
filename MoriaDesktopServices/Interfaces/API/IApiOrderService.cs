using MoriaModelsDo.Models.Orders;

namespace MoriaDesktopServices.Interfaces.API;
public interface IApiOrderService
{
    Task<OrderDo> CreateOrder(string username, OrderDo order);
    Task<bool> DeleteOrder(string username, int id);
    Task<OrderDo> GetOrder(string username, int id);
    Task<IEnumerable<OrderDo>> GetOrders(string username);
    Task<OrderDo> UpdateOrder(string username, OrderDo order);
}
