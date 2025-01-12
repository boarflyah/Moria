using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktopServices.Services.API;

/// <summary>
/// Used for reading enpoints in EmployeeController in MoriaApi
/// </summary>
public class ApiEmployeeService : IApiEmployeeService
{
    readonly IApiService _apiService;

    public ApiEmployeeService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<EmployeeDo> Login(string username, string password)
    {
        return await _apiService.Post<EmployeeDo>(username, WebAPIEndpointsProvider.PostLoginPath, null, new UserCredentials() { Username = username, Password = password }); 
    }

    public async Task<IEnumerable<EmployeeDo>> GetEmployees(string username)
    {
        var result = await _apiService.Get<IEnumerable<EmployeeDo>>(username, WebAPIEndpointsProvider.GetEmployeesPath, null);
        if (result == null)
            return new List<EmployeeDo>();

        return result;
    }

    public async Task<EmployeeDo> GetEmployee(string username, int id)
    {
        return await _apiService.Get<EmployeeDo>(username, WebAPIEndpointsProvider.GetEmployeePath, null, id);
    }

    public async Task<EmployeeDo> CreateEmployee(string username, EmployeeDo employee)
    {
        return await _apiService.Post<EmployeeDo>(username, WebAPIEndpointsProvider.PostEmployeePath, null, employee);
    }

    public async Task<EmployeeDo> UpdateEmployee(string username, EmployeeDo employee)
    {
        return await _apiService.Put<EmployeeDo>(username, WebAPIEndpointsProvider.PutEmployeePath, null, employee);
    }

    public async Task<bool> DeleteEmployee(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteEmployeePath, null, id);
    }

}
