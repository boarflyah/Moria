using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.DriveComponents.Relations;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Products;
using MoriaModels.Models.Warehouses;

namespace MoriaWebAPIServices.Contexts;

public class ApplicationDbContext : DbContext
{
    protected readonly IConfiguration _configuration;
    public ApplicationDbContext(IConfiguration configuration )
    {
        _configuration = configuration;       
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //https://www.youtube.com/watch?v=z7G6HV7WWz0
        //base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("MoriaDataBase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Drive>()
            .HasMany(e => e.MotorGears)
            .WithMany(e => e.Drives)
            .UsingEntity<MotorGearToDrive>();
    }

    public IQueryable<BaseModel> Get(Type objectType)
    {
        var methodInfo = GetType().GetMethod(nameof(Set), Type.EmptyTypes);
        if (methodInfo != null)
        {
            var method = methodInfo.MakeGenericMethod(objectType);
            if (method != null)
            {

                var collection = method.Invoke(this, null);
                if (collection is IQueryable<BaseModel> dbset)
                {
                    return dbset;
                }
            }
        }
        throw new InvalidOperationException("Failed to retrieve DbSet.");
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
