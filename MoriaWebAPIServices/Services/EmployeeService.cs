using MoriaModels.Models.EntityPersonel;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPIServices.Services;
public class EmployeeService: IEmployeeService
{
    readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDo> LogIn(string username, string password)
    {
        var employee = _context.Employees.FirstOrDefault(x => x.Username.Equals(username));
        if (employee == null || !employee.Password.Equals(password))
            return null;
        
        return GetEmployeeDo(employee);
    }

    public async Task<EmployeeDo> GetEmployee(int id)
    {
        var employee = _context.Employees.FirstOrDefault(x => x.Id == id);
        if (employee == null)
            return null;

        return GetEmployeeDo(employee);

    }

    public async Task<IEnumerable<EmployeeDo>> GetEmployees()
    {
        List<EmployeeDo> result = new();
        foreach (var employee in _context.Employees)
            result.Add(GetEmployeeDo(employee));

        return result;
    }

    public async Task<bool> CreateEmployee(EmployeeDo employee)
    {
        var entity = await _context.AddAsync(GetEmployee(employee));

        var created = await _context.SaveChangesAsync();

        return true;
    }


    EmployeeDo GetEmployeeDo(Employee employee)
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

    Employee GetEmployee(EmployeeDo employee)
    {
        return new Employee()
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Username = employee.Username,
            Password = employee.Password,
            PhoneNumber = employee.PhoneNumber,
            LastModified = employee.LastModified,
            //Position = employee.Position != null ? new Position()
            //{
            //    Code = employee.Position.Code,
            //    Name = employee.Position.Name,
            //    LastModified = employee.LastModified,

            //} : null,
        };
    }
}
