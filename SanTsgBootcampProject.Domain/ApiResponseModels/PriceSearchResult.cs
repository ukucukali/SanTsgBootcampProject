using System.Collections.Generic;
using static SanTsgBootcampProject.Domain.ApiResponseModels.PriceSearchResult;

namespace SanTsgBootcampProject.Domain.ApiResponseModels
{
    /// <summary>
    /// this model helps us diplay available hotels 
    /// </summary>
    public class PriceSearchResult : BaseEntity<HotelList>
    {
        public class HotelList
        {
            public List<Hotel> Hotels { get; set; }
        }
        public class Hotel
        {
            public float Stars { get; set; }
            public double Rating { get; set; }
            public City City { get; set; }
            public List<Offer> Offers { get; set; }
            public string ThumbnailFull { get; set; } = "/img/hotelNotfound.jpg";
            public string Id { get; set; }
            public string Name { get; set; }
        }
        public class Offer
        {
            public int Night { get; set; }
            public string OfferId { get; set; }
            public Price Price { get; set; }
        }
        public class City
        {
            public string Name { get; set; }
        }
        public class Price
        {
            public double Amount { get; set; }
            public string Currency { get; set; }
        }
    }
}
