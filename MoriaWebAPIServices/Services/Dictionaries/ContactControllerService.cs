using Microsoft.EntityFrameworkCore;
using MoriaBaseServices;
using MoriaModels.Models.Orders;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class ContactControllerService : IContactControllerService
{
    private readonly ApplicationDbContext _context;
    readonly ModelsCreator _creator;


    public ContactControllerService(ApplicationDbContext context, ModelsCreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public async Task<ContactDo> CreateContact(ContactDo contact)
    {
        var createdContact = await _creator.CreateContact(contact);

        _context.Contacts.Add(createdContact);
        await _context.SaveChangesAsync();

        contact.Id = createdContact.Id;
        return contact;
    }

    public async Task<ContactDo?> GetContactById(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);

        return _creator.GetContact(contact);
    }

    public async Task<List<ContactDo>> GetAllContacts()
    {
        return await _context.Contacts
            .Select(contact => _creator.GetContact(contact))
            .ToListAsync();
    }

    public async Task<ContactDo?> EditContact(ContactDo contact)
    {
        var searchContact = await _context.Contacts.FindAsync(contact.Id);
        if (searchContact == null) throw new MoriaApiException(MoriaApiExceptionReason.ObjectNotFound, MoriaApiException.ApiExceptionThrownStatusCode);
        
        await _creator.UpdateContact(searchContact, contact);
        await _context.SaveChangesAsync();

        return _creator.GetContact(searchContact);
    }

    public async Task<bool> DeleteContact(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact == null) return false;

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();

        return true;
    }
}
