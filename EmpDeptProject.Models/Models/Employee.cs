using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDeptProject.Models.Models
{
   public class Employee
    {
        [Key]
        [Display(Name ="Employee Id")]
        public int EmpId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Department name can only contain characters")]
        [StringLength(20, ErrorMessage = "Length Should Be 20 Only")]
        [Display(Name = "Employee Name")]
        public string EmpName { get; set; }
        [Display(Name = "Employee Salary")]
        public int EmpSalary { get; set; }

        [Display(Name ="Department Id")]
        public int DeptId { get; set; }

        [ForeignKey("DeptId")]
        public Department? Departments { get; set; }

    }
}
