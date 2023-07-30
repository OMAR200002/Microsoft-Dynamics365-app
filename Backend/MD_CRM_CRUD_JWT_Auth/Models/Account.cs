namespace MD_CRM_CRUD_JWT_Auth.Models
{
    public class Account
    {
        public Guid accountid { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public double? revenue { get; set; } = 0.0;
        public int? statecode { get; set; } = 1;
    }
}
