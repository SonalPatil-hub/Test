using EmployeeManagement.Models.Common;
using EmployeeManagement.Models.Request.Employee;
using EmployeeManagement.Models.Response.Employee;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<PaginationResponse<EmployeeSearchResponse>> GetEmployeeData(EmployeeSearchRequest employeeSearchRequest);
    }
}
