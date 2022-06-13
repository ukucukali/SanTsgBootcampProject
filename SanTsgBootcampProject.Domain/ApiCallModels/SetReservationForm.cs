using System.ComponentModel.DataAnnotations;

namespace SanTsgBootcampProject.Domain.ApiCallModels
{
    /// <summary>
    /// to control user typed inputs 
    /// </summary>
    public class SetReservationForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string IdentityNumber { get; set; }
    }
}
