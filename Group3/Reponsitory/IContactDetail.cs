using Lib;

namespace Group3.Reponsitory
{
    public interface IContactDetail
    {
        Task<IEnumerable<Contact>> GetContactList();
        Task<Contact> GetContactById(int id);

        Task<bool> addContacts(Contact newContact);
        Task<bool> removeContacts(int id);
        Task<bool> updateContacts(Contact editContact);
    }
}
