using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Products;
using MoriaModels.Models.Warehouses;
using MoriaWebAPIServices.Contexts;

namespace MoriaModels.Models.Settings;

public class DataGenerator
{
    private readonly ApplicationDbContext _context;

    public DataGenerator(ApplicationDbContext context)
    {
        _context = context;
    }

    public void SeedData()
    {

        if (!_context.Employees.Any()) CreateEmployees();
        if (!_context.Warehouses.Any()) CreateWarehouses();
        if (!_context.Products.Any()) CreateProducts();
        if (!_context.Categories.Any()) CreateCategories();
        if (!_context.SteelKinds.Any()) CreateSteelTypes();
        if (!_context.Orders.Any()) CreateOrders();
        if (!_context.Contacts.Any()) CreateContacts();

        _context.SaveChanges();
    }

    private void CreateEmployees()
    {
        var employees = new List<Employee>();
        for (int i = 1; i <= 40; i++)
        {
            employees.Add(new Employee
            {
                FirstName = $"FirstName{i}",
                LastName = $"LastName{i}",
                Username = $"User{i}",
                Password = $"Pass{i}!",
                PhoneNumber = $"900123{i:D3}",
                Position = new Position { Name = i % 2 == 0 ? "Electrician" : "Assembler" }
            });
        }
        _context.Employees.AddRange(employees);
    }

    private void CreateWarehouses()
    {
        var warehouses = new List<Warehouse>();
        for (int i = 1; i <= 10; i++)
        {
            warehouses.Add(new Warehouse
            {
                Name = $"Warehouse {i}",
                Symbol = $"WH{i:D2}"
            });
        }
        _context.Warehouses.AddRange(warehouses);
    }

    private void CreateProducts()
    {
        var products = new List<Product>();
        for (int i = 1; i <= 40; i++)
        {
            products.Add(new Product
            {
                Name = $"Product-{i}",
                Symbol = $"PR-{i:D3}",
                IsMainMachine = i % 2 == 0,
                SerialNumber = $"SN{i:D6}",
                CategoryId = (i % 4) + 1,     
                SteelKindId = (i % 5) + 1     
            });
        }
        _context.Products.AddRange(products);
    }

    private void CreateCategories()
    {
        var categories = new List<Category>
    {
        new Category { Name = "Machinery" },
        new Category { Name = "Components" },
        new Category { Name = "Tools" },
        new Category { Name = "Accessories" }
    };

        for (int i = 5; i <= 10; i++)
        {
            categories.Add(new Category { Name = $"ExtraCategory-{i}" });
        }

        _context.Categories.AddRange(categories);
    }

    private void CreateSteelTypes()
    {
        var steelTypes = new List<SteelKind>
    {
        new SteelKind { Name = "Carbon Steel", Symbol = "CS" },
        new SteelKind { Name = "Stainless Steel", Symbol = "SS" },
        new SteelKind { Name = "Tool Steel", Symbol = "TS" },
        new SteelKind { Name = "Alloy Steel", Symbol = "AS" },
        new SteelKind { Name = "Spring Steel", Symbol = "SP" }
    };

        for (int i = 6; i <= 10; i++)
        {
            steelTypes.Add(new SteelKind { Name = $"SpecialSteel-{i}", Symbol = $"SS{i}" });
        }

        _context.SteelKinds.AddRange(steelTypes);
    }

    private void CreateOrders()
    {
        var orders = new List<Order>();
        for (int i = 1; i <= 40; i++)
        {
            orders.Add(new Order
            {
                OrderNumberSymbol = $"ORD-{i:D4}",
                Remarks = i % 2 == 0 ? "Special order" : "Standard order",
                CatalogLink = $"https://example.com/catalog{i}",
                ClientSymbol = $"Client-{i:D3}",
                OrderingContactId = (i % 10) + 1,   
                ReceivingContactId = ((i + 1) % 10) + 1
            });
        }
        _context.Orders.AddRange(orders);
    }

    private void CreateContacts()
    {
        var contacts = new List<Contact>();
        for (int i = 1; i <= 10; i++)
        {
            contacts.Add(new Contact
            {
                ShortName = $"Client-{i}",
                LongName = $"Client {i} Corporation",
                Symbol = $"CL{i:D3}"
            });
        }

        _context.Contacts.AddRange(contacts);
    }
}
