using Microsoft.AspNetCore.Mvc;
using MD_CRM_CRUD_JWT_Auth.Models;
using MD_CRM_CRUD_JWT_Auth.Requests;
using MD_CRM_CRUD_JWT_Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MD_CRM_CRUD_JWT_Auth.Controllers
{
    //[Authorize(Roles = "Admin")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }


        [HttpGet("GetAccounts")]
        public async Task<IActionResult> GetAccounts()
        {
            List<Account> accounts = await accountService.GetAccounts();
            return Ok(accounts);
        }

        [HttpGet("GetAccountById/{accountid}")]
        public async Task<IActionResult> GetAccountById(Guid accountid)
        {
            Account account = await accountService.GetAccountById(accountid);
            return Ok(account);
        }

        [HttpGet("GetAccountNameById/{accountid}")]
        public async Task<IActionResult> GetAccountNameById(Guid accountid)
        {
            string account = await accountService.GetAccountNameById(accountid);
            return Ok(account);
        }

        [HttpGet("GetAccountByName/{name}")]
        public async Task<IActionResult> GetAccountByName(string name)
        {
            Account account = await accountService.GetAccountByName(name);
            return Ok(account);
        }

        [HttpPost("AddAccount")]
        public async Task<IActionResult> AddAccount(AccountRequest account)
        {
            bool isAdded = await accountService.AddAccount(account);
            return Ok(isAdded);
        }

        [HttpPatch("UpdateAccount/{accountid}")]
        public async Task<IActionResult> UpdateAccount(Guid accountid, [FromBody] AccountRequest account)
        {
            bool isUpdated = await accountService.UpdateAccount(accountid, account);
            return Ok(isUpdated);
        }

        [HttpPatch("status/{accountid}")]
        public async Task<IActionResult> ToggleStatus(Guid accountid, [FromBody] StatusRequest statusRequest)
        {
            bool isUpdated = await accountService.ToggleStatus(accountid, statusRequest);
            return Ok(isUpdated);
        }

        [HttpDelete("DeleteAccount/{accountid}")]
        public async Task<IActionResult> DeleteAccount(Guid accountid)
        {
            bool isDeleted = await accountService.DeleteAccount(accountid);
            return Ok(isDeleted);
        }
        
        [HttpPost("DeleteAccounts")]
        public async Task<IActionResult> DeleteAccounts(string[] accountsIds)
        {
            await accountService.DeleteAccounts(accountsIds);
            return Ok();
        }
    }
}
