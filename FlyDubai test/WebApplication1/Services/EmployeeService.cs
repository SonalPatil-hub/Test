using EmployeeManagement.Interfaces;
using EmployeeManagement.Models.Common;
using EmployeeManagement.Models.Request.Employee;
using EmployeeManagement.Models.Response.Employee;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<PaginationResponse<EmployeeSearchResponse>> SearchEmployee(EmployeeSearchRequest employeeSearchRequest)
        {           
           return await _employeeRepository.GetEmployeeData(employeeSearchRequest);
        }
    }
}
