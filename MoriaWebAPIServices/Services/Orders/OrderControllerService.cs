﻿using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModelsDo.Models.Orders;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces;
using MoriaWebAPIServices.Services.Interfaces.Orders;

namespace MoriaWebAPIServices.Services.Orders;
public class OrderControllerService : IOrderControllerService
{
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;
    readonly ICatalogService _catalogService;

    public OrderControllerService(ApplicationDbContext context, ModelsCreator creator, ICatalogService catalogService)
    {
        _context = context;
        _creator = creator;
        _catalogService = catalogService;
    }

    public async Task<OrderDo> CreateOrder(OrderDo order)
    {
        var entity = await _context.AddAsync(await _creator.CreateOrder(order));

        var result = await _catalogService.CreateCatalogs(order.OrderNumberSymbol);
        if (!result)
            throw new MoriaApiException(MoriaApiExceptionReason.CreateCatalogError, MoriaApiException.ApiExceptionThrownStatusCode);

        var created = await _context.SaveChangesAsync();
        return _creator.GetOrderDo(entity.Entity);
    }

    public async Task<bool> DeleteOrder(int id)
    {
        var serachOrder = await _context.Orders.FindAsync(id);
        if (serachOrder == null)
            return false;

        if (serachOrder.IsLocked)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectIsLocked, 406, serachOrder.LockedBy);

        _context.Orders.Remove(serachOrder);

        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<OrderDo> GetOrder(int id)
    {
        var order = await _context.Orders
            .Include(x => x.OrderingContact)
            .Include(x => x.ReceivingContact)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Component)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Designer)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Drive)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Warehouse)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.ComponentToOrderItems).ThenInclude(x => x.Color)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.ComponentToOrderItems).ThenInclude(x => x.Component).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (order == null)
            return null;

        return _creator.GetOrderDo(order);
    }

    public async Task<IEnumerable<OrderDo>> GetOrders()
    {
        List<OrderDo> result = new();
        foreach (var category in _context.Orders.Include(x => x.OrderingContact).Include(x => x.ReceivingContact))
            result.Add(_creator.GetOrderDo(category));

        return result;
    }

    public async Task<OrderDo> UpdateOrder(OrderDo order)
    {
        var searchOrder = await _context.Orders
            .Include(x => x.OrderingContact)
            .Include(x => x.ReceivingContact)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Component)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Designer)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Drive)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Warehouse)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.ComponentToOrderItems).ThenInclude(x => x.Color)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.ComponentToOrderItems).ThenInclude(x => x.Component).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == order.Id);

        if (searchOrder == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateOrder(searchOrder, order);

        var created = await _context.SaveChangesAsync();
        return _creator.GetOrderDo(searchOrder);
    }
}
