using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.DriveComponents.Relations;
using MoriaModels.Models.Electrical;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Orders.Relations;
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
        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Drive>()
            .HasMany(e => e.MotorGears)
            .WithMany(e => e.Drives)
            .UsingEntity<MotorGearToDrive>();
        modelBuilder.Entity<Component>()
            .HasMany(e => e.OrderItems)
            .WithMany(e => e.Components)
            .UsingEntity<ComponentToOrderItem>();
        modelBuilder.Entity<Component>()
            .HasMany(e => e.Drives)
            .WithMany(e => e.Components)
            .UsingEntity<DriveToComponent>();
        // base.OnModelCreating(modelBuilder);
    }

    public DbSet<SteelKind> SteelKinds
    {
        get; set;
    }
    public DbSet<Employee> Employees
    {
        get; set;
    }
    public DbSet<Warehouse> Warehouses
    {
        get; set;
    }
    public DbSet<Product> Products
    {
        get; set;
    }
    public DbSet<Category> Categories
    {
        get; set;
    }
    public DbSet<Color> Colors
    {
        get; set;
    }
    public DbSet<Position> Positions
    {
        get; set;
    }
    public DbSet<MotorGear> MotorGears
    {
        get; set;
    }
    public DbSet<Motor> Motors
    {
        get; set;
    }
    public DbSet<Drive> Drives
    {
        get; set;
    }
    public DbSet<Order> Orders
    {
        get; set;
    }
    public DbSet<Contact> Contacts
    {
        get; set;
    }
    public DbSet<OrderItem> OrderItems
    {
        get; set;
    }
    public DbSet<Component> Components
    {
        get; set;
    }
    public DbSet<MotorGearToDrive> MotorGearToDrives
    {
        get; set;
    }
    public DbSet<DriveToComponent> DriveToComponents
    {
        get; set;
    }
    public DbSet<ComponentToOrderItem> ComponentToOrderItems
    {
        get; set;
    }

    public DbSet<ElectricalCabinet> ElectricalCabinets
    {
        get; set;
    }

    public DbSet<Settings> Settings
    {
        get;
        set;
    }

    public DbSet<ListViewSetup> ListViewsSetup
    {
        get; set;
    }
    public DbSet<Brake> Brakes
    {
        get;
        set;
    }

    public DbSet<ExternalCooling> ExternalCoolings
    {
        get;
        set;
    }

    public DbSet<Inverter> Inverters
    {
        get;
        set;
    }

    public DbSet<Pump> Pumps
    {
        get;
        set;
    }

    public DbSet<Supplement> Supplements
    {
        get;
        set;
    }

    public DbSet<Variator> Variators
    {
        get;
        set;
    }

    public void CreatePPermissionsTrigger()
    {
        Database.ExecuteSqlRaw(CreateInsertPermissionsFunctionQuery());
        Database.ExecuteSqlRaw(CreateTriggerInsertPersmissionsFunctionQuery());
        Database.ExecuteSqlRaw(CreateInsertPermissionsTriggerQuery());
    }

    string CreateInsertPermissionsFunctionQuery()
    {
        return @$"CREATE OR REPLACE FUNCTION InsertPermissions(PositionId INT)
                RETURNS VOID AS $$
                BEGIN
                INSERT INTO ""Permission"" (""CanRead"", ""CanWrite"", ""DisplayName"", ""PropertyName"", ""PositionId"", ""IsLocked"", ""LockedBy"", ""LastModified"")
                VALUES 
                (true, true, 'Rodzaj stali - Symbol', 'SteelKind_Symbol', PositionId, false, '', 'System'),
                (true, true, 'Rodzaj stali - Nazwa', 'SteelKind_Name', PositionId, false, '', 'System'),
                (true, true, 'Kolor - Kod', 'Color_Code', PositionId, false, '', 'System'),
                (true, true, 'Kolor - Nazwa', 'Color_Name', PositionId, false, '', 'System'),
                (true, true, 'Szafa elektryczna - Symbol', 'ElectricalCabinet_Symbol', PositionId, false, '', 'System'),
                (true, true, 'Podmiot - Symbol', 'Contact_Symbol', PositionId, false, '', 'System'),
                (true, true, 'Podmiot - Nazwa krótka', 'Contact_ShortName', PositionId, false, '', 'System'),
                (true, true, 'Podmiot - Nazwa długa', 'Contact_LongName', PositionId, false, '', 'System'),
                (true, true, 'Silnik - Symbol', 'Motor_Symbol', PositionId, false, '', 'System'),
                (true, true, 'Silnik - Nazwa', 'Motor_Name', PositionId, false, '', 'System'),
                (true, true, 'Silnik - Moc', 'Motor_Power', PositionId, false, '', 'System'),
                (true, true, 'Silnik - Obroty', 'Motor_RPM', PositionId, false, '', 'System'),
                (true, true, 'Przekładnia - Symbol', 'MotorGear_Symbol', PositionId, false, '', 'System'),
                (true, true, 'Przekładnia - Nazwa', 'MotorGear_Name', PositionId, false, '', 'System'),
                (true, true, 'Przekładnia - Przełożenie', 'MotorGear_Ratio', PositionId, false, '', 'System'),
                (true, true, 'Stanowisko - Kod', 'Position_Code', PositionId, false, '', 'System'),
                (true, true, 'Stanowisko - Nazwa', 'Position_Name', PositionId, false, '', 'System'),
                (true, true, 'Magazyn - Symbol', 'Warehouse_Symbol', PositionId, false, '', 'System'),
                (true, true, 'Magazyn - Nazwa', 'Warehouse_WarehouseName', PositionId, false, '', 'System'),
                (true, true, 'Hamulce - Rodzaj', 'Brake_Kind', PositionId, false, '', 'System'),
                (true, true, 'Chłodzenie zewnętrzne - Typ', 'ExternalCooling_Type', PositionId, false, '', 'System'),
                (true, true, 'Chłodzenie zewnętrzne - Moc', 'ExternalCooling_Power', PositionId, false, '', 'System'),
                (true, true, 'Falownik - Typ', 'Inverter_Type', PositionId, false, '', 'System'),
                (true, true, 'Falownik - Moc', 'Inverter_Power', PositionId, false, '', 'System'),
                (true, true, 'Pompa - Typ', 'Pump_Type', PositionId, false, '', 'System'),
                (true, true, 'Pompa - Rozmiar', 'Pump_Size', PositionId, false, '', 'System'),
                (true, true, 'Pompa - i', 'Pump_IProperty', PositionId, false, '', 'System'),
                (true, true, 'Wyposażenie dodatkowe - Typ', 'Supplement_Type', PositionId, false, '', 'System'),
                (true, true, 'Wyposażenie dodatkowe - Rozmiar', 'Supplement_Size', PositionId, false, '', 'System'),
                (true, true, 'Wyposażenie dodatkowe - i', 'Supplement_IProperty', PositionId, false, '', 'System'),
                (true, true, 'Wariator - Typ', 'Variator_Type', PositionId, false, '', 'System'),
                (true, true, 'Napęd - Silnik', 'Drive_Motor', PositionId, false, '', 'System'),
                (true, true, 'Napęd - Nazwa', 'Drive_Name', PositionId, false, '', 'System'),
                (true, true, 'Napęd - Wariator', 'Drive_Variator', PositionId, false, '', 'System'),
                (true, true, 'Napęd - Falownik', 'Drive_Inverter', PositionId, false, '', 'System'),
                (true, true, 'Napęd - Ilość', 'Drive_Quantity', PositionId, false, '', 'System'),
                (true, true, 'Napęd - Reduktor/pompa', 'Drive_Pump', PositionId, false, '', 'System'),
                (true, true, 'Napęd - Zewn. chłodz.', 'Drive_ExternalCooling', PositionId, false, '', 'System'),
                (true, true, 'Napęd - Hamulec', 'Drive_Brake', PositionId, false, '', 'System'),
                (true, true, 'Napęd - Przystawka', 'Drive_Supplement', PositionId, false, '', 'System'),
                (true, true, 'Katergoria - Nazwa', 'Category_Name', PositionId, false, '', 'System'),
                (true, true, 'Produkt - Nazwa', 'Product_Name', PositionId, false, '', 'System'),
                (true, true, 'Produkt - Symbol', 'Product_Symbol', PositionId, false, '', 'System'),
                (true, true, 'Produkt - Maszyna główna', 'Product_IsMainMachine', PositionId, false, '', 'System'),
                (true, true, 'Produkt - Numer seryjny', 'Product_SerialNumber', PositionId, false, '', 'System'),
                (true, true, 'Produkt - Kategoria', 'Product_Category', PositionId, false, '', 'System'),
                (true, true, 'Produkt - Rodzaj stali', 'Product_SteelKind', PositionId, false, '', 'System'),
                (true, true, 'Komponent - Nazwa', 'Component_Name', PositionId, false, '', 'System'),
                (true, true, 'Komponent - Produkt', 'Component_ComponentProduct', PositionId, false, '', 'System'),
                (true, true, 'Komponent - Ilość', 'Component_Quantity', PositionId, false, '', 'System'),
                (true, true, 'Komponent - Kolor', 'Component_ComponentColor', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Symbol', 'Order_OrderNumberSymbol', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Uwagi', 'Order_Remarks', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Nr oferty', 'Order_OfferNumber', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Katalog', 'Order_CatalogLink', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Oferta sprzedaży', 'Order_SalesOfferLink', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Nr klienta', 'Order_ClientSymbol', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Zamawiający', 'Order_OrderingContact', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Odbierający', 'Order_ReceivingContact', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Rysunek techniczny', 'Order_TechnicalDrawingCompleted', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Cięcie zakończone', 'Order_CuttingCompleted', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Czyszczenie metalu', 'Order_MetalCliningCompleted', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Malowanie zakończone', 'Order_PaintingCompleted', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Szafa elektryczna gotowa', 'Order_ElectricaCabinetCompleted', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Spawanie gotowe', 'Order_WeldingCompleted', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Maszyna zmontowana', 'Order_MachineAssembled', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Maszyna okablowana i przetestowana', 'Order_MachineWiredAndTested', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Maszyna wydana', 'Order_MachineReleased', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Transport zamówiony', 'Order_TransportOrdered', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Termin realizacji', 'Order_DueDate', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Planowanie okablowania', 'Order_PlannedMachineWiredAndTestedd', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Planowanie zestawienia na hali ', 'Order_PlannedMachineAssembled', PositionId, false, '', 'System'),
                (true, true, 'Zamówienie - Planowane ladowanie', 'Order_PlannedTransport', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - L.p.', 'OrderItem_Index', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Symbol', 'OrderItem_Symbol', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Opis', 'OrderItem_Description', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Komentarz', 'OrderItem_Notes', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Waga', 'OrderItem_MachineWeight', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Moc', 'OrderItem_Power', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Rysunek techniczny', 'OrderItem_TechnicalDrawingLink', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Ilość', 'OrderItem_Quantity', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Produkt', 'OrderItem_Product', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Główny kolor', 'OrderItem_MainColor', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Detale kolor', 'OrderItem_DetailsColor', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Komponent', 'OrderItem_Component', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Napęd', 'OrderItem_Drive', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Magazyn', 'OrderItem_Warehouse', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Projektant', 'OrderItem_Designer', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Rysunek techniczny ukończony', 'OrderItem_TechnicalDrawingCompleted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Rysunek techniczny planowany', 'OrderItem_TechnicalDrawingPlanned', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Rysunek techniczny rozpoczety', 'OrderItem_TechnicalDrawingStarted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Cięcie zakończone', 'OrderItem_CuttingCompleted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Cięcie planowane', 'OrderItem_CuttingPlanned', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Cięcie rozpoczęte', 'OrderItem_CuttingStarted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Spawanie zakończone', 'OrderItem_WeldingCompleted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Spawanie planowane', 'OrderItem_WeldingPlanned', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Spawanie rozpoczęte', 'OrderItem_WeldingStarted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Czyszczenie metalu ukończone', 'OrderItem_MetalCliningCompleted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Czyszczenie metalu planowane', 'OrderItem_MetalCliningPlanned', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Czyszczenie metalu start', 'OrderItem_MetalCliningStarted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Malowanie zakończone', 'OrderItem_PaintingCompleted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Malowanie planowane', 'OrderItem_PaintingPlanned', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Malowanie start', 'OrderItem_PaintingStarted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Szafa elektryczna gotowa', 'OrderItem_ElectricaCabinetCompleted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Szafa elektryczna planowana', 'OrderItem_ElectricaCabinetPlanned', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Szafa elektryczna start', 'OrderItem_ElectricaCabinetStarted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Maszyna zmontowana', 'OrderItem_MachineAssembled', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Maszyna zmontowana planowana ', 'OrderItem_MachineAssembledPlanned', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Maszyna zmontowana start ', 'OrderItem_MachineAssembledStarted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Maszyna okablowana i przetestowana', 'OrderItem_MachineWiredAndTested', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Maszyna okablowana i przetestowana planowo', 'OrderItem_MachineWiredAndTestedPlanned', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Maszyna okablowana i przetestowana start', 'OrderItem_MachineWiredAndTestedStarted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Maszyna wydana', 'OrderItem_MachineReleased', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Maszyna wydana plan', 'OrderItem_MachineReleasedPlanned', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Maszyna wydana start, 'OrderItem_MachineReleasedStarted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Schemat elektryczny ukończony, 'OrderItem_ElectricalDiagramCompleted', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Schemat elektryczny planowany, 'OrderItem_ElectricalDiagramPlanned', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Schemat elektryczny planowany, 'OrderItem_ElectricalDiagramStarted, PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Transport zamówiony', 'OrderItem_TransportOrdered', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Termin realizacji', 'OrderItem_DueDate', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Rok produkcji', 'OrderItem_ProductionYear', PositionId, false, '', 'System'),
                (true, true, 'Pozycja zamówienia - Nr seryjny', 'OrderItem_SerialNumber', PositionId, false, '', 'System'),
	            (true, true, 'Pracownik - Imię', 'Employee_FirstName', PositionId, false, '', 'System'),
	            (true, true, 'Pracownik - Nazwisko', 'Employee_LastName', PositionId, false, '', 'System'),
	            (true, true, 'Pracownik - Nazwa użytkownika', 'Employee_Username', PositionId, false, '', 'System'),
	            (true, true, 'Pracownik - Numer telefonu', 'Employee_PhoneNumber', PositionId, false, '', 'System'),
	            (true, true, 'Pracownik - Stanowisko', 'Employee_Position', PositionId, false, '', 'System'),
	            (true, true, 'Pracownik - Admin', 'Employee_Admin', PositionId, false, '', 'System');
                END;
                $$ LANGUAGE plpgsql;";
    }

    string CreateTriggerInsertPersmissionsFunctionQuery()
    {
        return $@"CREATE OR REPLACE FUNCTION trigger_InsertPermissions()
                RETURNS TRIGGER AS $$
                BEGIN
                PERFORM InsertPermissions(NEW.""Id"");
                RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;";
    }

    string CreateInsertPermissionsTriggerQuery()
    {
        return $@"CREATE TRIGGER after_insert_positions
                AFTER INSERT ON ""Positions""
                FOR EACH ROW
                EXECUTE FUNCTION trigger_InsertPermissions();";
    }
}
