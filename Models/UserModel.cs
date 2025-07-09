using System.ComponentModel.DataAnnotations;

namespace QuizManagement.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Invalid Moblie Number")]
        public string Mobile { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }
    }

    public class UserDropDownModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }

    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserRegistrationModel
    {
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }


        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }


        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }


        [Required]
        [StringLength(10, ErrorMessage = "Invalid Moblie Number")]
        public string Mobile { get; set; }
    }
}