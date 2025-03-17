using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.DriveComponents.Relations;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Products;
using MoriaModels.Models.Warehouses;
using MoriaModelsDo.Base;
using MoriaModelsDo.Base.Enums;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents.Relations;
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
            LastModified = color.LastModified,
        };
    }

    public async Task<Color> CreateColor(ColorDo colorDo)
    {
        return new()
        {
            Name = colorDo.Name,
            Code = colorDo.Code,
            LastModified = colorDo.LastModified,
        };
    }

    public async Task UpdateColor(Color color, ColorDo colorModel)
    {
        color.Name = colorModel.Name;
        color.Code = colorModel.Code;
        color.LastModified = colorModel.LastModified;
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

    public async Task UpdateContact(Contact contact, ContactDo contactModel)
    {
        contact.Symbol = contactModel.Symbol;
        contact.LongName = contactModel.LongName;
        contact.ShortName = contactModel.ShortName;
        contact.LastModified = contactModel.LastModified;
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

    public async Task UpdateWarehouse(Warehouse warehouse, WarehouseDo warehouseModel)
    {
        warehouse.Symbol = warehouseModel.Symbol;
        warehouse.Name = warehouseModel.Name;
        warehouse.LastModified = warehouseModel.LastModified;
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

    public async Task UpdateProduct(Product product, ProductDo productModel)
    {
        product.IsMainMachine = productModel.IsMainMachine;
        product.Name = productModel.Name;
        product.SerialNumber = productModel.SerialNumber;
        product.Symbol = productModel.Symbol;
        product.LastModified = productModel.LastModified;

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
            Inverter = drive.Inverter,
            Variator = drive.Variator,
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
            Inverter = drive.Inverter,
            Quantity = drive.Quantity,
            Variator = drive.Variator,
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
        drive.Inverter = driveModel.Inverter;
        drive.Variator = driveModel.Variator;
        drive.Quantity = driveModel.Quantity;
        drive.Name = driveModel.Name;
        drive.LastModified = driveModel.LastModified;

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
}
