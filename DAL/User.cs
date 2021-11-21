using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter full name")]
        [Column(TypeName = "varchar(70)")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Column(TypeName = "varchar(300)")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        [RegularExpression("^(?=.{8,})(?=.*[a-z]{2})(?=.*[A-Z])(?=.*[@#$%^&+*!=]).*$", ErrorMessage = "Passwords must be minimum 8 characters at least 1 special characters, 1 upper case, 2 lower case")]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }


    }
}
