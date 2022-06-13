using System.Collections.Generic;
using static SanTsgBootcampProject.Domain.ApiResponseModels.OfferIdResult;

namespace SanTsgBootcampProject.Domain.ApiResponseModels
{
    public class OfferIdResult : BaseEntity<OfferBody>
    {
        public class OfferBody
        {
            public List<HotelInfos> Hotels { get; set; }
        }
        public class HotelInfos
        {
            public List<Offer> Offers { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
        }
        public class Offer
        {
            public string OfferId { get; set; }

        }
    }
}
