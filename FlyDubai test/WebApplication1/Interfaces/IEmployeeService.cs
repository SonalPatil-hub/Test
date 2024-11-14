using EmployeeManagement.Models.Common;
using EmployeeManagement.Models.Request.Employee;
using EmployeeManagement.Models.Response.Employee;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeService
    {
        public Task<PaginationResponse<EmployeeSearchResponse>> SearchEmployee(EmployeeSearchRequest employeeSearchRequest);
    }
}
