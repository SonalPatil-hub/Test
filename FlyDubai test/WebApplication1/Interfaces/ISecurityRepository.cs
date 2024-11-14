using EmployeeManagement.Models.Common;
using EmployeeManagement.Models.Request.Security;

namespace EmployeeManagement.Interfaces
{
    public interface ISecurityRepository
    {
        public Task<BaseResponse<User>> GetUser(LoginRequest request);
    }
}
