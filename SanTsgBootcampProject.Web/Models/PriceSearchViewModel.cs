using System;
using System.ComponentModel.DataAnnotations;

namespace SanTsgBootcampProject.Web.Models
{
    /// <summary>
    ///  this model using to get reservation details correctly
    /// </summary>
    public class PriceSearchViewModel
    {
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public int Night { get; set; }
        public string Currency { get; set; }
        [Required]
        public int Adult { get; set; }
        public int? TotalChild { get; set; }
        public int? Child1 { get; set; }
        public int? Child2 { get; set; }
        public int? Child3 { get; set; }
        public int? Child4 { get; set; }
        [Required]
        public string Nationality { get; set; }
        public string GetCheckinDate()
        {

            return CheckIn.ToString("yyyy-MM-dd");

        }
    }
}
