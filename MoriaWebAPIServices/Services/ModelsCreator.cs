using MoriaModels.Models.EntityPersonel;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Contexts;

namespace MoriaWebAPIServices.Services;
public class ModelsCreator
{
    readonly ApplicationDbContext _context;

    public ModelsCreator(ApplicationDbContext context)
    {
        _context = context;
    }

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
        };

        if (employee.Position == null)
            result.Position = null;
        else
        {
            result.Position = await _context.Positions.FindAsync(employee.Position.Id);
            if (result.Position == null)
            {
                result.Position = await CreatePosition(employee.Position);
                await _context.AddAsync(result.Position);
            }
        }

        return result;
    }

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
}
