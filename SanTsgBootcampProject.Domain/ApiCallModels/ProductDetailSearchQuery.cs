using System.Globalization;

namespace SanTsgBootcampProject.Domain.ApiCallModels
{
    /// <summary>
    /// helps us search according to given query when to call API
    /// </summary>
    public class ProductDetailSearchQuery
    {
        public int ProductType { get; set; } = 2;
        public int OwnerProvider { get; set; } = 2;
        public string Product { get; set; }
        public string Culture { get; set; } = new CultureInfo("us-US").ToString();
    }
}
