using System.Collections.Generic;
using static SanTsgBootcampProject.Domain.ApiResponseModels.QueryResult;

namespace SanTsgBootcampProject.Domain.ApiResponseModels
{
    /// <summary>
    ///  This class created to turn  response into mappings from query calls to API
    /// </summary>
    public class QueryResult : BaseEntity<QueryInfo>
    {
        public class QueryInfo
        {
            public List<AllInfo> Items { get; set; }
        }

        public class AllInfo
        {
            public CityDetailedInfo City { get; set; }
            public HotelDetailedInfo Hotel { get; set; }
        }
        public class HotelDetailedInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class CityDetailedInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
