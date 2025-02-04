using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.Base;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Products;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
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

    #endregion

    #region Category

    public CategoryDo GetCategoryDo(Category category)
        => new()
        {
            Id = category.Id,
            Name = category.Name
        };

    public async Task<Category> CreateCategory(CategoryDo category)
    {
        return new()
        {
            Name = category.Name,
            LastModified = category.LastModified
        };
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

        //TODO resolve components

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
    }

    #endregion
}
