using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.Extensions.Configuration;
using MoriaBaseServices;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPIServices.Services;

public class CatalogService : ICatalogService
{

    // w cmd  net user
    // net user konkretny albo net user /domain

    private readonly IConfiguration _configuration;

    public List<string> Users { get; set; } = new();
    public List<string> CanOpenProduction { get; set; } = new();
    public List<string> CanOpenTrade { get; set; } = new();
    public string folderPath { get; set; }

    public CatalogService(IConfiguration configuration)
    {
        _configuration = configuration;
        Users = _configuration.GetSection("Users").Get<List<string>>() ?? new List<string>();
        CanOpenProduction = _configuration.GetSection("Permissions:CanOpenProduction").Get<List<string>>() ?? new List<string>();
        CanOpenTrade = _configuration.GetSection("Permissions:CanOpenTrade").Get<List<string>>() ?? new List<string>();
        folderPath = _configuration.GetValue<string>("MainCatalogPath");
    }

    async Task<string> ICatalogService.CreateCatalogs(string orderSymbol)
    {
        if (folderPath == null) return null;

        var mainCatalog = folderPath + @"\" + orderSymbol.GetFolderName();
        DirectoryInfo directory = new DirectoryInfo(mainCatalog);

        if (!directory.Exists)
        {
            directory.Create();
        }

        // Production

        var roductionCatalogPath = mainCatalog + @"\_Produkcja";
        DirectoryInfo directoryProduction = new DirectoryInfo(roductionCatalogPath);

        if (!directoryProduction.Exists)
        {
            directoryProduction.Create();
        }

        DirectorySecurity security = directoryProduction.GetAccessControl();
        foreach (var user in Users.Except(CanOpenProduction).ToList())
        {
            try
            {
                FileSystemAccessRule denyRule = new FileSystemAccessRule(
                          user,
                          FileSystemRights.FullControl,
                          InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                          PropagationFlags.None,
                          AccessControlType.Deny);

                security.AddAccessRule(denyRule);

                directoryProduction.SetAccessControl(security);

            }
            catch (Exception ex)
            { }
        }
    

        // Trade

        var tradeCatalogPath = mainCatalog + @"\_Handlowe";
        DirectoryInfo directoryTrade = new DirectoryInfo(tradeCatalogPath);

        if (!directoryTrade.Exists)
        {
            directoryTrade.Create();
        }

        DirectorySecurity securityTrade = directoryTrade.GetAccessControl();
        foreach (var user in Users.Except(CanOpenTrade).ToList())
        {
            try
            {
                FileSystemAccessRule denyRule = new FileSystemAccessRule(
                          user,
                          FileSystemRights.FullControl,
                          InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                          PropagationFlags.None,
                          AccessControlType.Deny);

                securityTrade.AddAccessRule(denyRule);

                directoryTrade.SetAccessControl(securityTrade);
            }
            catch (Exception ex) 
            { }
        }

        //try
        //{            
        //    NTAccount account = new NTAccount(user);

        //    SecurityIdentifier sid = (SecurityIdentifier)account.Translate(typeof(SecurityIdentifier));
        //}
        //catch (Exception ex)
        //{
        //    ;
        //}

        return mainCatalog;
    }
}
