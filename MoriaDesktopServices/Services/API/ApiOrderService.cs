﻿using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Orders;

namespace MoriaDesktopServices.Services.API;
public class ApiOrderService : IApiOrderService
{
    readonly IApiService _apiService;

    public ApiOrderService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<OrderDo> CreateOrder(string username, OrderDo order)
    {
        return await _apiService.Post<OrderDo>(username, WebAPIEndpointsProvider.PostOrderPath, null, order);
    }
    public async Task<bool> DeleteOrder(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteOrderPath, null, id);
    }
    public async Task<OrderDo> GetOrder(string username, int id)
    {
        return await _apiService.Get<OrderDo>(username, WebAPIEndpointsProvider.GetOrderPath, null, null, id);
    }
    public async Task<IEnumerable<OrderDo>> GetOrders(string username)
    {
        var result = await _apiService.Get<IEnumerable<OrderDo>>(username, WebAPIEndpointsProvider.GetOrdersPath, null, null);
        if (result == null)
            return new List<OrderDo>();

        return result;
    }
    public async Task<OrderDo> UpdateOrder(string username, OrderDo order)
    {
        return await _apiService.Put<OrderDo>(username, WebAPIEndpointsProvider.PutOrderPath, null, order);
    }

    public async Task<IEnumerable<OrderDo>> GetCalendarOrders(string username, int weekNumber)
    {
        var result = await _apiService.Get<IEnumerable<OrderDo>>(username, WebAPIEndpointsProvider.GetCalendarOrdersPath, null, null, weekNumber);
        if (result == null)
            return new List<OrderDo>();

        return result;
    }
}
