using System.Net;

namespace MD_CRM_CRUD_JWT_Auth.Responses
{
    public class APIResponse
    {
        public HttpStatusCode httpStatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = null;
        public object? Result { get; set; } = null;
    }
}
