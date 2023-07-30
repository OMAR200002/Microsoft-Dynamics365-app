using MD_CRM_CRUD_JWT_Auth.Controllers;
using MD_CRM_CRUD_JWT_Auth.Models;
using MD_CRM_CRUD_JWT_Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MD_CRM_CRUD_JWT_AUTH.Test
{
    public class TestOpportunitiesController
    {
        [Fact]
        public async Task GetOpportunitys_Returns_OkResult_With_Opportunities()
        {
            // Arrange
            var mockOpportunityService = new Mock<IOpportunityService>();
            var expectedOpportunities = new List<Opportunity>
        {
           new Opportunity
            {
                opportunityid = new Guid("e90a0493-e8f0-ea11-a815-000d3a1b14a2"),
                name = "10 machines à café Airpot XL pour Alpine Ski House",
                description = "Ajout de machines à café au siège social",
                estimatedvalue = 4990,
                closeprobability = 65,
                salesstagecode = 1,
                currentsituation = "Il n’y a pas assez de machines à café pour répondre à la demande.",
                purchasetimeframe = 4
            },
            new Opportunity
            {
                opportunityid = new Guid("b052fc98-e8f0-ea11-a815-000d3a1b14a2"),
                name = "18 machines à café Airpot pour Northwind Traders",
                description = "Achat de nouvelles machines pour les bureaux",
                estimatedvalue = 30582,
                closeprobability = 93,
                salesstagecode = 1,
                currentsituation = "Les établissements n’ont aucune machine expresso.",
                purchasetimeframe = 1
            },
           
        };
            mockOpportunityService.Setup(service => service.GetOpportunities())
                .ReturnsAsync(expectedOpportunities);

            var controller = new OpportunityController(mockOpportunityService.Object);

            // Act
            var result = await controller.GetOpportunitys();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expectedOpportunities, okResult.Value);
        }

        [Fact]
        public async Task GetOpportunitys_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var mockOpportunityService = new Mock<IOpportunityService>();
            mockOpportunityService.Setup(service => service.GetOpportunities())
                .ThrowsAsync(new Exception($"Failed to retrieve opportunities. Some error message."));

            var controller = new OpportunityController(mockOpportunityService.Object);

            // Act
            var result = await controller.GetOpportunitys();

            // Assert
            Assert.IsType<ObjectResult>(result); // Expecting an ObjectResult
            var objectResult = (ObjectResult)result;
            Assert.Equal(500, objectResult.StatusCode); // Verify the status code
        }


        [Fact]
        public async Task GetOpportunitys_Returns_EmptyList()
        {
            // Arrange
            var mockOpportunityService = new Mock<IOpportunityService>();
            var expectedOpportunities = new List<Opportunity>(); // Empty list of opportunities
            mockOpportunityService.Setup(service => service.GetOpportunities())
                .ReturnsAsync(expectedOpportunities);

            var controller = new OpportunityController(mockOpportunityService.Object);

            // Act
            var result = await controller.GetOpportunitys();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Empty((IEnumerable<Opportunity>)okResult.Value);
        }

    }
}