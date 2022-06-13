using System.ComponentModel.DataAnnotations;

namespace SanTsgBootcampProject.Domain
{
    /// <summary>
    /// database representation of reservation details
    /// </summary>
    public class ConfirmationResult
    {
        [Key]
        public int Id { get; set; }
        public string ReservationNumber { get; set; }
        public string EncryptedReservationNumber { get; set; }
        public string TransactionId { get; set; }
    }
}
