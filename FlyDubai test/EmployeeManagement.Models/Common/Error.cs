
using System.Net;

namespace EmployeeManagement.Models.Common
{
    public class Error
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage {  get; set; }
    }
}
