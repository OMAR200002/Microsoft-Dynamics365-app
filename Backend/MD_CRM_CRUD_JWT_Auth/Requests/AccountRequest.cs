namespace MD_CRM_CRUD_JWT_Auth.Requests
{
    public class AccountRequest
    {
        public string? name { get; set; } = String.Empty;
        public string? description { get; set; } = String.Empty;
        public double revenue { get; set; }
    }
}
