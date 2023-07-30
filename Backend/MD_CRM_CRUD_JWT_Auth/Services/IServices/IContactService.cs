using MD_CRM_CRUD_JWT_Auth.Models;
using MD_CRM_CRUD_JWT_Auth.Requests.Contact;

namespace MD_CRM_CRUD_JWT_Auth.Services.IServices
{
    public interface IContactService
    {
        public Task<List<Contact>> GetContacts();
        public Task<Contact> GetContactById(Guid contactId);
        public Task<string> GetContactFullnameById(Guid contactId);
        public Task<Contact> GetContactByFullname(string fullname);
        public Task<Contact> AddContact(ContactCreateRequest contactCreate);
        public Task<bool> UpdateContact(Guid contactId, ContactUpdateRequest contactUpdate);
        public Task<bool> DeleteContact(Guid contactId);
    }
}
