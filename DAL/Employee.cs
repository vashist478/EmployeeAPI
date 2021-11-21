using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class Employee
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter employee code")]
        [Column(TypeName = "varchar(80)")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage ="Please enter employee name")]
        [Column(TypeName = "varchar(250)")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Please enter EmployeeEmail")]
        [Column(TypeName = "varchar(200)")]
        public string EmployeeEmail { get; set; }

        public bool IsActive { get; set; } = false;



    }
}
