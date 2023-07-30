using MD_CRM_CRUD_JWT_Auth.Models;
using MD_CRM_CRUD_JWT_Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MD_CRM_CRUD_JWT_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunityController : ControllerBase
    {


        private readonly IOpportunityService _opportunityService;

        public OpportunityController(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }



        [HttpGet]
        public async Task<IActionResult> GetOpportunitys()
        {
            try
            {
                List<Opportunity> result = await _opportunityService.GetOpportunities();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the retrieval process
                return StatusCode(500, "Error retrieving opportunities: " + ex.Message);
            }
        }

    }
}
