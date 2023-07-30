using MD_CRM_CRUD_JWT_Auth.CRM;
using MD_CRM_CRUD_JWT_Auth.Models;
using Newtonsoft.Json;

namespace MD_CRM_CRUD_JWT_Auth.Services
{
    public class OpportinityService : IOpportunityService
    {
        private readonly DynamicsCRM CRM;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;

        public OpportinityService(DynamicsCRM crmConfig, IConfiguration configuration)
        {
            _configuration = configuration;
            CRM = crmConfig;
            baseUrl = $"{_configuration.GetValue<string>("DynamicsCrmSettings:Scope")}/api/data/v9.2";
        }




        // Implement the GetOpportunities method
        public async Task<List<Opportunity>> GetOpportunities()
        {
            try
            {
                string accessToken = await CRM.GetAccessTokenAsync();
     
                var response = await CRM.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                    $"{baseUrl}/opportunities");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve opportunities. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.value?.ToObject<List<Opportunity>>();

                // Handle the case where the data is not coming
                if (op == null)
                {
                    return new List<Opportunity>();
                }

                return op;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving opportunities: {ex.Message}");
                return new List<Opportunity>();
            }
        }
   
        
    
    
    }
}
