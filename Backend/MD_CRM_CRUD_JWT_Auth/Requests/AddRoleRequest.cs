using System.ComponentModel.DataAnnotations;

namespace MD_CRM_CRUD_JWT_Auth.Requests
{
    public class AddRoleRequest
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
