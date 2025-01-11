using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.EntityPersonel;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPIServices.Services.Dictionaries;
public class EmployeeControllerService : IEmployeeControllerService
{
    readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;

    public EmployeeControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<EmployeeDo> LogIn(string username, string password)
    {
        var employee = _context.Employees.FirstOrDefault(x => x.Username.Equals(username));
        if (employee == null || !employee.Password.Equals(password))
            return null;

        return _creator.GetEmployeeDo(employee);
    }

    public async Task<EmployeeDo> GetEmployee(int id)
    {
        var employee = _context.Employees.Include(x => x.Position).FirstOrDefault(x => x.Id == id);
        if (employee == null)
            return null;

        return _creator.GetEmployeeDo(employee);

    }

    public async Task<IEnumerable<EmployeeDo>> GetEmployees()
    {
        //return await _context.Employees
        //    .Select(entity => GetEmployeeDo(entity))
        //    .ToListAsync();

        List<EmployeeDo> result = new();
        foreach (var employee in _context.Employees)
            result.Add(_creator.GetEmployeeDo(employee));

        return result;
    }

    public async Task<bool> CreateEmployee(EmployeeDo employee)
    {
        var entity = await _context.AddAsync(await _creator.CreateEmployee(employee));
        var created = await _context.SaveChangesAsync();

        //TODO - obsluzyc powtorzenie Username

        return true;
    }

    public async Task<EmployeeDo> EditEmployee(EmployeeDo employee)
    {
        var searchEmployee = await _context.Employees.FindAsync(employee.Id);
        if (searchEmployee == null)
            return null;

        searchEmployee.FirstName = employee.FirstName;
        searchEmployee.LastName = employee.LastName;
        searchEmployee.Username = employee.Username;
        searchEmployee.Password = employee.Password;
        searchEmployee.PhoneNumber = employee.PhoneNumber;
        //searchEmployee.PositionId = employee.Position?.Id ?? 0;

        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        var searchEmployee = await _context.Employees.FindAsync(id);
        if (searchEmployee == null)
            return false;

        _context.Employees.Remove(searchEmployee);

        return await _context.SaveChangesAsync() == 1;
    }
}
