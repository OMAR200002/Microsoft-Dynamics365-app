using Azure;
using MD_CRM_CRUD_JWT_Auth.Models;
using MD_CRM_CRUD_JWT_Auth.Requests.Contact;
using MD_CRM_CRUD_JWT_Auth.Responses;
using MD_CRM_CRUD_JWT_Auth.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MD_CRM_CRUD_JWT_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private APIResponse _response;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
            _response = new();
        }


        [HttpGet("GetContacts")]
        public async Task<ActionResult<APIResponse>> GetContacts()
        {
            List<Contact> contacts = await _contactService.GetContacts();
            _response.httpStatusCode = HttpStatusCode.OK;
            _response.Result = contacts;
            return Ok(_response);
        }

        [HttpGet("GetContactById/{contactId}")]
        public async Task<ActionResult<APIResponse>> GetContactById(Guid contactId)
        {
            Contact contact = await _contactService.GetContactById(contactId);

            if (contact == null)
            {
                _response.IsSuccess = false;
                _response.httpStatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    $"Customer with id = {contactId} not found"
                };
                return BadRequest(_response);
            }

            _response.httpStatusCode=HttpStatusCode.OK;
            _response.Result = contact;
            return Ok(_response);
        }

        [HttpGet("GetContactNameById/{contactId}")]
        public async Task<IActionResult> GetContactNameById(Guid contactId)
        {
            string contact = await _contactService.GetContactFullnameById(contactId);
            return Ok(contact);
        }

        [HttpGet("GetContactByName/{fullname}")]
        public async Task<IActionResult> GetContactByName(string fullname)
        {
            Contact Contact = await _contactService.GetContactByFullname(fullname);
            return Ok(Contact);
        }

        [HttpPost("AddContact")]
        public async Task<ActionResult<APIResponse>> AddContact(ContactCreateRequest contactCreate)
        {
            if(contactCreate.gendercode != 1 && contactCreate.gendercode != 2)
            {
                _response.IsSuccess = false;
                _response.httpStatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    $"Gender Code is incorrect : 1 => Male | 2 => Female"
                };
                return BadRequest(_response);
            }
            if(contactCreate.accountrolecode < 1 && contactCreate.accountrolecode > 3)
            {
                _response.IsSuccess = false;
                _response.httpStatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    "Account role  code in incorrect : 1 => Decision Maker, 2 => Employee, 3 => Influencer"
                };
                return BadRequest(_response);
            }
            var contact =  await _contactService.AddContact(contactCreate);
            if (contact == null) 
            {
                _response.IsSuccess = false;
                _response.httpStatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    "Error while creating Contact"
                };
                return BadRequest(_response);
            }
            
            _response.httpStatusCode=HttpStatusCode.OK;
            _response.Result = contact;
            return Ok(_response);
        }

        [HttpPatch("UpdateContact/{contactId}")]
        public async Task<ActionResult<APIResponse>> UpdateContact(Guid contactId, [FromBody] ContactUpdateRequest contactUpdate)
        {
            
            var contact = await _contactService.GetContactById(contactId);
            if (contact == null)
            {
                _response.IsSuccess = false;
                _response.httpStatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string>()
                {
                    $"Contact with id = {contactId} not found"
                };
                return BadRequest(_response);
            }

            if (contactUpdate.gendercode != 1 && contactUpdate.gendercode != 2)
            {
                _response.IsSuccess = false;
                _response.httpStatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    $"Gender Code is incorrect : 1 => Male | 2 => Female"
                };
                return BadRequest(_response);
            }
            if (contactUpdate.statecode != 0 && contactUpdate.statecode != 1)
            {
                _response.IsSuccess = false;
                _response.httpStatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    "Status code in incorrect : 1 => Active, 2 => Inactive"
                };
                return BadRequest(_response);
            }
            if (contactUpdate.accountrolecode < 1 && contactUpdate.accountrolecode > 3)
            {
                _response.IsSuccess = false;
                _response.httpStatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    "Account role  code in incorrect : 1 => Decision Maker, 2 => Employee, 3 => Influencer"
                };
                return BadRequest(_response);
            }


            bool isUpdated = await _contactService.UpdateContact(contactId, contactUpdate);

            if (!isUpdated)
            {
                _response.IsSuccess = false;
                _response.httpStatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>()
                {
                    "Error while updating the contact"
                };
                return BadRequest(_response);
            }

            _response.httpStatusCode = HttpStatusCode.OK;
            _response.Result = contactUpdate;
            return Ok(_response);
        }

        [HttpDelete("DeleteContact/{contactId}")]
        public async Task<ActionResult<APIResponse>> DeleteContact(Guid contactId)
        {
            Contact contact = await _contactService.GetContactById(contactId);

            if (contact == null)
            {
                _response.IsSuccess = false;
                _response.httpStatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    $"Contact with id = {contactId} doesn't exist"
                };
                return BadRequest(_response);
            }


            bool isDeleted = await _contactService.DeleteContact(contactId);

            if (!isDeleted)
            {
                _response.IsSuccess = false;
                _response.httpStatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>()
                {
                    $"Error while deleting and contact"
                };
                return BadRequest(_response);
            }

            _response.httpStatusCode = HttpStatusCode.OK;
            _response.Result = contactId;
            return Ok(_response);
        }
    }
}
