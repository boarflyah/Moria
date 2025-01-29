using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktopServices.Services.API;
/// <summary>
/// Used for reading enpoints in ContactController in MoriaApi
/// </summary>
public class ApiContactService : IApiContactService
{
    readonly IApiService _apiService;

    public ApiContactService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IEnumerable<ContactDo>> GetContacts(string username)
    {
        var result = await _apiService.Get<IEnumerable<ContactDo>>(username, WebAPIEndpointsProvider.GetContactsPath, null, null);
        if (result == null)
            return new List<ContactDo>();

        return result;
    }

    public async Task<ContactDo> GetContact(string username, int id)
    {
        return await _apiService.Get<ContactDo>(username, WebAPIEndpointsProvider.GetContactPath, null, null, parameters: id);
    }

    public async Task<ContactDo> CreateContact(string username, ContactDo contact)
    {
        return await _apiService.Post<ContactDo>(username, WebAPIEndpointsProvider.PostContactPath, null, contact);
    }

    public async Task<ContactDo> UpdateContact(string username, ContactDo contact)
    {
        return await _apiService.Put<ContactDo>(username, WebAPIEndpointsProvider.PutContactPath, null, contact);
    }

    public async Task<bool> DeleteContact(string username, int id)
    {
        return await _apiService.Delete<bool>(username, WebAPIEndpointsProvider.DeleteContactPath, null, id);
    }
}
