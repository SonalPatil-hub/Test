using EmployeeManagement.Interfaces;
using EmployeeManagement.Repository;
using EmployeeManagement.Services;

namespace EmployeeManagement
{
    public static class RegisterServiceDependency
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            //Register all applicaion services
            builder.Services.AddTransient(typeof(ISecurityService), typeof(SecurityService));
            builder.Services.AddTransient(typeof(ISecurityRepository), typeof(SecurityRepository));
            builder.Services.AddTransient(typeof(IEmployeeService), typeof(EmployeeService));
            builder.Services.AddTransient(typeof(IEmployeeRepository), typeof(EmployeeRepository));
        }
    }
}
