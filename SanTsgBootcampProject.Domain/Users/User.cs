using System;
using System.ComponentModel.DataAnnotations;

namespace SanTsgBootcampProject.Domain.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "User name cannot be longer then 25 character")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

    }
}





