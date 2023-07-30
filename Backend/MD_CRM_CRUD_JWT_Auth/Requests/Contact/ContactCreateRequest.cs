namespace MD_CRM_CRUD_JWT_Auth.Requests.Contact
{
    public class ContactCreateRequest
    {
        public string firstname { get; set; } = String.Empty;
        public string lastname { get; set; } = String.Empty;
        public string emailaddress1 { get; set; } = String.Empty;
        public string? jobtitle { get; set; } 
        public Int32? gendercode { get; set; }
        /// <summary>
        /// Select the contact's role within the company or sales process, such as decision maker, employee, or influencer.
        /// 1 => Decision Maker, 2 => Employee, 3 => Influencer
        /// </summary>
        public int? accountrolecode { get; set; }
    }
}
