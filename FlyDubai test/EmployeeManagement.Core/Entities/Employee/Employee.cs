
using EmployeeManagement.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Core.Entities.Employee
{
    [Table("Employee")]
    public class Employee: BaseEntity
    {
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string EmployeeId { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(35)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(35)")]
        public string LastName { get; set; }
        [Column(TypeName = "INT")]
        public int EmpDeptId { get; set; }
        [Required]
        [Column(TypeName = "DATETIME")]
        public DateTime Doj { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string Email { get; set; }
        [Column(TypeName = "VARCHAR(500)")]
        public string Address { get; set; }
        [Column(TypeName = "VARCHAR(20)")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime ConfirmationDate { get; set; }
    }
}
