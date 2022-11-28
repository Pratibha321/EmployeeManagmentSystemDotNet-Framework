using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDeptProject.Models.Models
{
    public class Department
    {
        [Key]
        
        [Display(Name ="Department Id")]
        public int DeptId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Department name can only contain characters")]
        [StringLength(20, ErrorMessage = "Length Should Be 20 Only")]
        [Display(Name ="Department Name")]
        public string DeptName { get; set; }
    }
}
