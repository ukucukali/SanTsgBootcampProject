using System.Collections.Generic;
using static SanTsgBootcampProject.Domain.ApiResponseModels.BeginTransactionResult;

namespace SanTsgBootcampProject.Domain.ApiResponseModels
{
    /// <summary>
    /// to read needed transaction details
    /// </summary>
    public class BeginTransactionResult : BaseEntity<TransactionDetails>
    {
        public class TransactionDetails
        {
            public string TransactionId { get; set; }
            public ReservationData ReservationData { get; set; }
        }
        public class ReservationData
        {
            public List<Traveller> Travellers { get; set; }
        }
        public class Traveller
        {
            public string TravellerId { get; set; }
            public bool IsLeader { get; set; }

        }


    }

}