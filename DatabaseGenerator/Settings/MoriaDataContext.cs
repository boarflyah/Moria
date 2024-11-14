using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Products;
using MoriaModels.Models.Warehouses;

public class MoriaDataContext : DbContext
{
    public MoriaDataContext(DbContextOptions<MoriaDataContext> options) : base(options)
    {
    }

    // Konstruktor bezparametrowy dla narzędzi migracji
    public MoriaDataContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
        // base.OnModelCreating(modelBuilder);
    }

    public DbSet<SteelKind> SteelKinds { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<MotorGear> MotorGears { get; set; }
    public DbSet<Motor> Motors { get; set; }
    public DbSet<Drive> Drives { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Component> Components { get; set; }
}
