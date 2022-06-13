using System.Collections.Generic;
using System.Globalization;

namespace SanTsgBootcampProject.Domain.ApiCallModels
{
    /// <summary>
    /// Send call to API to start transaction
    /// </summary>
    public class BeginTransactionQuery
    {
        public List<string> OfferIds { get; set; }
        public string Currency { get; set; } = "EUR";
        public string Culture { get; set; } = new CultureInfo("us-US").ToString();
    }
}
