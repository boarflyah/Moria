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
    private readonly IConfiguration _configuration;

    public List<string> Users { get; set; } = new();
    public List<string> CanReadProduction { get; set; } = new();
    public List<string> CanEditProduction { get; set; } = new();
    public List<string> CanReadTrade { get; set; } = new();
    public List<string> CanEditTrade { get; set; } = new();
    public List<string> CanReadElectric { get; set; } = new();
    public List<string> CanEditElectric { get; set; } = new();
    public List<string> CanReadWarehouse { get; set; } = new();
    public List<string> CanEditWarehouse { get; set; } = new();
    public List<string> CanReadSales { get; set; } = new();
    public List<string> CanEditSales { get; set; } = new();

    public string folderPath { get; set; }

    public CatalogService(IConfiguration configuration)
    {
        _configuration = configuration;

        Users = _configuration.GetSection("Users").Get<List<string>>() ?? new();
        CanReadProduction = _configuration.GetSection("Permissions:Production:Read").Get<List<string>>() ?? new();
        CanEditProduction = _configuration.GetSection("Permissions:Production:Edit").Get<List<string>>() ?? new();
        CanReadTrade = _configuration.GetSection("Permissions:Trade:Read").Get<List<string>>() ?? new();
        CanEditTrade = _configuration.GetSection("Permissions:Trade:Edit").Get<List<string>>() ?? new();
        CanReadElectric = _configuration.GetSection("Permissions:Electric:Read").Get<List<string>>() ?? new();
        CanEditElectric = _configuration.GetSection("Permissions:Electric:Edit").Get<List<string>>() ?? new();
        CanReadWarehouse = _configuration.GetSection("Permissions:Warehouse:Read").Get<List<string>>() ?? new();
        CanEditWarehouse = _configuration.GetSection("Permissions:Warehouse:Edit").Get<List<string>>() ?? new();
        CanReadSales = _configuration.GetSection("Permissions:Sales:Read").Get<List<string>>() ?? new();
        CanEditSales = _configuration.GetSection("Permissions:Sales:Edit").Get<List<string>>() ?? new();

        folderPath = _configuration.GetValue<string>("MainCatalogPath");
    }

    public async Task<string> CreateCatalogs(string orderSymbol)
    {
        if (string.IsNullOrEmpty(folderPath)) return null;

        var mainCatalog = Path.Combine(folderPath, orderSymbol.GetFolderName());

        if (!Directory.Exists(mainCatalog))
        {
            Directory.CreateDirectory(mainCatalog);
        }

       

        await CreateSecuredFolder(mainCatalog, "Produkcja", CanReadProduction, CanEditProduction);
        await CreateSecuredFolder(mainCatalog, "Handel", CanReadTrade, CanEditTrade);
        await CreateSecuredFolder(mainCatalog, "Magazyn", CanReadWarehouse, CanEditWarehouse);
        await CreateSecuredFolder(mainCatalog, "Elektryka", CanReadElectric, CanEditElectric);

        var handelPath = Path.Combine(mainCatalog, "Handel");
        var salesUsers = CanReadSales.Concat(CanEditSales).Distinct(StringComparer.OrdinalIgnoreCase);
        EnsureParentFolderAccess(handelPath, salesUsers);

        await CreateSecuredFolder(handelPath, "Sprzedaż", CanReadSales, CanEditSales);

        return mainCatalog;
    }

    private async Task CreateSecuredFolder(string parentPath, string folderName, List<string> canRead, List<string> canEdit)
    {
        var fullPath = Path.Combine(parentPath, folderName);
        DirectoryInfo directory = new(fullPath);

        if (!directory.Exists)
        {
            directory.Create();
        }

        DirectorySecurity security = new();
        security.SetAccessRuleProtection(true, false); 

        foreach (var user in Users)
        {
            try
            {
                var rights = GetRights(user, canRead, canEdit);

                FileSystemAccessRule rule;

                if (rights.HasValue)
                {
                    rule = new FileSystemAccessRule(
                        user,
                        rights.Value,
                        InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                        PropagationFlags.None,
                        AccessControlType.Allow
                    );
                }
                else
                {
                    rule = new FileSystemAccessRule(
                        user,
                        FileSystemRights.FullControl,
                        InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                        PropagationFlags.None,
                        AccessControlType.Deny
                    );
                }

                security.AddAccessRule(rule);
            }
            catch (Exception ex)
            {            
            }
        }

        directory.SetAccessControl(security);
    }

    private void EnsureParentFolderAccess(string parentFolderPath, IEnumerable<string> users)
    {
        var dirInfo = new DirectoryInfo(parentFolderPath);
        if (!dirInfo.Exists) return;

        var security = dirInfo.GetAccessControl();
        security.SetAccessRuleProtection(true, false);

        foreach (var user in users.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var rule = new FileSystemAccessRule(
                user,
                FileSystemRights.ReadAndExecute | FileSystemRights.ListDirectory,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None,
                AccessControlType.Allow);

            security.AddAccessRule(rule);
        }

        dirInfo.SetAccessControl(security);
    }

    private FileSystemRights? GetRights(string user, List<string> readUsers, List<string> editUsers)
    {
        if (editUsers.Contains(user, StringComparer.OrdinalIgnoreCase))
        {
            return FileSystemRights.ReadData
                   | FileSystemRights.WriteData
                   | FileSystemRights.AppendData
                   | FileSystemRights.ReadAttributes
                   | FileSystemRights.ReadExtendedAttributes
                   | FileSystemRights.WriteAttributes
                   | FileSystemRights.WriteExtendedAttributes
                   | FileSystemRights.ReadPermissions
                   | FileSystemRights.CreateDirectories
                   | FileSystemRights.CreateFiles
                   | FileSystemRights.Delete;
        }

        if (readUsers.Contains(user, StringComparer.OrdinalIgnoreCase))
        {
            return FileSystemRights.ReadAndExecute
                   | FileSystemRights.ListDirectory
                   | FileSystemRights.ReadAttributes
                   | FileSystemRights.ReadExtendedAttributes
                   | FileSystemRights.ReadPermissions;
        }

        return null;
    }
}