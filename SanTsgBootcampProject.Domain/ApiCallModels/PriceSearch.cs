using System.Collections.Generic;
using System.Globalization;

namespace SanTsgBootcampProject.Domain.ApiCallModels
{
    /// <summary>
    /// when need to get prices from API. this model is used to call API correctly
    /// </summary>
    public class PriceSearch
    {
        public bool CheckAllotment { get; set; } = true;
        public bool CheckStopSale { get; set; } = true;
        public bool GetOnlyDiscountedPrice { get; set; } = false;
        public bool GetOnlyBestOffers { get; set; } = true;
        public int ProductType { get; set; } = 2;
        public List<ArrivalLocation> ArrivalLocations { get; set; }
        public List<RoomCriteria> RoomCriteria { get; set; }
        public string Nationality { get; set; } = "DE";
        public string CheckIn { get; set; }
        public int Night { get; set; }
        public string Currency { get; set; } = "EUR";
        public string Culture { get; set; } = new CultureInfo("us-US").ToString();

    }

    public class ArrivalLocation
    {
        public string Id { get; set; }
        public int Type { get; set; } = 2;
    }

    public class RoomCriteria
    {
        public int Adult { get; set; }
        public List<int> ChildAges { get; set; }
    }
}
