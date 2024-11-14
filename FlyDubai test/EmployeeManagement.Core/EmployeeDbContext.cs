using EmployeeManagement.Core.Entities.Employee;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Core
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> EmployeeDataSet { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}
