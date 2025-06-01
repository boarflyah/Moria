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
                .ThenInclude( x=> x.Electrician)
             .Include(x => x.OrderItems)
                .ThenInclude(x => x.ElectricalCabinet)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Warehouse)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.MainColor)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.DetailsColor)
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
                .ThenInclude(x => x.MainColor)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.DetailsColor)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.ComponentToOrderItems).ThenInclude(x => x.Color)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.ComponentToOrderItems).ThenInclude(x => x.Component).ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == order.Id);

        if (searchOrder == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateOrder(searchOrder, order);

        var created = await _context.SaveChangesAsync();

        if (searchOrder.SubiektId != 0 && searchOrder.OrderItems.Any(x => x.SubiektId != 0))
        {
            var binding = new BasicHttpBinding();
            binding.SendTimeout = TimeSpan.FromMinutes(5);
            var endpoint = new EndpointAddress("http://localhost:8080/MyService");

            var client = new SalesOrderContractClient(binding, endpoint);
            try
            {
                MoriaSalesOrder mso = new()
                {
                    Id = searchOrder.SubiektId,
                    SalesOrderItems = new()
                };
                foreach (var oi in searchOrder.OrderItems.Where(x => x.SubiektId != 0))
                {
                    MoriaSalesOrderItem msoi = new()
                    {
                        Id = oi.SubiektId,
                        ProductionYear = oi.ProductionYear,
                        Weight = oi.MachineWeight,
                        SerialNumber = oi.SerialNumber,
                        Power = oi.Power,
                    };
                    mso.SalesOrderItems.Add(msoi);
                }
//update w subiekcie na razie zakomentowany w debugu
#if DEBUG==false
                client.UpdateSalesOrderAsync(mso);
#endif
            }
            finally
            {
                await client.CloseAsync();
            }
        }

        return _creator.GetOrderDo(searchOrder);
    }

    public async Task ImportOrders()
    {
        var binding = new BasicHttpBinding()
        {
            MaxReceivedMessageSize = int.MaxValue,
            MaxBufferSize = int.MaxValue,
            SendTimeout = TimeSpan.FromMinutes(5),
            ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas
            {
                MaxDepth = 64,
                MaxStringContentLength = int.MaxValue,
                MaxArrayLength = int.MaxValue,
                MaxBytesPerRead = 4096,
                MaxNameTableCharCount = int.MaxValue
            }
        };

        var endpoint = new EndpointAddress("http://localhost:8080/MyService");

        var client = new SalesOrderContractClient(binding, endpoint);
        try
        {

            var settings = await _context.Settings.FirstOrDefaultAsync();
            var orders = await client.GetSalesOrdersSimplifiedAsync(settings?.LastSubiektImport.AddMonths(-6) ?? new DateTime(2025, 1, 1));

            if (orders.Any())
            {
                var ids = await GetOrderIds(orders);
                if (ids.Any())
                {
                    var ordersToImport = await client.GetDetailedSalesOrdersAsync(ids.ToArray());
                    bool updateB0 = false;
                    foreach (var order in ordersToImport)
                    {
                        updateB0 = true;
                        var toUpdate = await _context.Orders
                            .Include(x => x.OrderingContact)
                            .Include(x => x.ReceivingContact)
                            .Include(x => x.OrderItems)
                            .ThenInclude(x => x.Warehouse)
                            .Include(x => x.OrderItems)
                            .ThenInclude(x => x.Product)
                            .FirstOrDefaultAsync(x => x.SubiektId == order.Id);
                        if (toUpdate == null)
                        {
                            var newOrder = await _creator.CreateOrder(order);
                            await _context.AddAsync(newOrder);
                            newOrder.CatalogLink = await _catalogService.CreateCatalogs(newOrder.OrderNumberSymbol);
                        }
                        else
                        {
                            await _creator.UpdateOrder(order, toUpdate);
                        }
                    }

                    if (updateB0)
                        await client.UpdateOrdersToUpdateValueAsync(ids.ToArray());
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

    async Task<IEnumerable<int>> GetOrderIds(IEnumerable<MoriaSalesOrder> orders, bool getAll = true)
    {
        var ids = new List<int>();

        foreach (var order in orders)
        {
            if (getAll)
                ids.Add(order.Id);
            else if (!await _context.Orders.AnyAsync(x => x.SubiektId == order.Id))
                ids.Add(order.Id);
        }

        return ids;
    }

    public async Task<IEnumerable<OrderDo>> GetCalendarOrders(int weekNumber)
    {
        DateTime startOfWeek = ISOWeek.ToDateTime(DateTime.Now.Year, weekNumber, DayOfWeek.Monday);
        DateTime endOfWeek = startOfWeek.AddDays(6);

        var filteredOrders = await _context.Orders
             .Include(x => x.OrderingContact)
             .Include(x => x.ReceivingContact)
             .Include(x => x.OrderItems)
                 .ThenInclude(x => x.Product)
             .Include(x => x.OrderItems)
                 .ThenInclude(x => x.Component)
             .Include(x => x.OrderItems)
                 .ThenInclude(x => x.Drive)
             .Where(x => x.OrderItems.Any(oi => oi.DueDate >= startOfWeek && oi.DueDate <= endOfWeek))
             .ToListAsync();

        if (!filteredOrders.Any())
            return Enumerable.Empty<OrderDo>();

        return filteredOrders.Select(order => _creator.GetOrderDo(order));

    }

    public async Task<IEnumerable<OrderItemDo>> GetOrderItems()
    {
        List<OrderItemDo> result = new();
        foreach (var orderItem in _context.OrderItems.Include(x=> x.Product).Include(x => x.ElectricalCabinet).Include(x => x.Electrician).Include(x => x.Order))
            result.Add(_creator.GetOrderItemDo(orderItem));

        return result;
    }

    public async Task<OrderItemDo> UpdateElectricOrderItem(OrderItemDo item)
    {
        var searchOrderItem = await _context.OrderItems
            .Include(x => x.Electrician)
            .Include(x => x.ElectricalCabinet)
            .Include(x => x.Product)            
            .FirstOrDefaultAsync(x => x.Id == item.Id);

        if (searchOrderItem == null)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        await _creator.UpdateElectricOrderItem(searchOrderItem, item);

        var created = await _context.SaveChangesAsync();
        return _creator.GetOrderItemDo(searchOrderItem);
    }

    public async Task<OrderItemDo> GetOrderItem(int id)
    {

        var orderItem = await _context.OrderItems
            .Include(x => x.Electrician)
            .Include(x => x.ElectricalCabinet)
            .Include(x => x.Product)            
            .FirstOrDefaultAsync(x => x.Id == id);
        if (orderItem == null)
            return null;

        return _creator.GetOrderItemDo(orderItem);
    }
}
