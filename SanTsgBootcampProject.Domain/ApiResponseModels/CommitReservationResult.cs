using static SanTsgBootcampProject.Domain.ApiResponseModels.CommitReservationResult;

namespace SanTsgBootcampProject.Domain.ApiResponseModels
{
    public class CommitReservationResult : BaseEntity<CommitReservation>
    {
        /// <summary>
        /// Read reservation confirmation details from  API
        /// </summary>
        public class CommitReservation
        {
            public string ReservationNumber { get; set; }
            public string EncryptedReservationNumber { get; set; }
            public string TransactionId { get; set; }
        }
    }
}
