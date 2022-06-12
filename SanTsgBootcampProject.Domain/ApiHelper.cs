using System.Net.Http;

namespace SanTsgBootcampProject.Domain
{
    /// <summary>
    /// This creates a new HTTP client for API. It is static because we don't want to create a new client every time. Main goal is to use the same instance   
    /// </summary>
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; } = new HttpClient();

    }
}
