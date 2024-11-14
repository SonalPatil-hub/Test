
using EmployeeManagement.Models.Common;
using EmployeeManagement.Models.Request.Security;

namespace EmployeeManagement.Interfaces
{
    public interface ISecurityService
    {
        public Task<BaseResponse<string>> Login(LoginRequest request);
    }
}
