﻿using MoriaModelsDo.Models.Orders;

namespace MoriaDesktopServices.Interfaces.API;
public interface IApiOrderService
{
    Task<OrderDo> CreateOrder(string username, OrderDo order);
    Task<bool> DeleteOrder(string username, int id);
    Task<OrderDo> GetOrder(string username, int id);
    Task<IEnumerable<OrderDo>> GetOrders(string username);
    Task<IEnumerable<OrderDo>> GetCalendarOrders(string username, int weekNumber);
    Task<OrderDo> UpdateOrder(string username, OrderDo order);
    Task ImportOrders(string username);

    Task<IEnumerable<OrderItemDo>> GetOrderItems(string username);
    Task<OrderItemDo> GetOrderItem(string username, int id);
    Task<OrderItemDo> UpdateElectricOrderItem(string username, OrderItemDo orderItem);

    Task<OrderDo> GetSearchOrder(string username, string symbol);
}
