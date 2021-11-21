using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        public string Password { get; set; }
    }
}
