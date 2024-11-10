﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Products;
using MoriaModels.Models.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseGenerator.Settings
{
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
}
