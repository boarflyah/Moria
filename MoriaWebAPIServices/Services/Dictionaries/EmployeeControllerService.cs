using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using Npgsql;

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
        var employee = _context.Employees.Include(x => x.Position).ThenInclude(y => y.Permissions).FirstOrDefault(x => x.Username.Equals(username));
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

    public async Task<EmployeeDo> CreateEmployee(EmployeeDo employee)
    {
        try
        {
            var entity = await _context.AddAsync(await _creator.CreateEmployee(employee));
            var created = await _context.SaveChangesAsync();
            return _creator.GetEmployeeDo(entity.Entity);
        }
        catch (DbUpdateException due) when(due.InnerException is PostgresException pe && pe.SqlState.Equals("23505"))
        {
            throw new MoriaApiException(MoriaApiExceptionReason.ValueIsNotUnique, MoriaApiException.ApiExceptionThrownStatusCode);
        }
    }

    public async Task<EmployeeDo> UpdateEmployee(EmployeeDo employee)
    {
        try
        {
            var searchEmployee = await _context.Employees.FindAsync(employee.Id);
            if (searchEmployee == null)
                throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

            await _creator.UpdateEmployee(searchEmployee, employee);

            var created = await _context.SaveChangesAsync();
            return _creator.GetEmployeeDo(searchEmployee);
        }
        catch (DbUpdateException due) when (due.InnerException is PostgresException pe && pe.SqlState.Equals("23505"))
        {
            throw new MoriaApiException(MoriaApiExceptionReason.ValueIsNotUnique, MoriaApiException.ApiExceptionThrownStatusCode);
        }
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        var searchEmployee = await _context.Employees.FindAsync(id);
        if (searchEmployee == null)
            return false;

        if (searchEmployee.IsLocked)
            throw new MoriaApiException(MoriaApiExceptionReason.ObjectIsLocked, 406, searchEmployee.LockedBy);

        _context.Employees.Remove(searchEmployee);

        return await _context.SaveChangesAsync() == 1;
    }
}
