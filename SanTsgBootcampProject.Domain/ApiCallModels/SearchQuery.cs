using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SanTsgBootcampProject.Domain.ApiCallModels
{
    /// <summary>
    /// This class created to get correct query information from the query search
    /// </summary>
    public class SearchQuery
    {
        public string ProductType { get; set; } = "2";
        [Required, MinLength(4)]
        public string Query { get; set; }
        public string Culture { get; set; } = new CultureInfo("us-US").ToString();
    }
}
