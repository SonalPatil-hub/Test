using EmployeeManagement.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Request.Employee
{
    public class EmployeeSearchRequest: PaginationRequest
    {
        [MaxLength(15)]
        public string EmployeeId { get; set; }
        [MaxLength(35)]
        public string FirstName { get; set; }
        [MaxLength(35)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string EmpDeptName {  get; set; }

        [Required(ErrorMessage = "StartDate is required.")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "EndDate is required.")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate.Value <= StartDate.Value)
            {
                yield return new ValidationResult("End date must be greater than the start date.", new[] { "EndDate" });
            }
        }
    }
}
