﻿namespace MD_CRM_CRUD_JWT_Auth.Helpers
{
    public class JWT
    {
        public string Key { get; set; } = String.Empty;
        public string Issuer { get; set; } = String.Empty;
        public string Audience { get; set; } = String.Empty;
        public double DurationInDays { get; set; }
    }
}
