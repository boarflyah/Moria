using System.Globalization;
using System.ServiceModel;
using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaDTObjects.Models;
using MoriaModels.Models.Orders;
using MoriaModelsDo.Models.Orders;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces;
using MoriaWebAPIServices.Services.Interfaces.Orders;
using SubiektSalesOrders;

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
       //var entity = await _context.AddAsync(await _creator.CreateOrder(order));

        var result = await _catalogService.CreateCatalogs(order.OrderNumberSymbol);
        if (string.IsNullOrEmpty(result))
            throw new MoriaApiException(MoriaApiExceptionReason.CreateCatalogError, MoriaApiException.ApiExceptionThrownStatusCode);
        order.CatalogLink = result;
        var entity = await _context.AddAsync(await _creator.CreateOrder(order));
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

    public async Task ImportOrders()
    {

        var binding = new BasicHttpBinding();
        binding.SendTimeout = TimeSpan.FromMinutes(5);
        var endpoint = new EndpointAddress("http://localhost:8080/MyService");

        var client = new SalesOrderContractClient(binding, endpoint);
        try
        {

            var settings = await _context.Settings.FirstOrDefaultAsync();
            var orders = await client.GetSalesOrdersSimplifiedAsync(settings?.LastSubiektImport ?? new DateTime(2025, 1, 1));

            if (orders.Any())
            {
                var ids = await GetOrderIds(orders);
                if (ids.Any())
                {
                    var ordersToImport = await client.GetDetailedSalesOrdersAsync(ids.ToArray());
                    foreach (var order in ordersToImport)
                    {
                        var newOrder = await _creator.CreateOrder(order);
                        await _context.AddAsync(newOrder);
                        await _catalogService.CreateCatalogs(newOrder.OrderNumberSymbol);
                    }
                }
                if (settings != null)
                    settings.LastSubiektImport = DateTime.Now;

                _context.SaveChanges();
            }
        }
        finally
        {
            await client.CloseAsync();
        }
    }

    async Task<IEnumerable<int>> GetOrderIds(IEnumerable<MoriaSalesOrder> orders)
    {
        var ids = new List<int>();

        foreach (var order in orders)
        {
            if (!await _context.Orders.AnyAsync(x => x.SubiektId == order.Id))
                ids.Add(order.Id);
        }

        return ids;
    }

    public async Task<IEnumerable<OrderDo>> GetCalendarOrders(int weekNumber)
    {
        DateTime startOfWeek = ISOWeek.ToDateTime(DateTime.Now.Year, weekNumber, DayOfWeek.Monday);
        DateTime endOfWeek = startOfWeek.AddDays(6);

        List<OrderDo> result = new();
        var filteredOrders = _context.Orders
       .Include(x => x.OrderingContact)
       .Include(x => x.ReceivingContact)
       .Include(x => x.OrderItems)
       .ThenInclude(x => x.Product)
       .Include(x => x.OrderItems)
       .ThenInclude(x => x.Component)
       .Include(x => x.OrderItems)
       .ThenInclude(x => x.Drive)
       .Where(x => x.OrderItems.Any(oi => oi.DueDate >= startOfWeek && oi.DueDate <= endOfWeek))
       .AsEnumerable()        
       .ToList();

        if (filteredOrders == null)
            return null;

        foreach (var order in filteredOrders)
        {
            result.Add(_creator.GetOrderDo(order));
        }
        return result;

    }
}
