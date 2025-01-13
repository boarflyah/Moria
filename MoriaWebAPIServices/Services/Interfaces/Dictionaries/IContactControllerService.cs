using MoriaModelsDo.Models.Contacts;

namespace MoriaWebAPIServices.Services.Interfaces.Dictionaries
{
    public interface IContactControllerService
    {
        Task<ContactDo> CreateContact(ContactDo contact);
        Task<bool> DeleteContact(int id);
        Task<ContactDo> EditContact(ContactDo contact);
        Task<List<ContactDo>> GetAllContacts();
        Task<ContactDo> GetContactById(int id);
    }
}