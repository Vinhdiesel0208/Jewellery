using System;
using Group3.Db;
using Group3.Reponsitory;
using Lib;
using Microsoft.EntityFrameworkCore;

namespace Group3.Services
{
	public class ContactServicesImp : IContactDetail
    {
        private DatabaseContext db;
        public ContactServicesImp(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<bool> addContacts(Contact newContact)
        {
            await db.Contact.AddAsync(newContact);
            await db.SaveChangesAsync();
            return true;
        }
     
        public async Task<Contact> GetContactById(int id)
        {
            return await db.Contact.SingleOrDefaultAsync(b => b.Id.Equals(id));
        }

        public async Task<IEnumerable<Contact>> GetContactList()
        {
            return await db.Contact.ToListAsync();
        }

        public async Task<bool> removeContacts(int id)
        {
            var contact = await db.Contact.FirstOrDefaultAsync(n => n.Id.Equals(id));
            if (contact != null)
            {
                db.Contact.Remove(contact);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> updateContacts(Contact editContact)
        {
            var news = await db.Contact.FirstOrDefaultAsync(n => n.Id == editContact.Id);
            if (news != null)
            {
                news.Id = editContact.Id;
                news.Name = editContact.Name;
                news.Phone = editContact.Phone;
                news.Email = editContact.Email;
                news.Address = editContact.Address;
                news.Message = editContact.Message;

                await db.SaveChangesAsync();
                return true;
            }
            else { return false; };
        }
    }
}

