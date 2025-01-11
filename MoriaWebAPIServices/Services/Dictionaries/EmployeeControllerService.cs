
using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.EntityPersonel;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Contexts;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class EmployeeControllerService
{
    private readonly ApplicationDbContext _context;

    public EmployeeControllerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDo> CreateEmployee(EmployeeDo employee)
    {
        var position = _context.Positions.FirstOrDefault(x => x.Id == employee.Position.Id);
        var createdEmployee = new Employee
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Username = employee.Username,
            Password = employee.Password,
            PhoneNumber = employee.PhoneNumber,
            Position = position == null ?
            new Position
            {
                Id = employee.Position.Id,
                Name = employee.Position.Name,
                Code = employee.Position.Code
            } : position
        };

        _context.Employees.Add(createdEmployee);
        await _context.SaveChangesAsync();

        employee.Id = createdEmployee.Id;
        return employee;
    }

    public async Task<EmployeeDo?> GetEmployeeById(int id)
    {
        var entity = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == id);

        if (entity == null) return null;

        return new EmployeeDo
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Username = entity.Username,
            Password = entity.Password,
            PhoneNumber = entity.PhoneNumber,
            Position = entity.Position != null
                ? new PositionDo
                {
                    Id = entity.Position.Id,
                    Name = entity.Position.Name
                }
                : null
        };
    }

    public async Task<List<EmployeeDo>> GetAllEmployees()
    {
        return await _context.Employees
            .Include(e => e.Position) 
            .Select(entity => new EmployeeDo
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Username = entity.Username,
                Password = entity.Password,
                PhoneNumber = entity.PhoneNumber,
                Position = entity.Position != null
                    ? new PositionDo
                    {
                        Id = entity.Position.Id,
                        Name = entity.Position.Name
                    }
                    : null
            })
            .ToListAsync();
    }

    public async Task<EmployeeDo?> EditEmployee(EmployeeDo employee)
    {

        var searchEmployee = await _context.Employees.FindAsync(employee.Id);
        if (searchEmployee == null) return null;

        searchEmployee.FirstName = employee.FirstName;
        searchEmployee.LastName = employee.LastName;
        searchEmployee.Username = employee.Username;
        searchEmployee.Password = employee.Password;
        searchEmployee.PhoneNumber = employee.PhoneNumber;
        searchEmployee.PositionId = employee.Position?.Id ?? 0;

        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        var searchEmployee = await _context.Employees.FindAsync(id);
        if (searchEmployee == null) return false;

        _context.Employees.Remove(searchEmployee);
        await _context.SaveChangesAsync();

        return true;
    }
}
