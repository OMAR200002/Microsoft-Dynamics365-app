using MD_CRM_CRUD_JWT_Auth.CRM;
using Newtonsoft.Json;
using MD_CRM_CRUD_JWT_Auth.Models;
using MD_CRM_CRUD_JWT_Auth.Requests;
using Microsoft.Identity.Client;
using System.Security.Principal;

namespace MD_CRM_CRUD_JWT_Auth.Services
{
    public class AccountService : IAccountService
    {
        private readonly DynamicsCRM dynamicsCRM;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;

        public AccountService(DynamicsCRM crmConfig, IConfiguration configuration)
        {
            _configuration = configuration;
            dynamicsCRM = crmConfig;
            baseUrl = $"{_configuration.GetValue<string>("DynamicsCrmSettings:Scope")}/api/data/v9.2";
        }

        public async Task<List<Account>> GetAccounts()
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Get,
                accessToken,
                $"{baseUrl}/accounts");

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(json);
            var accounts = result!.value.ToObject<List<Account>>();

            return accounts;
        }

        public async Task<Account> GetAccountById(Guid accountid)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Get,
                accessToken,
            $"{baseUrl}/accounts({accountid})");

            var json = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<Account>(json);

            return account!;
        }

        public async Task<string> GetAccountNameById(Guid accountid)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Get,
                accessToken,
            $"{baseUrl}/accounts({accountid})?$select=name");

            var json = await response.Content.ReadAsStringAsync();
            var accountName = JsonConvert.DeserializeObject<dynamic>(json)?.name;

            return accountName!;
        }
        public async Task<Account> GetAccountByName(string name)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Get,
                accessToken,
                $"{baseUrl}/accounts?$filter=name eq '{name}'");

            var json = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<Account>(json);

            return account!;
        }

        public async Task<bool> AddAccount(AccountRequest account)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();

            // Convert the Account object to JSON
            var jsonAccount = JsonConvert.SerializeObject(account);
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Post,
                accessToken,
                $"{baseUrl}/accounts",
                jsonAccount);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAccount(Guid accountid, AccountRequest account)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();

            // Convert the Account object to JSON
            var jsonAccount = JsonConvert.SerializeObject(account);
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Patch,
            accessToken,
                $"{baseUrl}/accounts({accountid})",
                jsonAccount);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ToggleStatus(Guid accountid, StatusRequest statusRequest)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();

            // Convert the Account object to JSON
            var jsonStatus = JsonConvert.SerializeObject(statusRequest);
            Console.WriteLine(statusRequest.statecode);
            Console.WriteLine(jsonStatus);
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Patch,
            accessToken,
                $"{baseUrl}/accounts({accountid})", 
                jsonStatus);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAccount(Guid accountid)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();

            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Delete,
                accessToken,
                $"{baseUrl}/accounts({accountid})");

            return response.IsSuccessStatusCode;
        }

        public async Task DeleteAccounts(string[] accountsIds)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();

            // delete accounts one by one
            foreach(string accountId in accountsIds )
            {
            await dynamicsCRM.CrmRequest(
                HttpMethod.Delete,
                accessToken,
                $"{baseUrl}/accounts({accountId})");
            }
        }
    }
}
