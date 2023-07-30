using MD_CRM_CRUD_JWT_Auth.CRM;
using MD_CRM_CRUD_JWT_Auth.Models;
using MD_CRM_CRUD_JWT_Auth.Requests.Contact;
using MD_CRM_CRUD_JWT_Auth.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http;

namespace MD_CRM_CRUD_JWT_Auth.Services
{
    public class ContactService : IContactService
    {
        private readonly DynamicsCRM dynamicsCRM;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;

        public ContactService(DynamicsCRM crmConfig, IConfiguration configuration)
        {
            _configuration = configuration;
            dynamicsCRM = crmConfig;
            baseUrl = $"{_configuration.GetValue<string>("DynamicsCrmSettings:Scope")}/api/data/v9.2";
        }

        public async Task<List<Contact>> GetContacts()
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Get,
                accessToken,
                $"{baseUrl}/contacts");

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(json);
            var contacts = result?.value.ToObject<List<Contact>>();

            return contacts!;
        }

        public async Task<Contact> GetContactById(Guid contactid)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Get,
                accessToken,
            $"{baseUrl}/contacts({contactid})");

            var json = await response.Content.ReadAsStringAsync();
            var contact = JsonConvert.DeserializeObject<Contact>(json);

            return contact!;
        }

        public async Task<string> GetContactFullnameById(Guid contactid)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Get,
                accessToken,
            $"{baseUrl}/contacts({contactid})?$select=fullname");

            var json = await response.Content.ReadAsStringAsync();
            var ContactName = JsonConvert.DeserializeObject<dynamic>(json)?.fullname;

            return ContactName!;
        }
        public async Task<Contact> GetContactByFullname(string fullname)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Get,
                accessToken,
                $"{baseUrl}/contacts?$filter=fullname eq '{fullname}'");

            var json = await response.Content.ReadAsStringAsync();
            var contact = JsonConvert.DeserializeObject<Contact>(json);

            return contact!;
        }

        public async Task<Contact> AddContact(ContactCreateRequest contactCreate)
        {
            Contact contact = null;
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();

            // Convert the Contact object to JSON
            var jsonContact = JsonConvert.SerializeObject(contactCreate);
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Post,
                accessToken,
                $"{baseUrl}/contacts",
                jsonContact);

            if (response.IsSuccessStatusCode)
            {
                var createdContactUrl = GetHeaderValue(response, "OData-EntityId");
                var response2 = await dynamicsCRM.CrmRequest(
                HttpMethod.Get,
                accessToken,
                createdContactUrl
                );
                var json = await response2.Content.ReadAsStringAsync();
                contact = JsonConvert.DeserializeObject<Contact>(json);

                return contact;

            }

            return contact;
        }

        private string GetHeaderValue(HttpResponseMessage response, string headerName)
        {
            // Check if the header exists in the response
            if (response.Headers.TryGetValues(headerName, out var headerValues))
            {
                // If the header is found, return its value
                return string.Join(",", headerValues);
            }

            // If the header is not found, return an empty string or handle the situation as needed
            return string.Empty;
        }

        public async Task<bool> UpdateContact(Guid contactid, ContactUpdateRequest contactUpdate)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();

            // Convert the Contact object to JSON
            var jsonContact = JsonConvert.SerializeObject(contactUpdate);
            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Patch,
            accessToken,
                $"{baseUrl}/contacts({contactid})",
                jsonContact);

            

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteContact(Guid contactid)
        {
            // get access token
            string accessToken = await dynamicsCRM.GetAccessTokenAsync();

            // Set xrm request params
            var response = await dynamicsCRM.CrmRequest(
                HttpMethod.Delete,
                accessToken,
                $"{baseUrl}/contacts({contactid})");

            return response.IsSuccessStatusCode;
        }
    }
}
