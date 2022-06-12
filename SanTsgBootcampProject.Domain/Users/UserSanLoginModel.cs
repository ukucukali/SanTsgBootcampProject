using System.ComponentModel.DataAnnotations;

namespace SanTsgBootcampProject.Domain.Users
{
    /// <summary>
    /// This class created to get correct login information from the login form
    /// </summary>
    public class UserSanLoginModel
    {
        [Required(ErrorMessage = "Agency Id is required")]
        public string Agency { get; set; }
        [Required(ErrorMessage = "User name is required")]
        public string User { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }
    }
}
