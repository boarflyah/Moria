using MoriaModelsDo.Models.Orders;

namespace MoriaWebAPIServices.Services.Interfaces.Orders;
public interface IOrderControllerService
{
    Task<IEnumerable<OrderDo>> GetOrders();

    Task<OrderDo> GetOrder(int id);

    Task<IEnumerable<OrderDo>> GetCalendarOrders(int weekNumber);

    Task<OrderDo> CreateOrder(OrderDo category);

    Task<OrderDo> UpdateOrder(OrderDo category);

    Task<bool> DeleteOrder(int id);
    Task ImportOrders();
}
