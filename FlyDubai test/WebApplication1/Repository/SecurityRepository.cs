
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models.Common;
using EmployeeManagement.Models.Request.Security;

namespace EmployeeManagement.Repository
{
    public class SecurityRepository : ISecurityRepository
    {
        public async Task<BaseResponse<User>> GetUser(LoginRequest request)
        {
            var result = new BaseResponse<User>();
            // Searches for a user in a predefined user store that matches both username and password.
            var user = UserStore.Users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            result.Result = user;
            return result;
        }
    }
}
