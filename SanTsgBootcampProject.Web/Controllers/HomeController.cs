using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SanTsgBootcampProject.Domain;
using SanTsgBootcampProject.Domain.ApiResponseModels;
using SanTsgBootcampProject.Domain.Users;
using SanTsgBootcampProject.Web.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgBootcampProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Login()
        {
            //returns Agency Login View
            return View();
        }
        /// <summary>
        /// This controller logs you in if valid login information given 
        /// </summary>
        /// <param name="userSan"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(UserSanLoginModel userSan)
        {
            //URL where to make API calls
            const string LoginUrl = "https://service.stage.paximum.com/v2/api/authenticationservice/login";
            if (ModelState.IsValid)
            {
                // HttpClient used to send requests and retrieve their responses
                var client = ApiHelper.ApiClient;
                //it serializes login info JSON file to make it understandable for API
                StringContent content = new StringContent(JsonConvert.SerializeObject(userSan), Encoding.UTF8, "application/json");
                //sent login info JSON file to related API Url and wait for a response for token information
                using var response = await client.PostAsync(LoginUrl, content);
                //if response code is between 200-299 it will read response
                if (response.IsSuccessStatusCode)
                {
                    //reads response as token result model
                    TokenResult loginDetails = await response.Content.ReadAsAsync<TokenResult>();
                    //we need only token information from the response
                    string token = loginDetails.Body.Token;
                    //createed a session to carry token for later use
                    HttpContext.Session.SetString("JWToken", token);
                    //Redirects you to the destination search controller
                    return RedirectToAction("SearchQuery", "Hotel");
                }
            }
            return View(userSan);
        }
        public IActionResult LogOff()
        {
            //it cleares all stored sessions
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
