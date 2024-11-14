using EmployeeManagement.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Core.Entities.Employee
{
    public class Department:BaseEntity
    {
        [Required]
        [Key]
        [Column(TypeName ="INT")]
        public int DeptId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string DeptName { get; set; }
    }
}
