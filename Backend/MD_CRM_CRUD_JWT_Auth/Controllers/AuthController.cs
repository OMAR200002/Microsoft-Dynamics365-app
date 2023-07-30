using MD_CRM_CRUD_JWT_Auth.Models;
using MD_CRM_CRUD_JWT_Auth.Requests;
using MD_CRM_CRUD_JWT_Auth.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MD_CRM_CRUD_JWT_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService= authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            return Ok(result);
        }

        [HttpPost("role")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("google")]
        public IActionResult GoogleAuth()
        {
            //var redirectUrl = Url.Action(nameof(GoogleAuthCallback), "Auth");
            var redirectUrl = "https://localhost:7133/api/Auth/google-callback";
            _authService.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, redirectUrl!);
            Console.WriteLine(redirectUrl);
            return Challenge(GoogleDefaults.AuthenticationScheme);
        }

        [AllowAnonymous]
        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleAuthCallback()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            Console.WriteLine(result.Succeeded);
            foreach (var claim in result.Principal.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }
            //if (!result.Succeeded)
            //{
            //    return BadRequest();
            //}

            var user = result.Principal;
            Console.WriteLine(user);
            return Ok(user);
        }

    }
}
