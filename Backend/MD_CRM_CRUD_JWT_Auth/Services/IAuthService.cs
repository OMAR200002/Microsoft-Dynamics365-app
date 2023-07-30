using MD_CRM_CRUD_JWT_Auth.Models;
using MD_CRM_CRUD_JWT_Auth.Requests;
using MD_CRM_CRUD_JWT_Auth.Responses;
using Microsoft.AspNetCore.Authentication;

namespace MD_CRM_CRUD_JWT_Auth.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest model);
        Task<AuthResponse> LoginAsync(LoginRequest model);
        public Task<string> AddRoleAsync(AddRoleRequest model);
        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
    }
}
