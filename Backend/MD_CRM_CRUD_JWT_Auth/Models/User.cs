﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MD_CRM_CRUD_JWT_Auth.Models
{
    public class User : IdentityUser
    {
        [MaxLength(50)]
        public required string Firstname { get; set; } = String.Empty;
        [MaxLength(50)]
        public required string Lastname { get; set; } = String.Empty;
    }
}