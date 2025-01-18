using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DatabaseGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

           
            var optionsBuilder = new DbContextOptionsBuilder<MoriaDataContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("MoriaDatabase"));

            using (var context = new MoriaDataContext(optionsBuilder.Options))
            {
                
                context.Database.Migrate();

                context.Employees.Add(new MoriaModels.Models.EntityPersonel.Employee()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Username = "admin",
                    Password = "admin",
                    Admin = true,
                });

                context.SaveChanges();

                Console.WriteLine("Baza danych została pomyślnie wygenerowana!");
            }
        }
    }
}