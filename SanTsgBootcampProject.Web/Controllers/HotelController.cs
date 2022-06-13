using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SanTsgBootcampProject.Domain;
using SanTsgBootcampProject.Domain.ApiCallModels;
using SanTsgBootcampProject.Domain.ApiResponseModels;
using SanTsgBootcampProject.Web.Models;
using System.Collections.Generic;
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

        [HttpGet]
        public IActionResult PriceSearch()
        {
            return View();
        }
        /// <summary>
        /// This controller gets reservation details here and transfers the result to the next step
        /// </summary>
        /// <param name="priceSearch"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PriceSearch(PriceSearchViewModel priceSearch)
        {
            if (ModelState.IsValid)
            {
                string arrivalLocationId = HttpContext.Session.GetString("CityId");
                string checkinDate = priceSearch.GetCheckinDate();

                PriceSearchQuery query = new PriceSearchQuery
                {
                    ArrivalLocations = new List<ArrivalLocation>
                    {
                         new ArrivalLocation
                         {
                             Id=arrivalLocationId
                         }
                    },
                    CheckIn = checkinDate,
                    Night = priceSearch.Night,
                    Currency = priceSearch.Currency.ToString(),
                    RoomCriteria = new List<RoomCriteria>
                    {
                        new RoomCriteria
                        {
                            Adult = priceSearch.Adult,
                            ChildAges=priceSearch.TotalChild > 0?ChildDetails(priceSearch):null
                        }
                    },
                    Nationality = priceSearch.Nationality,
                };
                string jsonQuery = JsonConvert.SerializeObject(query);
                HttpContext.Session.SetString("SearchQuery", jsonQuery);
                return RedirectToAction("HotelResult", "Hotel");
            }
            return View();
        }
        /// <summary>
        /// displays hotels according to reservation details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> HotelResult()
        {
            string reservationsDetails = HttpContext.Session.GetString("SearchQuery");
            if (reservationsDetails != null)
            {
                #region SendRequestToApi
                const string hotelInfoUrl = "https://service.stage.paximum.com/v2/api/productservice/pricesearch";
                StringContent content = new StringContent(reservationsDetails, Encoding.UTF8, "application/json");
                var client = ApiHelper.ApiClient;
                var token = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using var response = await client.PostAsync(hotelInfoUrl, content);
                #endregion
                if (response.IsSuccessStatusCode)
                {
                    PriceSearchResult info = await response.Content.ReadAsAsync<PriceSearchResult>();
                    string jsonOffer = JsonConvert.SerializeObject(info, Formatting.Indented);
                    HttpContext.Session.SetString("GetOfferId", jsonOffer);
                    return View(info);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult HotelResult(string id)
        {
            if (id != null)
                return RedirectToAction("HotelDetails", "Hotel", new { id });

            return View();
        }
        /// <summary>
        /// displays selected hotel details. You can continue booking your hatel here
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> HotelDetails(string id)
        {
            if (!id.Equals(null))
            {
                const string hotelInfoUrl = "https://service.stage.paximum.com/v2/api/productservice/getproductInfo";

                var query = new ProductDetailSearchQuery
                {
                    Product = id

                };
                StringContent content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
                var client = ApiHelper.ApiClient;
                var token = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using var response = await client.PostAsync(hotelInfoUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var test = await response.Content.ReadAsStringAsync();
                    ProductDetailsResult info = await response.Content.ReadAsAsync<ProductDetailsResult>();

                    if (info != null)
                    {
                        return View(info);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult HotelDetails(int id)
        {
            if (!id.Equals(null))
            {
                //TempData["HotelId"] = id;
                return RedirectToAction("BeginTransaction", "Booking", new { id });
            }
            return BadRequest("Something went wrong");
        }






        //adds children to pricesearch model
        public List<int> ChildDetails(PriceSearchViewModel child)
        {

            var childAges = new List<int>();

            if (child.Child1.HasValue)
            {
                childAges.Add((int)child.Child1);
            }

            if (child.Child2.HasValue)
            {
                childAges.Add((int)child.Child2);
            }

            if (child.Child3.HasValue)
            {
                childAges.Add((int)child.Child3);
            }

            if (child.Child4.HasValue)
            {
                childAges.Add((int)child.Child4);
            }

            return childAges;
        }

    }
}
