using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Entities.Common
{
    public class BaseEntity
    {
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string CreatedById {  get; set; }
        [Required]
        [Column(TypeName = "DATETIME")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string ModifiedById {  get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime ModifiedDate {  get; set; }
    }
}
