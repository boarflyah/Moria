using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Products;
using MoriaModels.Models.Warehouses;
using MoriaModelsDo.Base;
using MoriaModelsDo.Base.Enums;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.Products;
using MoriaWebAPIServices.Contexts;

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
            Position = employee.Position != null ? new()
            {
                Id = employee.Position.Id,
                Code = employee.Position.Code,
                LastModified = employee.Position.LastModified,
                Name = employee.Position.Name,
                IsLocked = employee.Position.IsLocked
            } : null,
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
    }

    #endregion

    #region SteelKind

    public SteelKindDo GetSteelKindDo(SteelKind steelKind)
        => new()
        {
            Id = steelKind.Id,
            Name = steelKind.Name,
            Symbol = steelKind.Symbol
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
    }

    #endregion

    #region Color

    public ColorDo GetColorDo(Color color)
    {
        return new ColorDo()
        {
            Id = color.Id,
            Name = color.Name,
            Code = color.Code,
        };
    }

    public async Task<Color> CreateColor(ColorDo colorDo)
    {
        return new()
        {
            Name = colorDo.Name,
            Code = colorDo.Code,
        };
    }

    public async Task UpdateColor(Color color, ColorDo colorModel)
    {
        color.Name = colorModel.Name;
        color.Code = colorModel.Code;
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
        };
    }

    public async Task<Contact> CreateContact(ContactDo contactDo)
    {
        return new()
        {
            Symbol = contactDo.Symbol,
            LongName = contactDo.LongName,
            ShortName = contactDo.ShortName,

        };
    }

    public async Task UpdateContact(Contact contact, ContactDo contactModel)
    {
        contact.Symbol = contactModel.Symbol;
        contact.LongName = contactModel.LongName;
        contact.ShortName = contactModel.ShortName;
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
        };
    }

    public async Task<Motor> CreateMotor(MotorDo motorDo)
    {
        return new()
        {
            Symbol = motorDo.Symbol,
            Power = motorDo.Power,
            Name = motorDo.Name,

        };
    }

    public async Task UpdateMotor(Motor motor, MotorDo motorModel)
    {
        motor.Symbol = motorModel.Symbol;
        motor.Power = motorModel.Power;
        motor.Name = motorModel.Name;
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
        };
    }

    public async Task<MotorGear> CreateMotorGear(MotorGearDo motorGearDo)
    {
        return new()
        {
            Symbol = motorGearDo.Symbol,
            Name = motorGearDo.Name,
            Ratio = motorGearDo.Ratio

        };
    }

    public async Task UpdateMotorGear(MotorGear motorGear, MotorGearDo motorGearModel)
    {
        motorGear.Symbol = motorGearModel.Symbol;
        motorGear.Ratio = motorGearModel.Ratio;
        motorGear.Name = motorGearModel.Name;
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
        };
    }

    public async Task<Warehouse> CreateWarehouse(WarehouseDo warehouseDo)
    {
        return new()
        {
            Name = warehouseDo.Name,
            Symbol = warehouseDo.Symbol,
        };
    }

    public async Task UpdateWarehouse(Warehouse warehouse, WarehouseDo warehouseModel)
    {
        warehouse.Symbol = warehouseModel.Symbol;
        warehouse.Name = warehouseModel.Name;
    }
    #endregion

    #region Category

    public CategoryDo GetCategoryDo(Category category)
        => new()
        {
            Id = category.Id,
            Name = category.Name,
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
    }

    #endregion

    #region Component

    public ComponentDo GetComponentDo(Component component)
    {
        return new()
        {
            Id = component.Id,
            ElectricalDescription = component.ElectricalDescription,
            ComponentProduct = component.ComponentProduct != null ? GetNestedProductDo(component.ComponentProduct) : null,
        };
    }

    public async Task<Component> CreateComponent(ComponentDo component)
    {
        return new()
        {
            ElectricalDescription = component.ElectricalDescription,
            LastModified = component.LastModified,
            ComponentProduct = await GetModelInContext(CreateProduct, component.ComponentProduct, _context.Products),
        };
    }

    public async Task UpdateComponent(Component component, ComponentDo componentModel)
    {
        component.ElectricalDescription = componentModel.ElectricalDescription;
        component.ComponentProduct = await GetModelInContext(CreateProduct, componentModel.ComponentProduct, _context.Products);
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
            Components = product.Components != null && product.Components.Any() ? product.Components.Select(GetComponentDo) : null,
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
            Symbol = product.Symbol
        };

        result.SteelKind = await GetModelInContext(CreateSteelKind, product.SteelKind, _context.SteelKinds);
        result.Category = await GetModelInContext(CreateCategory, product.Category, _context.Categories);

        foreach (var component in product.Components.Where(x => x.ChangeType == SystemChangeType.Added))
            result.Components.Add(await GetModelInContext(CreateComponent, component, _context.Components));

        return result;
    }

    public async Task UpdateProduct(Product product, ProductDo productModel)
    {
        product.IsMainMachine = productModel.IsMainMachine;
        product.Name = productModel.Name;
        product.SerialNumber = productModel.SerialNumber;
        product.Symbol = productModel.Symbol;

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

    public DriveDo GetDriveDo(Drive drive)
    {
        return new()
        {
            Id = drive.Id,
            Inverter = drive.Inverter,
            Variator = drive.Variator,
            Quantity = drive.Quantity,
            //Motor = drive.Motor != null ? GetMotorDo(drive.Motor) : null,
            //Gearboxes = drive.MotorGearToDrives != null && drive.MotorGearToDrives.Any() ? GetDoMotorGearBoxToDrives(drive.MotorGearToDrives) : null,
            LastModified = drive.LastModified,
        };
    }

    public async Task<Drive> CreateDrive(DriveDo drive)
    {
        var result = new Drive()
        {
            Inverter = drive.Inverter,
            Quantity = drive.Quantity,
            Variator = drive.Variator,
        };

        //result.Motor = await GetModelInContext(CreateMotorr, drive.Motor, _context.Motors);

        //foreach (var component in product.Components.Where(x => x.ChangeType == SystemChangeType.Added))
        //    result.Components.Add(await GetModelInContext(CreateComponent, component, _context.Components));

        return result;
    }

    public async Task UpdateDrive(Drive drive, DriveDo driveModel)
    {
        drive.Inverter = driveModel.Inverter;
        drive.Variator = driveModel.Variator;
        drive.Quantity = driveModel.Quantity;

        //product.SteelKind = await GetModelInContext(CreateSteelKind, productModel.SteelKind, _context.SteelKinds);
        //product.Category = await GetModelInContext(CreateCategory, productModel.Category, _context.Categories);

        //foreach (var component in productModel.Components.Where(x => x.ChangeType != SystemChangeType.None))
        //{
        //    switch (component.ChangeType)
        //    {
        //        case SystemChangeType.Added:
        //            product.Components.Add(await GetModelInContext(CreateComponent, component, _context.Components));
        //            break;
        //        case SystemChangeType.Modified:
        //            var contextComponent = await _context.Components.Include(x => x.ComponentProduct).FirstOrDefaultAsync(x => x.Id == component.Id);
        //            if (contextComponent != null)
        //                await UpdateComponent(contextComponent, component);
        //            break;
        //        case SystemChangeType.Deleted:
        //            var searchComponent = await _context.Components.FindAsync(component.Id);
        //            if (searchComponent != null)
        //                _context.Components.Remove(searchComponent);
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }

    #endregion
}
