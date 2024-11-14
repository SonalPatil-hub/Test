
namespace EmployeeManagement.Models.Response.Employee
{
    public class EmployeeSearchResponse
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmpDeptName { get; set; }
        public DateTime Doj { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ConfirmationDate { get; set; }
    }
}
