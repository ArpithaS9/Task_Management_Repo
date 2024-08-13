using System.ComponentModel.DataAnnotations;

namespace Task_Mangement.Models
{
    public class User
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(30, ErrorMessage = "First name cannot be longer than 30 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(30, ErrorMessage = "Last name cannot be longer than 30 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address shuld be unique.")]
        public string Email { get; set; }

        [RegularExpression("Admin|Manager|Employee", ErrorMessage = "Role must be Admin, Manager, or Employee.")]
        public string Roles { get; set; }
        
    }
}
