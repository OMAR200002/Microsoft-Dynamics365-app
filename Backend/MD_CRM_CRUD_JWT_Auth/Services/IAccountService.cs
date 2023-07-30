using MD_CRM_CRUD_JWT_Auth.Models;
using MD_CRM_CRUD_JWT_Auth.Requests;

namespace MD_CRM_CRUD_JWT_Auth.Services
{
    public interface IAccountService
    {
        public Task<List<Account>> GetAccounts();
        public Task<Account> GetAccountById(Guid accountid);
        public Task<string> GetAccountNameById(Guid accountid);
        public Task<Account> GetAccountByName(string fullname);
        public Task<bool> AddAccount(AccountRequest account);
        public Task<bool> UpdateAccount(Guid accountid, AccountRequest account);
        public Task<bool> ToggleStatus(Guid accountid, StatusRequest statusRequest);
        public Task<bool> DeleteAccount(Guid accountid);
        public Task DeleteAccounts(string[] accountsIds);
    }
}
