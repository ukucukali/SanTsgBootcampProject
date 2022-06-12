using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SanTsgBootcampProject.Domain;
using SanTsgBootcampProject.Domain.ApiResponseModels;
using SanTsgBootcampProject.Web.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgBootcampProject.Web.Controllers
{
    public class HotelController : Controller
    {

        /// <summary>
        /// This controller searches cities if valid query search details given
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SearchQuery(QuerySearcResultViewModel searchQuery)
        {
            // HttpClient used to send requests and retrieve their responses
            const string hotelInfoUrl = "https://service.stage.paximum.com/v2/api/productservice/getarrivalautocomplete";
            if (searchQuery.HotelResults != null || searchQuery.HotelSearchQuery != null && searchQuery.HotelSearchQuery.Query != null)
            {
                if (ModelState.IsValid)
                {
                    //it serializes login info JSON file to make it understandable for API
                    StringContent content = new StringContent(JsonConvert.SerializeObject(searchQuery.HotelSearchQuery), Encoding.UTF8, "application/json");
                    // HttpClient used to send requests and retrieve their responses
                    var client = ApiHelper.ApiClient;
                    //retrieve token from session
                    var token = HttpContext.Session.GetString("JWToken");
                    //Adding Authorization to header section
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //sent login info JSON file to related API Url and wait for a response for token information
                    using HttpResponseMessage response = await client.PostAsync(hotelInfoUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        //Reads response as given model
                        QueryResult info = await response.Content.ReadAsAsync<QueryResult>();
                        //Adds retrieved result to view model
                        QuerySearcResultViewModel result = new QuerySearcResultViewModel
                        {
                            HotelResults = info
                        };
                        if (result.HotelResults != null)
                            return View(result);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult SearchQuery(string id)
        {
            if (id != null)
            {
                HttpContext.Session.SetString("CityId", id);
                return RedirectToAction("PriceSearch", "Hotel");
            }
            return View();
        }
    }
}
