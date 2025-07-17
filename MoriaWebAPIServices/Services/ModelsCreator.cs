using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MoriaBaseModels.Models;
using MoriaDTObjects.Models;
using MoriaDTObjects.Models.Interfaces;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.DriveComponents.Relations;
using MoriaModels.Models.Electrical;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Interfaces;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Orders.Relations;
using MoriaModels.Models.Products;
using MoriaModels.Models.Warehouses;
using MoriaModelsDo.Base;
using MoriaModelsDo.Base.Enums;
using MoriaModelsDo.Models.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents.Relations;
using MoriaModelsDo.Models.Orders;
using MoriaModelsDo.Models.Orders.Relations;
using MoriaModelsDo.Models.Products;
using MoriaWebAPIServices.Contexts;
using Component = MoriaModels.Models.DriveComponents.Component;

namespace MoriaWebAPIServices.Services;
public class ModelsCreator
{
    readonly ApplicationDbContext _context;

    public ModelsCreator(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Generic

    public async Task<T1> GetModelInContext<T1, T2>(Func<T2, Task<T1>> creator, T2 dO, DbSet<T1> set) where T1 : BaseModel, new() where T2 : BaseDo
    {
        if (dO == null)
            return null;

        var model = await set.FindAsync(dO.Id);
        if (model == null)
        {
            model = await creator(dO);
            await _context.AddAsync(model);
        }

        return model;
    }

    public async Task<T1> GetSubiektModelInContext<T1, T2>(Func<T2, Task<T1>> creator, Func<T2, T1, Task> updater, T2 subiektObject, DbSet<T1> set) where T1 : BaseModel, ISubiektModel, new() where T2 : ISubiektBaseObject
    {
        if (subiektObject == null)
            return null;

        var model = set.FirstOrDefault(x => x.SubiektId == subiektObject.Id);
        if (model == null)
            model = set.Local.FirstOrDefault(x => x.SubiektId == subiektObject.Id);
        if (model == null)
        {
            model = await creator(subiektObject);
            set.Add(model);
            await _context.AddAsync(model);
        }
        else
        {
            await updater(subiektObject, model);
        }

        return model;
    }

    #endregion

    #region Employee

    public EmployeeDo GetEmployeeDo(Employee employee)
    {
        return new EmployeeDo()
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            IsLocked = employee.IsLocked,
            LastModified = employee.LastModified,
            LastName = employee.LastName,
            PhoneNumber = employee.PhoneNumber,
            Username = employee.Username,
            Admin = employee.Admin,
            Position = employee.Position != null ?
            GetPosition(employee.Position) : null,
        };
    }

    public async Task<Employee> CreateEmployee(EmployeeDo employee)
    {
        var result = new Employee()
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Username = employee.Username,
            Password = employee.Password,
            PhoneNumber = employee.PhoneNumber,
            LastModified = employee.LastModified,
            Admin = employee.Admin,
        };

        result.Position = await GetModelInContext(CreatePosition, employee.Position, _context.Positions);

        return result;
    }

    public async Task UpdateEmployee(Employee employee, EmployeeDo employeeModel)
    {
        employee.FirstName = employeeModel.FirstName;
        employee.LastName = employeeModel.LastName;
        employee.Username = employeeModel.Username;
        if (employeeModel.Password != null)
            employee.Password = employeeModel.Password;
        employee.PhoneNumber = employeeModel.PhoneNumber;
        employee.Admin = employeeModel.Admin;
        employee.LastModified = employeeModel.LastModified;
        employee.IsLocked = false;
        employee.LockedBy = string.Empty;

        employee.Position = await GetModelInContext(CreatePosition, employeeModel.Position, _context.Positions);
    }

    #endregion

    #region Position

    public PositionDo GetPosition(Position position)
    {
        return new()
        {
            Id = position.Id,
            Code = position.Code,
            Name = position.Name,
            LastModified = position.LastModified,
            Permissions = position.Permissions?.Any() == true ? position.Permissions.Select(GetPermissionDo).ToList() : new()
        };
    }

    public PermissionDo GetPermissionDo(Permission permission)
    {
        return new()
        {
            Id = permission.Id,
            CanRead = permission.CanRead,
            CanWrite = permission.CanWrite,
            DisplayName = permission.DisplayName,
            PropertyName = permission.PropertyName,
        };
    }

    public async Task<Position> CreatePosition(PositionDo position)
    {
        return new()
        {
            Code = position.Code,
            Name = position.Name,
            LastModified = position.LastModified
        };
    }

    public async Task UpdatePosition(Position position, PositionDo positionModel)
    {
        position.Code = positionModel.Code;
        position.Name = positionModel.Name;
        position.LastModified = positionModel.LastModified;

        foreach (var permission in positionModel.Permissions.Where(x => x.ChangeType == SystemChangeType.Modified))
        {
            var contextPermission = position.Permissions.FirstOrDefault(x => x.Id == permission.Id);
            if (contextPermission != null)
            {
                contextPermission.CanWrite = permission.CanWrite;
                contextPermission.CanRead = permission.CanRead;
            }
        }
    }

    #endregion

    #region SteelKind

    public SteelKindDo GetSteelKindDo(SteelKind steelKind)
        => new()
        {
            Id = steelKind.Id,
            Name = steelKind.Name,
            Symbol = steelKind.Symbol,
            LastModified = steelKind.LastModified,
        };

    public async Task<SteelKind> CreateSteelKind(SteelKindDo steelKind)
    {
        return new()
        {
            Name = steelKind.Name,
            LastModified = steelKind.LastModified,
            Symbol = steelKind.Symbol
        };
    }

    public async Task UpdateSteelKind(SteelKind steelKind, SteelKindDo steelKindModel)
    {
        steelKind.Name = steelKindModel.Name;
        steelKind.Symbol = steelKindModel.Symbol;
        steelKind.LastModified = steelKindModel.LastModified;
        steelKind.IsLocked = false;
        steelKind.LockedBy = string.Empty;
    }

    #endregion

    #region Color

    public ColorDo GetColorDo(MoriaModels.Models.Products.Color color)
    {
        return new ColorDo()
        {
            Id = color.Id,
            Name = color.Name,
            Code = color.Code,
            LastModified = color.LastModified,
        };
    }

    public async Task<MoriaModels.Models.Products.Color> CreateColor(ColorDo colorDo)
    {
        return new()
        {
            Name = colorDo.Name,
            Code = colorDo.Code,
            LastModified = colorDo.LastModified,
        };
    }

    public async Task UpdateColor(MoriaModels.Models.Products.Color color, ColorDo colorModel)
    {
        color.Name = colorModel.Name;
        color.Code = colorModel.Code;
        color.LastModified = colorModel.LastModified;
        color.IsLocked = false;
        color.LockedBy = string.Empty;
    }

    #endregion

    #region Contact

    public ContactDo GetContact(Contact contact)
    {
        return new ContactDo()
        {
            Id = contact.Id,
            LongName = contact.LongName,
            ShortName = contact.ShortName,
            Symbol = contact.Symbol,
            LastModified = contact.LastModified,
        };
    }

    public async Task<Contact> CreateContact(ContactDo contactDo)
    {
        return new()
        {
            Symbol = contactDo.Symbol,
            LongName = contactDo.LongName,
            ShortName = contactDo.ShortName,
            LastModified = contactDo.LastModified
        };
    }

    public async Task<Contact> CreateContact(MoriaEntity model)
    {
        return new()
        {
            Symbol = model.Symbol,
            LongName = model.LongName,
            ShortName = model.ShortName,
            SubiektId = model.Id,
        };
    }

    public async Task UpdateContact(MoriaEntity model, Contact entity)
    {
        entity.Symbol = model.Symbol;
        entity.LongName = model.LongName;
        entity.ShortName = model.ShortName;
    }

    public async Task UpdateContact(Contact contact, ContactDo contactModel)
    {
        contact.Symbol = contactModel.Symbol;
        contact.LongName = contactModel.LongName;
        contact.ShortName = contactModel.ShortName;
        contact.LastModified = contactModel.LastModified;
        contact.IsLocked = false;
        contact.LockedBy = string.Empty;
    }

    #endregion

    #region Motor

    public MotorDo GetMotor(Motor motor)
    {
        return new MotorDo()
        {
            Id = motor.Id,
            Name = motor.Name,
            Power = motor.Power,
            Symbol = motor.Symbol,
            LastModified = motor.LastModified
        };
    }

    public async Task<Motor> CreateMotor(MotorDo motorDo)
    {
        return new()
        {
            Symbol = motorDo.Symbol,
            Power = motorDo.Power,
            Name = motorDo.Name,
            LastModified = motorDo.LastModified
        };
    }

    public async Task UpdateMotor(Motor motor, MotorDo motorModel)
    {
        motor.Symbol = motorModel.Symbol;
        motor.Power = motorModel.Power;
        motor.Name = motorModel.Name;
        motor.LastModified = motorModel.LastModified;
        motor.IsLocked = false;
        motor.LockedBy = string.Empty;
    }
    #endregion

    #region MotorGear

    public MotorGearDo GetMotorGear(MotorGear motorGear)
    {
        return new MotorGearDo()
        {
            Id = motorGear.Id,
            Name = motorGear.Name,
            Ratio = motorGear.Ratio,
            Symbol = motorGear.Symbol,
            LastModified = motorGear.LastModified
        };
    }

    public async Task<MotorGear> CreateMotorGear(MotorGearDo motorGearDo)
    {
        return new()
        {
            Symbol = motorGearDo.Symbol,
            Name = motorGearDo.Name,
            Ratio = motorGearDo.Ratio,
            LastModified = motorGearDo.LastModified,
        };
    }

    public async Task UpdateMotorGear(MotorGear motorGear, MotorGearDo motorGearModel)
    {
        motorGear.Symbol = motorGearModel.Symbol;
        motorGear.Ratio = motorGearModel.Ratio;
        motorGear.Name = motorGearModel.Name;
        motorGear.LastModified = motorGearModel.LastModified;
        motorGear.IsLocked = false;
        motorGear.LockedBy = string.Empty;
    }

    #endregion

    #region Warehouse
    public WarehouseDo GetWarehouse(Warehouse warehouse)
    {
        return new WarehouseDo()
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            Symbol = warehouse.Symbol,
            LastModified = warehouse.LastModified,
        };
    }

    public async Task<Warehouse> CreateWarehouse(WarehouseDo warehouseDo)
    {
        return new()
        {
            Name = warehouseDo.Name,
            Symbol = warehouseDo.Symbol,
            LastModified = warehouseDo.LastModified
        };
    }

    public async Task<Warehouse> CreateWarehouse(MoriaWarehouse model)
    {
        return new()
        {
            SubiektId = model.Id,
            Name = model.Name,
            Symbol = model.Symbol
        };
    }

    public async Task UpdateWarehouse(MoriaWarehouse model, Warehouse entity)
    {
        entity.Name = model.Name;
        entity.Symbol = model.Symbol;
    }

    public async Task UpdateWarehouse(Warehouse warehouse, WarehouseDo warehouseModel)
    {
        warehouse.Symbol = warehouseModel.Symbol;
        warehouse.Name = warehouseModel.Name;
        warehouse.LastModified = warehouseModel.LastModified;
        warehouse.IsLocked = false;
        warehouse.LockedBy = string.Empty;
    }
    #endregion

    #region Category

    public CategoryDo GetCategoryDo(Category category)
        => new()
        {
            Id = category.Id,
            Name = category.Name,
            LastModified = category.LastModified,
            Products = category != null && category.Products.Any() ? category.Products.Select(GetNestedProductDo) : null,
        };

    public async Task<Category> CreateCategory(CategoryDo category)
    {
        return new()
        {
            Name = category.Name,
            LastModified = category.LastModified
        };
    }

    public async Task UpdateCategory(Category category, CategoryDo categoryModel)
    {
        category.Name = categoryModel.Name;
        category.LastModified = categoryModel.LastModified;
        category.IsLocked = false;
        category.LockedBy = string.Empty;
    }

    #endregion

    #region Component

    public ComponentDo GetComponentDo(Component component)
    {
        var result = new ComponentDo()
        {
            Id = component.Id,
            ComponentProduct = component.ComponentProduct != null ? GetNestedProductDo(component.ComponentProduct) : null,
            ComponentColor = component.ComponentColor != null ? GetColorDo(component.ComponentColor) : null,
            LastModified = component.LastModified,
            Name = component.Name,
            Quantity = component.Quantity,
            Drives = component.DriveToComponents != null && component.DriveToComponents.Any() ? GetDriveToComponents(component.DriveToComponents) : null
        };

        return result;
    }

    public async Task<Component> CreateComponent(ComponentDo component)
    {
        var result = new Component()
        {
            LastModified = component.LastModified,
            ComponentProduct = await GetModelInContext(CreateProduct, component.ComponentProduct, _context.Products),
            ComponentColor = await GetModelInContext(CreateColor, component.ComponentColor, _context.Colors),
            Name = component.Name,
            Quantity = component.Quantity,
        };

        foreach (var dtc in component.Drives.Where(x => x.ChangeType != SystemChangeType.Deleted))
            result.DriveToComponents.Add(await GetModelInContext(CreateDriveToComponent, dtc, _context.DriveToComponents));

        return result;
    }

    public async Task UpdateComponent(Component component, ComponentDo componentModel)
    {
        component.ComponentProduct = await GetModelInContext(CreateProduct, componentModel.ComponentProduct, _context.Products);
        component.ComponentColor = await GetModelInContext(CreateColor, componentModel.ComponentColor, _context.Colors);
        component.LastModified = componentModel.LastModified;
        component.Name = componentModel.Name;
        component.Quantity = componentModel.Quantity;
        component.IsLocked = false;
        component.LockedBy = string.Empty;

        if (componentModel?.Drives?.Any() == true)
            foreach (var dtc in componentModel.Drives.Where(x => x.ChangeType != SystemChangeType.None))
            {
                switch (dtc.ChangeType)
                {
                    case SystemChangeType.Added:
                        component.DriveToComponents.Add(await GetModelInContext(CreateDriveToComponent, dtc, _context.DriveToComponents));
                        break;
                    case SystemChangeType.Modified:
                        var contextDtc = await _context.DriveToComponents.FirstOrDefaultAsync(x => x.Id == dtc.Id);
                        if (contextDtc != null)
                            await UpdateDriveToComponent(contextDtc, dtc);
                        break;
                    case SystemChangeType.Deleted:
                        var searchDtc = await _context.DriveToComponents.FirstOrDefaultAsync(x => x.Id == dtc.Id);
                        if (searchDtc != null)
                            _context.DriveToComponents.Remove(searchDtc);
                        break;
                }
            }
    }

    #endregion

    #region Product

    public ProductDo GetProductDo(Product product)
    {
        return new()
        {
            Id = product.Id,
            IsMainMachine = product.IsMainMachine,
            Name = product.Name,
            SerialNumber = product.SerialNumber,
            Symbol = product.Symbol,
            SteelKind = product.SteelKind != null ? GetSteelKindDo(product.SteelKind) : null,
            Category = product.Category != null ? GetCategoryDo(product.Category) : null,
            Components = product.Components != null && product.Components.Any() ? product.Components.Select(GetComponentDo).ToList() : null,
            LastModified = product.LastModified,
        };
    }

    public ProductDo GetNestedProductDo(Product product)
    {
        return new()
        {
            Id = product.Id,
            IsMainMachine = product.IsMainMachine,
            Name = product.Name,
            SerialNumber = product.SerialNumber,
            Symbol = product.Symbol,
        };
    }

    public async Task<Product> CreateProduct(ProductDo product)
    {
        var result = new Product()
        {
            IsMainMachine = product.IsMainMachine,
            Name = product.Name,
            SerialNumber = product.SerialNumber,
            Symbol = product.Symbol,
            LastModified = product.LastModified,
        };

        result.SteelKind = await GetModelInContext(CreateSteelKind, product.SteelKind, _context.SteelKinds);
        result.Category = await GetModelInContext(CreateCategory, product.Category, _context.Categories);

        foreach (var component in product.Components.Where(x => x.ChangeType == SystemChangeType.Added))
            result.Components.Add(await GetModelInContext(CreateComponent, component, _context.Components));

        return result;
    }

    public async Task<Product> CreateProduct(MoriaProduct model)
    {
        return new Product()
        {
            SubiektId = model.Id,
            Name = model.Name,
            SerialNumber = model.SerialNumber,
            Symbol = model.Symbol
        };
    }

    public async Task UpdateProduct(MoriaProduct model, Product entity)
    {
        entity.Name = model.Name;
        entity.SerialNumber = model.SerialNumber;
        entity.Symbol = model.Symbol;
    }

    public async Task UpdateProduct(Product product, ProductDo productModel)
    {
        product.IsMainMachine = productModel.IsMainMachine;
        product.Name = productModel.Name;
        product.SerialNumber = productModel.SerialNumber;
        product.Symbol = productModel.Symbol;
        product.LastModified = productModel.LastModified;
        product.IsLocked = false;
        product.LockedBy = string.Empty;

        product.SteelKind = await GetModelInContext(CreateSteelKind, productModel.SteelKind, _context.SteelKinds);
        product.Category = await GetModelInContext(CreateCategory, productModel.Category, _context.Categories);

        foreach (var component in productModel.Components.Where(x => x.ChangeType != SystemChangeType.None))
        {
            switch (component.ChangeType)
            {
                case SystemChangeType.Added:
                    product.Components.Add(await GetModelInContext(CreateComponent, component, _context.Components));
                    break;
                case SystemChangeType.Modified:
                    var contextComponent = await _context.Components.Include(x => x.ComponentProduct).FirstOrDefaultAsync(x => x.Id == component.Id);
                    if (contextComponent != null)
                        await UpdateComponent(contextComponent, component);
                    break;
                case SystemChangeType.Deleted:
                    var searchComponent = await _context.Components.FindAsync(component.Id);
                    if (searchComponent != null)
                        _context.Components.Remove(searchComponent);
                    break;
                default:
                    break;
            }
        }
    }

    #endregion

    #region Drive

    public DriveDo GetDriveDo(Drive drive, bool getGearboxes = true)
    {
        return new()
        {
            Id = drive.Id,
            //Inverter = drive.Inverter,
            //Variator = drive.Variator, #TODO
            Quantity = drive.Quantity,
            Name = drive.Name,
            Motor = drive.Motor != null ? GetMotor(drive.Motor) : null,
            Gearboxes = getGearboxes && drive.MotorGearToDrives != null && drive.MotorGearToDrives.Any() ? GetDoMotorGearBoxToDrives(drive.MotorGearToDrives) : null,
            LastModified = drive.LastModified,
        };
    }

    public async Task<Drive> CreateDrive(DriveDo drive)
    {
        var result = new Drive()
        {
            //Inverter = drive.Inverter,
            Quantity = drive.Quantity,
            //Variator = drive.Variator, #TODO
            Name = drive.Name,
            LastModified = drive.LastModified,
        };

        result.Motor = await GetModelInContext(CreateMotor, drive.Motor, _context.Motors);

        foreach (var mgtd in drive.Gearboxes.Where(x => x.ChangeType == SystemChangeType.Added))
            result.MotorGearToDrives.Add(await GetModelInContext(CreateMotorGearToDrive, mgtd, _context.MotorGearToDrives));

        return result;
    }

    public async Task UpdateDrive(Drive drive, DriveDo driveModel)
    {
    //    drive.Inverter = driveModel.Inverter;
    //    drive.Variator = driveModel.Variator;
        drive.Quantity = driveModel.Quantity;
        drive.Name = driveModel.Name;
        drive.LastModified = driveModel.LastModified;
        drive.IsLocked = false;
        drive.LockedBy = string.Empty;

        drive.Motor = await GetModelInContext(CreateMotor, driveModel.Motor, _context.Motors);
        //product.Category = await GetModelInContext(CreateCategory, productModel.Category, _context.Categories);

        foreach (var motorgear in driveModel.Gearboxes.Where(x => x.ChangeType != SystemChangeType.None))
        {
            motorgear.Drive = driveModel;
            switch (motorgear.ChangeType)
            {
                case SystemChangeType.Added:
                    drive.MotorGearToDrives.Add(await GetModelInContext(CreateMotorGearToDrive, motorgear, _context.MotorGearToDrives));
                    break;
                case SystemChangeType.Modified:
                    var contextMotorGearToDrive = await _context.MotorGearToDrives.Include(x => x.MotorGear).FirstOrDefaultAsync(x => x.Id == motorgear.Id);
                    if (contextMotorGearToDrive != null)
                        await UpdateMotorGearToDrive(contextMotorGearToDrive, motorgear);
                    break;
                case SystemChangeType.Deleted:
                    var searchMotorGearToDrive = await _context.MotorGearToDrives.FindAsync(motorgear.Id);
                    if (searchMotorGearToDrive != null)
                        _context.MotorGearToDrives.Remove(searchMotorGearToDrive);
                    break;
                default:
                    break;
            }
        }
    }

    #endregion

    #region Order

    public OrderDo GetOrderDo(Order order)
    {
        var result = new OrderDo()
        {
            Id = order.Id,
            LastModified = order.LastModified,
            OrderNumberSymbol = order.OrderNumberSymbol,
            Remarks = order.Remarks,
            OfferNumber = order.OfferNumber,
            CatalogLink = order.CatalogLink,
            ClientSymbol = order.ClientSymbol,
            OrderingContact = order.OrderingContact == null ? null : GetContact(order.OrderingContact),
            ReceivingContact = order.ReceivingContact == null ? null : GetContact(order.ReceivingContact),
            OrderState = order.OrderState,
            SalesOfferLink = order.SalesOfferLink,

        };

        if (order.OrderItems != null)
            foreach (var oi in order.OrderItems.OrderBy(x => x.DueDate))
                result.OrderItems.Add(GetOrderItemDo(oi));

        result.ElectricaCabinetCompleted = !result.OrderItems.Any(x => x.ElectricaCabinetCompleted == null);
        result.TechnicalDrawingCompleted = !result.OrderItems.Any(x => x.TechnicalDrawingCompleted == null);
        result.WeldingCompleted = !result.OrderItems.Any(x => x.WeldingCompleted == null);
        result.CuttingCompleted = !result.OrderItems.Any(x => x.CuttingCompleted == null);
        result.MetalCliningCompleted = !result.OrderItems.Any(x => x.MetalCliningCompleted == null);
        result.PaintingCompleted = !result.OrderItems.Any(x => x.PaintingCompleted == null);
        result.MachineAssembled = !result.OrderItems.Any(x => x.MachineAssembled == null);
        result.MachineWiredAndTested = !result.OrderItems.Any(x => x.MachineWiredAndTested == null);
        result.MachineReleased = !result.OrderItems.Any(x => x.MachineReleased == null);
        result.TransportOrdered = !result.OrderItems.Any(x => x.TransportOrdered == null);


        return result;
    }

    public async Task<Order> CreateOrder(OrderDo model)
    {
        var result = new Order()
        {
            CatalogLink = model.CatalogLink,
            ClientSymbol = model.ClientSymbol,
            LastModified = model.LastModified,
            Remarks = model.Remarks,
            OrderingContact = model.OrderingContact != null ? await GetModelInContext(CreateContact, model.OrderingContact, _context.Contacts) : null,
            ReceivingContact = model.ReceivingContact != null ? await GetModelInContext(CreateContact, model.ReceivingContact, _context.Contacts) : null,
            OrderNumberSymbol = model.OrderNumberSymbol,
            DueDate = model.DueDate,
            SalesOfferLink = model.SalesOfferLink,
        };

        if (model.OrderItems != null)
        {
            foreach (var oi in model.OrderItems.Where(x => x.ChangeType == SystemChangeType.Added))
                result.OrderItems.Add(await CreateOrderItem(oi));
        }

        return result;
    }

    public async Task<Order> CreateOrder(MoriaSalesOrder model)
    {
        var result = new Order()
        {
            SubiektId = model.Id,
            OrderNumberSymbol = model.Symbol,
            Remarks = model.Remarks,
            OfferNumber = model.OfferNumber,
            ClientSymbol = model.ClientNumber,
            OrderingContact = await GetSubiektModelInContext(CreateContact, UpdateContact, model.Client, _context.Contacts),
            ReceivingContact = await GetSubiektModelInContext(CreateContact, UpdateContact, model.Recipient, _context.Contacts),
            OrderDate = model.DocumentDate,
            DueDate = model.DueDate,
        };

        if (model.SalesOrderItems != null && model.SalesOrderItems.Any())
        {
            result.OrderItems = new List<OrderItem>();
            var warehouse = await GetSubiektModelInContext(CreateWarehouse, UpdateWarehouse, model.Warehouse, _context.Warehouses);

            var splitSymbol = model.Symbol?.Split(' ');
            string moriaSymbol = null;
            if (splitSymbol.Count() > 2)
                moriaSymbol = splitSymbol[1] + splitSymbol[2];
            foreach (var soi in model.SalesOrderItems)
            {
                var oi = await CreateOrderItem(soi, moriaSymbol);
                oi.Warehouse = warehouse;
                result.OrderItems.Add(oi);
            }
        }

        return result;
    }

    public async Task UpdateOrder(MoriaSalesOrder model, Order toUpdate)
    {
        toUpdate.SubiektId = model.Id;
        toUpdate.OrderNumberSymbol = model.Symbol;
        toUpdate.Remarks = model.Remarks;
        toUpdate.OfferNumber = model.OfferNumber;
        toUpdate.ClientSymbol = model.ClientNumber;
        toUpdate.OrderingContact = await GetSubiektModelInContext(CreateContact, UpdateContact, model.Client, _context.Contacts);
        toUpdate.ReceivingContact = await GetSubiektModelInContext(CreateContact, UpdateContact, model.Recipient, _context.Contacts);
        toUpdate.OrderDate = model.DocumentDate;
        toUpdate.DueDate = model.DueDate;

        if (model.SalesOrderItems != null && model.SalesOrderItems.Any())
        {
            foreach (var msoi in model.SalesOrderItems)
            {
                toUpdate.OrderItems ??= new List<OrderItem>();

                var warehouse = await GetSubiektModelInContext(CreateWarehouse, UpdateWarehouse, model.Warehouse, _context.Warehouses);
                var splitSymbol = model.Symbol?.Split(' ');
                string moriaSymbol = null;
                if (splitSymbol.Count() > 2)
                    moriaSymbol = splitSymbol[1] + splitSymbol[2];

                var oi = toUpdate.OrderItems.FirstOrDefault(x => x.SubiektId == msoi.Id);
                if (oi == null)
                {
                    oi = await CreateOrderItem(msoi, moriaSymbol);
                    oi.Warehouse = warehouse;
                    toUpdate.OrderItems.Add(oi);
                }
                else
                {
                    await UpdateOrderItem(msoi, moriaSymbol, oi);
                }
            }

            foreach (var oi in toUpdate.OrderItems)
            {
                if (!model.SalesOrderItems.Any(x => x.Id == oi.SubiektId))
                    _context.OrderItems.Remove(oi);
            }
        }
    }


    public async Task UpdateOrder(Order order, OrderDo model)
    {
        order.ClientSymbol = model.ClientSymbol;
        order.Remarks = model.Remarks;
        order.OrderNumberSymbol = model.OrderNumberSymbol;
        order.LastModified = model.LastModified;
        order.CatalogLink = model.CatalogLink;
        order.SalesOfferLink = model.SalesOfferLink;
        order.DueDate = model.DueDate;
        order.IsLocked = false;
        order.LockedBy = string.Empty;

        order.OrderingContact = await GetModelInContext(CreateContact, model.OrderingContact, _context.Contacts);
        order.ReceivingContact = await GetModelInContext(CreateContact, model.ReceivingContact, _context.Contacts);

        foreach (var oi in model.OrderItems.Where(x => x.ChangeType != SystemChangeType.None))
        {
            //motorgear.Drive = model;
            switch (oi.ChangeType)
            {
                case SystemChangeType.Added:
                    order.OrderItems.Add(await CreateOrderItem(oi));
                    break;
                case SystemChangeType.Modified:
                    var contextOrderItem = await _context.OrderItems.Include(x => x.Component).Include(x => x.Product)
                        .Include(x => x.Drive).Include(x => x.Designer).Include(x => x.Warehouse)
                        .Include(x => x.ComponentToOrderItems)
                            .ThenInclude(x => x.Component)
                        .Include(x => x.ComponentToOrderItems)
                            .ThenInclude(x => x.Color)
                        .Include(x => x.ComponentToOrderItems)
                            .ThenInclude(x => x.Drive)
                        .FirstOrDefaultAsync(x => x.Id == oi.Id);
                    if (contextOrderItem != null)
                        await UpdateOrderItem(contextOrderItem, oi);
                    break;
                case SystemChangeType.Deleted:
                    var searchOrderItem = await _context.OrderItems.FindAsync(oi.Id);
                    if (searchOrderItem != null)
                        _context.OrderItems.Remove(searchOrderItem);
                    break;
                default:
                    break;
            }
        }
        var change = false;
        if (!order.OrderItems.Any(x => x.MachineWiredAndTested == null))
        {
            order.OrderState = SystemOrderState.MachineWiredAndTested;
            change = true;
        }
        if (!order.OrderItems.Any(x => x.MachineReleased == null))
        {
            order.OrderState = SystemOrderState.MachineReleased;
            change = true;
        }
        if (!change)
            order.OrderState = SystemOrderState.None;
    }

    #endregion

    #region OrderItem

    public OrderItemDo GetOrderItemDo(OrderItem oi)
    {
        var result = new OrderItemDo()
        {
            Id = oi.Id,
            Index = oi.Index,
            Description = oi.Description,
            Notes = oi.Notes,
            MachineWeight = oi.MachineWeight,
            TechnicalDrawingLink = oi.TechnicalDrawingLink,
            Quantity = oi.Quantity,
            LastModified = oi.LastModified,
            Product = oi.Product != null ? GetNestedProductDo(oi.Product) : null,
            Component = oi.Component != null ? GetComponentDo(oi.Component) : null,
            Drive = oi.Drive != null ? GetDriveDo(oi.Drive, false) : null,
            Designer = oi.Designer != null ? GetEmployeeDo(oi.Designer) : null,
            Warehouse = oi.Warehouse != null ? GetWarehouse(oi.Warehouse) : null,
            MainColor = oi.MainColor != null ? GetColorDo(oi.MainColor) : null,
            DetailsColor = oi.DetailsColor != null ? GetColorDo(oi.DetailsColor) : null,
            Power = oi.Power,
            ElectricaCabinetCompleted = oi.ElectricaCabinetCompleted,
            TechnicalDrawingCompleted = oi.TechnicalDrawingCompleted,
            CuttingCompleted = oi.CuttingCompleted,
            WeldingCompleted = oi.WeldingCompleted,
            MetalCliningCompleted = oi.MetalCliningCompleted,
            PaintingCompleted = oi.PaintingCompleted,
            ElectricialDescription = oi.ElectricalDescription,
            MachineAssembled = oi.MachineAssembled,
            MachineWiredAndTested = oi.MachineWiredAndTested,
            MachineReleased = oi.MachineReleased,
            TransportOrdered = oi.TransportOrdered,
            PlannedMachineAssembled = oi.PlannedMachineAssembled,
            PlannedMachineWiredAndTested = oi.PlannedMachineWiredAndTested,
            PlannedTransport = oi.PlannedTransport,
            DueDate = oi.DueDate,
            Symbol = oi.Symbol,
            ElectricalCabinet = oi.ElectricalCabinet != null ? GetElectricalCabinet(oi.ElectricalCabinet) : null,
            Electrician = oi.Electrician != null ? GetEmployeeDo(oi.Electrician) : null,
            ControlCabinetWorkStartDate = oi.ControlCabinetWorkStartDate,
            ElectricalDiagramCompleted = oi.ElectricalDiagramCompleted,
            ProductionYear = oi.ProductionYear,
            SerialNumber = oi.SerialNumber,
            Order = oi.Order == null ? null : new OrderDo()
            {
                OrderNumberSymbol = oi.Order.OrderNumberSymbol
            }
        };

        if (oi.ComponentToOrderItems != null)
            foreach (var ctoi in oi.ComponentToOrderItems)
                result.ComponentsToOrderItem.Add(GetComponentToOrderItemDo(ctoi));

        return result;
    }

    public async Task<OrderItem> CreateOrderItem(OrderItemDo model)
    {
        var result = new OrderItem()
        {
            Description = model.Description,
            Index = model.Index,
            MachineWeight = model.MachineWeight,
            Notes = model.Notes,
            LastModified = model.LastModified,
            Component = model.Component != null ? await GetModelInContext(CreateComponent, model.Component, _context.Components) : null,
            Product = model.Product != null ? await GetModelInContext(CreateProduct, model.Product, _context.Products) : null,
            Designer = model.Designer != null ? await GetModelInContext(CreateEmployee, model.Designer, _context.Employees) : null,
            Drive = model.Drive != null ? await GetModelInContext(CreateDrive, model.Drive, _context.Drives) : null,
            Quantity = model.Quantity,
            Warehouse = model.Warehouse != null ? await GetModelInContext(CreateWarehouse, model.Warehouse, _context.Warehouses) : null,
            MainColor = model.MainColor != null ? await GetModelInContext(CreateColor, model.MainColor, _context.Colors) : null,
            DetailsColor = model.DetailsColor != null ? await GetModelInContext(CreateColor, model.DetailsColor, _context.Colors) : null,
            Power = model.Power,
            TechnicalDrawingLink = model.TechnicalDrawingLink,
            ElectricaCabinetCompleted = model.ElectricaCabinetCompleted,
            TechnicalDrawingCompleted = model.TechnicalDrawingCompleted,
            WeldingCompleted = model.WeldingCompleted,
            CuttingCompleted = model.CuttingCompleted,
            MetalCliningCompleted = model.MetalCliningCompleted,
            PaintingCompleted = model.PaintingCompleted,
            ElectricalDescription = model.ElectricialDescription,
            MachineAssembled = model.MachineAssembled,
            MachineWiredAndTested = model.MachineWiredAndTested,
            MachineReleased = model.MachineReleased,
            TransportOrdered = model.TransportOrdered,
            PlannedMachineAssembled = model.PlannedMachineAssembled,
            PlannedMachineWiredAndTested = model.PlannedMachineWiredAndTested,
            PlannedTransport = model.PlannedTransport,
            DueDate = model.DueDate,
            ProductionYear = model.ProductionYear,
            SerialNumber = model.SerialNumber,
            ElectricalCabinet = model.ElectricalCabinet != null ? await GetModelInContext(CreateElectricalCabinet, model.ElectricalCabinet, _context.ElectricalCabinets) : null,
            Electrician = model.Electrician != null ? await GetModelInContext(CreateEmployee, model.Electrician, _context.Employees) : null,
            ControlCabinetWorkStartDate = model.ControlCabinetWorkStartDate,
            ElectricalDiagramCompleted = model.ElectricalDiagramCompleted,
        };

        if (model.ComponentsToOrderItem != null)
        {
            foreach (var ctoi in model.ComponentsToOrderItem.Where(x => x.ChangeType == SystemChangeType.Added))
                result.ComponentToOrderItems.Add(await CreateComponentToOrderItem(ctoi));
        }

        return result;
    }

    public async Task<OrderItem> CreateOrderItem(MoriaSalesOrderItem model, string moriaSymbol)
    {
        return new()
        {
            SubiektId = model.Id,
            Symbol = moriaSymbol + model.Index.ToString().PadLeft(2, '0'),
            Index = model.Index,
            Quantity = (double)model.Quantity,
            Description = model.Remarks,
            Product = await GetSubiektModelInContext(CreateProduct, UpdateProduct, model.Product, _context.Products),
            DueDate = model.DueDate,
            MachineWeight = model.Weight,
            Power = model.Power,
            ProductionYear = model.ProductionYear,
            SerialNumber = model.SerialNumber,
        };
    }

    public async Task UpdateOrderItem(MoriaSalesOrderItem model, string moriaSymbol, OrderItem entity)
    {
        entity.Symbol = moriaSymbol + model.Index.ToString().PadLeft(2, '0');
        entity.Index = model.Index;
        entity.Quantity = (double)model.Quantity;
        entity.Description = model.Remarks;
        entity.Product = await GetSubiektModelInContext(CreateProduct, UpdateProduct, model.Product, _context.Products);
        entity.DueDate = model.DueDate;
        entity.MachineWeight = model.Weight;
        entity.Power = model.Power;
        entity.ProductionYear = model.ProductionYear;
        entity.SerialNumber = model.SerialNumber;
    }

    public async Task UpdateElectricOrderItem(OrderItem orderItem, OrderItemDo model)
    {
        orderItem.ElectricaCabinetCompleted = model.ElectricaCabinetCompleted;
        orderItem.MachineWiredAndTested = model.MachineWiredAndTested;
        orderItem.Electrician = model.Electrician != null ? await GetModelInContext(CreateEmployee, model.Electrician, _context.Employees) : null;
        orderItem.ElectricalCabinet = model.ElectricalCabinet != null ? await GetModelInContext(CreateElectricalCabinet, model.ElectricalCabinet, _context.ElectricalCabinets) : null;
        orderItem.ControlCabinetWorkStartDate = model.ControlCabinetWorkStartDate;
        orderItem.ElectricalDiagramCompleted = model.ElectricalDiagramCompleted;
        orderItem.IsLocked = false;
        orderItem.LockedBy = default;
    }

    public async Task UpdateOrderItem(OrderItem orderItem, OrderItemDo model)
    {
        orderItem.Description = model.Description;
        orderItem.Index = model.Index;
        orderItem.MachineWeight = model.MachineWeight;
        orderItem.Notes = model.Notes;
        orderItem.LastModified = model.LastModified;
        orderItem.Quantity = model.Quantity;

        orderItem.Component = await GetModelInContext(CreateComponent, model.Component, _context.Components);
        orderItem.Product = await GetModelInContext(CreateProduct, model.Product, _context.Products);
        orderItem.Designer = await GetModelInContext(CreateEmployee, model.Designer, _context.Employees);
        orderItem.Drive = await GetModelInContext(CreateDrive, model.Drive, _context.Drives);
        orderItem.Warehouse = await GetModelInContext(CreateWarehouse, model.Warehouse, _context.Warehouses);

        orderItem.MainColor = model.MainColor != null ? await GetModelInContext(CreateColor, model.MainColor, _context.Colors) : null;
        orderItem.DetailsColor = model.DetailsColor != null ? await GetModelInContext(CreateColor, model.DetailsColor, _context.Colors) : null;
        orderItem.Power = model.Power;
        orderItem.ProductionYear = model.ProductionYear;
        orderItem.SerialNumber = model.SerialNumber;
        orderItem.TechnicalDrawingLink = model.TechnicalDrawingLink;
        //orderItem.ElectricaCabinetCompleted = model.ElectricaCabinetCompleted;
        orderItem.TechnicalDrawingCompleted = model.TechnicalDrawingCompleted;
        orderItem.CuttingCompleted = model.CuttingCompleted;
        orderItem.MetalCliningCompleted = model.MetalCliningCompleted;
        orderItem.PaintingCompleted = model.PaintingCompleted;
        orderItem.ElectricalDescription = model.ElectricialDescription;
        orderItem.MachineAssembled = model.MachineAssembled;
        orderItem.WeldingCompleted = model.WeldingCompleted;
        //orderItem.MachineWiredAndTested = model.MachineWiredAndTested;
        orderItem.MachineReleased = model.MachineReleased;
        orderItem.TransportOrdered = model.TransportOrdered;
        orderItem.PlannedMachineAssembled = model.PlannedMachineAssembled;
        orderItem.PlannedMachineWiredAndTested = model.PlannedMachineWiredAndTested;
        orderItem.PlannedTransport = model.PlannedTransport;
        orderItem.DueDate = model.DueDate;
        orderItem.IsLocked = false;
        orderItem.LockedBy = default;

        foreach (var ctoi in model.ComponentsToOrderItem.Where(x => x.ChangeType != SystemChangeType.None))
        {
            //motorgear.Drive = model;
            switch (ctoi.ChangeType)
            {
                case SystemChangeType.Added:
                    orderItem.ComponentToOrderItems.Add(await CreateComponentToOrderItem(ctoi));
                    break;
                case SystemChangeType.Modified:
                    var contextComponentToOrderItem = await _context.ComponentToOrderItems.Include(x => x.Component).FirstOrDefaultAsync(x => x.Id == ctoi.Id);
                    if (contextComponentToOrderItem != null)
                        await UpdateComponentToOrderItem(contextComponentToOrderItem, ctoi);
                    break;
                case SystemChangeType.Deleted:
                    var searchComponentToOrderItem = await _context.ComponentToOrderItems.FindAsync(ctoi.Id);
                    if (searchComponentToOrderItem != null)
                        _context.ComponentToOrderItems.Remove(searchComponentToOrderItem);
                    break;
                default:
                    break;
            }
        }
    }

    #endregion

    #region ComponentToOrderItem

    public ComponentToOrderItemDo GetComponentToOrderItemDo(ComponentToOrderItem ctoi)
    {
        return new()
        {
            Id = ctoi.Id,
            LastModified = ctoi.LastModified,
            Color = ctoi.Color == null ? null : GetColorDo(ctoi.Color),
            Component = ctoi.Component == null ? null : GetComponentDo(ctoi.Component),
            Quantity = ctoi.Quantity,
            Drive = ctoi.Drive == null ? null : GetDriveDo(ctoi.Drive),
        };
    }

    public async Task<ComponentToOrderItem> CreateComponentToOrderItem(ComponentToOrderItemDo model)
    {
        return new()
        {
            Color = model.Color != null ? await GetModelInContext(CreateColor, model.Color, _context.Colors) : null,
            Component = model.Component != null ? await GetModelInContext(CreateComponent, model.Component, _context.Components) : null,
            Drive = model.Drive != null ? await GetModelInContext(CreateDrive, model.Drive, _context.Drives) : null,
            Quantity = model.Quantity,
            LastModified = model.LastModified,
        };
    }

    public async Task UpdateComponentToOrderItem(ComponentToOrderItem ctoi, ComponentToOrderItemDo model)
    {
        ctoi.Color = model.Color != null ? await GetModelInContext(CreateColor, model.Color, _context.Colors) : null;
        ctoi.Component = model.Component != null ? await GetModelInContext(CreateComponent, model.Component, _context.Components) : null;
        ctoi.Drive = model.Drive != null ? await GetModelInContext(CreateDrive, model.Drive, _context.Drives) : null;
        ctoi.Quantity = model.Quantity;
        ctoi.LastModified = model.LastModified;
    }

    #endregion

    #region MotorGearToDrive

    public async Task<MotorGearToDrive> CreateMotorGearToDrive(MotorGearToDriveDo motorGearToDrive)
    {
        return new()
        {
            Quantity = motorGearToDrive.Quantity,
            MotorGear = await GetModelInContext(CreateMotorGear, motorGearToDrive.MotorGear, _context.MotorGears),
            Drive = await GetModelInContext(CreateDrive, motorGearToDrive.Drive, _context.Drives),
            LastModified = motorGearToDrive.LastModified,
        };
    }

    public async Task UpdateMotorGearToDrive(MotorGearToDrive mgtd, MotorGearToDriveDo model)
    {
        mgtd.LastModified = model.LastModified;
        mgtd.Quantity = model.Quantity;
        mgtd.MotorGear = await GetModelInContext(CreateMotorGear, model.MotorGear, _context.MotorGears);
    }

    public IEnumerable<MotorGearToDriveDo> GetDoMotorGearBoxToDrives(IEnumerable<MotorGearToDrive> motorGearToDrives)
    {
        var result = new List<MotorGearToDriveDo>();

        foreach (var mgtd in motorGearToDrives)
            result.Add(GetDoMotorGearBoxToDrive(mgtd));

        return result;
    }

    public MotorGearToDriveDo GetDoMotorGearBoxToDrive(MotorGearToDrive motorGearToDrive)
        => new()
        {
            Id = motorGearToDrive.Id,
            Drive = motorGearToDrive.Drive != null ? GetDriveDo(motorGearToDrive.Drive, false) : null,
            MotorGear = motorGearToDrive.MotorGear != null ? GetMotorGear(motorGearToDrive.MotorGear) : null,
            Quantity = motorGearToDrive.Quantity,
            LastModified = motorGearToDrive.LastModified
        };


    #endregion

    #region DriveToComponent

    public IList<DriveToComponentDo> GetDriveToComponents(IEnumerable<DriveToComponent> drives)
    {
        var result = new List<DriveToComponentDo>();

        foreach (var dtc in drives)
            result.Add(GetDriveToComponentDo(dtc));

        return result;
    }

    public DriveToComponentDo GetDriveToComponentDo(DriveToComponent drive)
    => new()
    {
        Id = drive.Id,
        Drive = drive.Drive != null ? GetDriveDo(drive.Drive, false) : null,
        LastModified = drive.LastModified,
        Quantity = drive.Quantity
    };

    public async Task<DriveToComponent> CreateDriveToComponent(DriveToComponentDo model)
    {
        return new()
        {
            Quantity = model.Quantity,
            Drive = model.Drive != null ? await GetModelInContext(CreateDrive, model.Drive, _context.Drives) : null,
            LastModified = model.LastModified,
        };
    }

    public async Task UpdateDriveToComponent(DriveToComponent dtc, DriveToComponentDo model)
    {
        dtc.Quantity = model.Quantity;
        dtc.LastModified = model.LastModified;
        dtc.Drive = model.Drive != null ? await GetModelInContext(CreateDrive, model.Drive, _context.Drives) : null;
    }

    #endregion

    #region Brake

    public BrakeDo GetBrakeDo(Brake brake)
    {
        return new BrakeDo()
        {
            Id = brake.Id,
            Kind = brake.Kind,
            LastModified = brake.LastModified,
        };
    }

    public async Task<Brake> CreateBrake(BrakeDo brakeDo)
    {
        return new()
        {
            Id = brakeDo.Id,
            Kind = brakeDo.Kind,
            LastModified = brakeDo.LastModified,
        };
    }

    public async Task UpdateBrake(Brake brake, BrakeDo brakeDo)
    {
        brake.Id = brakeDo.Id;
        brake.Kind = brakeDo.Kind;
        brake.LastModified = brakeDo.LastModified;
        brake.IsLocked = false;
        brake.LockedBy = string.Empty;
    }
    #endregion

    #region ExternalCooling

    public ExternalCoolingDo GetExternalCoolingDo(ExternalCooling externalCooling)
    {
        return new ExternalCoolingDo()
        {
            Id = externalCooling.Id,
            Type = externalCooling.Type,
            Power = externalCooling.Power,
            LastModified = externalCooling.LastModified,
        };
    }

    public async Task<ExternalCooling> CreateExternalCooling(ExternalCoolingDo externalCoolingDo)
    {
        return new()
        {
            Id = externalCoolingDo.Id,
            Type = externalCoolingDo.Type,
            Power = externalCoolingDo.Power,
            LastModified = externalCoolingDo.LastModified,
        };
    }

    public async Task UpdateExternalCooling(ExternalCooling externalCooling, ExternalCoolingDo externalCoolingDo)
    {
        externalCooling.Id = externalCoolingDo.Id;
        externalCooling.Type = externalCoolingDo.Type;
        externalCooling.Power = externalCoolingDo.Power;
        externalCooling.LastModified = externalCoolingDo.LastModified;
        externalCooling.IsLocked = false;
        externalCooling.LockedBy = string.Empty;
    }
    #endregion

    #region Inverter

    public InverterDo GetInverterDo(Inverter inverterDo)
    {
        return new InverterDo()
        {
            Id = inverterDo.Id,
            Type = inverterDo.Type,
            Power = inverterDo.Power,
            LastModified = inverterDo.LastModified,
        };
    }

    public async Task<Inverter> CreateInverter(InverterDo inverterDo)
    {
        return new()
        {
            Id = inverterDo.Id,
            Type = inverterDo.Type,
            Power = inverterDo.Power,
            LastModified = inverterDo.LastModified,
        };
    }

    public async Task UpdateInverter(Inverter inverter, InverterDo inverterDo)
    {
        inverter.Id = inverterDo.Id;
        inverter.Type = inverterDo.Type;
        inverter.Power = inverterDo.Power;
        inverter.LastModified = inverterDo.LastModified;
        inverter.IsLocked = false;
        inverter.LockedBy = string.Empty;
    }
    #endregion

    #region Pump

    public PumpDo GetPumpDo(Pump pump)
    {
        return new PumpDo()
        {
            Id = pump.Id,
            Type = pump.Type,
            Size = pump.Size,
            IProperty = pump.IProperty,
            LastModified = pump.LastModified,
        };
    }

    public async Task<Pump> CreatePump(PumpDo pumpDo)
    {
        return new()
        {
            Id = pumpDo.Id,
            Type = pumpDo.Type,
            Size = pumpDo.Size,
            IProperty = pumpDo.IProperty,
            LastModified = pumpDo.LastModified,
        };
    }

    public async Task UpdatePump(Pump pump, PumpDo pumpDo)
    {
        pump.Id = pumpDo.Id;
        pump.Type = pumpDo.Type;
        pump.Size = pumpDo.Size;
        pump.IProperty = pumpDo.IProperty;
        pump.LastModified = pumpDo.LastModified;
        pump.IsLocked = false;
        pump.LockedBy = string.Empty;
    }

    #endregion

    #region Supplement

    public SupplementDo GetSupplementDo(Supplement supplement)
    {
        return new SupplementDo()
        {
            Id = supplement.Id,
            Type = supplement.Type,
            Size = supplement.Size,
            IProperty = supplement.IProperty,
            LastModified = supplement.LastModified,
        };
    }

    public async Task<Supplement> CreateSuplement(SupplementDo supplementDo)
    {
        return new()
        {
            Id = supplementDo.Id,
            Type = supplementDo.Type,
            Size = supplementDo.Size,
            IProperty = supplementDo.IProperty,
            LastModified = supplementDo.LastModified,
        };
    }

    public async Task UpdateSupplement(Supplement supplement, SupplementDo supplementDo)
    {
        supplement.Id = supplementDo.Id;
        supplement.Type = supplementDo.Type;
        supplement.Size = supplementDo.Size;
        supplement.IProperty = supplementDo.IProperty;
        supplement.LastModified = supplementDo.LastModified;
        supplement.IsLocked = false;
        supplement.LockedBy = string.Empty;
    }

    #endregion

    #region Variator

    public VariatorDo GetVariatortDo(Variator variator)
    {
        return new VariatorDo()
        {
            Id = variator.Id,
            Type = variator.Type,
            LastModified = variator.LastModified,
        };
    }

    public async Task<Variator> CreateVariator(VariatorDo variator)
    {
        return new()
        {
            Id = variator.Id,
            Type = variator.Type,
            LastModified = variator.LastModified,
        };
    }

    public async Task UpdateVariator(Variator variator, VariatorDo variatorDo)
    {
        variator.Id = variatorDo.Id;
        variator.Type = variatorDo.Type;
        variator.LastModified = variatorDo.LastModified;
        variator.IsLocked = false;
        variator.LockedBy = string.Empty;
    }

    #endregion

    public ElectricalCabinetDo GetElectricalCabinet(ElectricalCabinet cabinet)
    {
        return new ElectricalCabinetDo()
        {
            Id = cabinet.Id,
            Symbol = cabinet.Symbol,
            LastModified = cabinet.LastModified
        };
    }
    public async Task<ElectricalCabinet> CreateElectricalCabinet(ElectricalCabinetDo cabinet)
    {
        return new()
        {
            Symbol = cabinet.Symbol,
            LastModified = cabinet.LastModified
        };
    }

    public async Task UpdateElectricalCabinet(ElectricalCabinet cabinet, ElectricalCabinetDo cabinetModel)
    {
        cabinet.Symbol = cabinetModel.Symbol;
        cabinet.LastModified = cabinetModel.LastModified;
        cabinet.IsLocked = false;
        cabinet.LockedBy = default;
    }




    #region ListViewSetup

    public ListViewSetupDo GetListViewSetupDo(ListViewSetup setup)
    => new()
    {
        Id = setup.Id,
        LastModified = setup.LastModified,
        ListViewId = setup.ListViewId,
        Columns = System.Text.Json.JsonSerializer.Deserialize<IList<ListViewColumnProvider>>(setup.Columns)
    };

    public async Task<ListViewSetup> CreateListViewSetup(ListViewSetupDo setup, Employee employee)
    {
        return new()
        {
            ListViewId = setup.ListViewId,
            Employee = employee,
            Columns = System.Text.Json.JsonSerializer.Serialize(setup.Columns)
        };
    }

    public async Task UpdateListViewSetup(ListViewSetup setup, ListViewSetupDo setupModel)
    {
        setup.Columns = System.Text.Json.JsonSerializer.Serialize(setupModel.Columns);
    }

    #endregion
}
