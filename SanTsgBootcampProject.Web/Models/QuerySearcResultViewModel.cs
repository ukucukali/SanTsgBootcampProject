using SanTsgBootcampProject.Domain.ApiCallModels;
using SanTsgBootcampProject.Domain.ApiResponseModels;

namespace SanTsgBootcampProject.Web.Models
{
    public class QuerySearcResultViewModel
    {
        public QuerySearch HotelSearchQuery { get; set; }
        public QueryResult HotelResults { get; set; }

    }
}
