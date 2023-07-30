using MD_CRM_CRUD_JWT_Auth.Models;

namespace MD_CRM_CRUD_JWT_Auth.Services
{
    public interface IOpportunityService
    {
        // Retrive All Opportunitys from DMS 365 .
        public Task<List<Opportunity>> GetOpportunities();
    


    }
}
