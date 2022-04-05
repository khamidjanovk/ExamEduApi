using MyEdu.Service.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MyEdu.Service.DTOs
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [StringLength(50,
            ErrorMessage = "Must be between 5 and 50 characters",
            MinimumLength = 5)]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(255,
            ErrorMessage = "Must be between 8 and 255 characters",
            MinimumLength = 8)]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [PhoneNumber]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(30,
            ErrorMessage = "Must be between 8 and 30 characters",
            MinimumLength = 8)]
        public string Username { get; set; }
    }
}