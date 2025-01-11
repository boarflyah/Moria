using Microsoft.EntityFrameworkCore;
using MoriaModels.Models.Orders;
using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Contexts;

namespace MoriaWebAPIServices.Services.Dictionaries;

public class ContactControllerService
{
    private readonly ApplicationDbContext _context;

    public ContactControllerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ContactDo> CreateContact(ContactDo contact)
    {
        var createdContact = new Contact
        {
            ShortName = contact.ShortName,
            LongName = contact.LongName,
            Symbol = contact.Symbol
        };

        _context.Contacts.Add(createdContact);
        await _context.SaveChangesAsync();

        contact.Id = createdContact.Id;
        return contact;
    }

    public async Task<ContactDo?> GetContactById(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact == null) return null;

        return new ContactDo
        {
            Id = contact.Id,
            ShortName = contact.ShortName,
            LongName = contact.LongName,
            Symbol = contact.Symbol
        };
    }

    public async Task<List<ContactDo>> GetAllContacts()
    {
        return await _context.Contacts
            .Select(contact => new ContactDo
            {
                Id = contact.Id,
                ShortName = contact.ShortName,
                LongName = contact.LongName,
                Symbol = contact.Symbol
            })
            .ToListAsync();
    }

    public async Task<ContactDo?> EditContact(ContactDo contact)
    {
        var searchContact = await _context.Contacts.FindAsync(contact.Id);
        if (searchContact == null) return null;

        searchContact.ShortName = contact.ShortName;
        searchContact.LongName = contact.LongName;
        searchContact.Symbol = contact.Symbol;

        await _context.SaveChangesAsync();

        return contact;
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
