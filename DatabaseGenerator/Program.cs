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

                context.CreatePPermissionsTrigger();

                var adminPosition = new MoriaModels.Models.EntityPersonel.Position()
                {
                    Name = "Administrator",
                    Code = "ADM"
                };

                context.Positions.Add(adminPosition);

                context.Employees.Add(new MoriaModels.Models.EntityPersonel.Employee()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Username = "admin",
                    Password = "admin",
                    Admin = true,
                    Position = adminPosition
                });

                context.Settings.Add(new MoriaModels.Models.Base.Settings()
                {
                    LastSubiektImport = new DateTime(2025, 1, 1)
                });
                ;

                context.SaveChanges();

                Console.WriteLine("Baza danych została pomyślnie wygenerowana!");
            }
        }
    }
}