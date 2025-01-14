using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktopServices.Interfaces.API
{
    public interface IApiContactService
    {
        Task<ContactDo> CreateContact(string username, ContactDo contact);
        Task<bool> DeleteContact(string username, int id);
        Task<ContactDo> GetContact(string username, int id);
        Task<IEnumerable<ContactDo>> GetContacts(string username);
        Task<ContactDo> UpdateContact(string username, ContactDo contact);
    }
}