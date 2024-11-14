
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Common
{
    public class PaginationRequest:BaseRequest
    {
        public int Skip {  get; set; }
        public int Take { get; set; }
        public string OrderByName { get; set; }
        public string OrderByType { get; set; }
    }
}
