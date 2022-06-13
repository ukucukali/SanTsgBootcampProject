using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SanTsgBootcampProject.Data.Repositories.Interfaces;
using SanTsgBootcampProject.Domain;
using SanTsgBootcampProject.Domain.ApiCallModels;
using SanTsgBootcampProject.Domain.ApiResponseModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgBootcampProject.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// this beggins transaction and get transaction id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> BeginTransaction(int id)
        {
            const string hotelInfoUrl = "https://service.stage.paximum.com/v2/api/bookingservice/begintransaction";
            if (!id.Equals(null))
            {
                //call GetOfferId method
                string offerId = GetOfferId(id);

                BeginTransactionQuery beginTransaction = new BeginTransactionQuery
                {
                    OfferIds = new List<string>
                    {
                        offerId
                    }
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(beginTransaction), Encoding.UTF8, "application/json");
                var client = ApiHelper.ApiClient;
                var token = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using var response = await client.PostAsync(hotelInfoUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var test = await response.Content.ReadAsStringAsync();
                    BeginTransactionResult info = await response.Content.ReadAsAsync<BeginTransactionResult>();

                    if (info.Body != null)
                    {
                        string transactionId = info.Body.TransactionId.ToString();
                        TempData["TravellerCount"] = info.Body.ReservationData.Travellers.Count;
                        TempData["TransactionBeggins"] = "Transaction Beggins";
                        HttpContext.Session.SetString("TransactionId", transactionId);
                        return RedirectToAction("SetTransaction", "Booking");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult SetTransaction()
        {

            return View();
        }
        /// <summary>
        /// set traveller details to API
        /// </summary>
        /// <param name="setReservation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SetTransaction(List<SetReservationForm> setReservation)
        {
            const string hotelInfoUrl = "https://service.stage.paximum.com/v2/api/bookingservice/setreservationinfo";
            string transactionId = HttpContext.Session.GetString("TransactionId");
            if (setReservation.Count > 0 && transactionId != null)
            {
                if (ModelState.IsValid)
                {
                    List<Traveller> travelers = new List<Traveller>();

                    for (int i = 0; i < setReservation.Count; i++)
                    {

                        travelers.Add(new Traveller()
                        {
                            TravellerId = (i + 1).ToString(),
                            IsLeader = i == 0 ? true : false,
                            Name = setReservation[i].Name,
                            Surname = setReservation[i].Surname,
                            IdentityNumber = setReservation[i].IdentityNumber,
                            Nationality = new Nationality()
                            {
                                TwoLetterCode = "DE"
                            },
                            Address = new Addresss()
                            {
                                Phone = setReservation[i].Phone,
                                Email = setReservation[i].Email
                            }
                        });
                    }

                    SetReservationInfoQuery query = new SetReservationInfoQuery
                    {
                        TransactionId = transactionId,
                        Travellers = travelers
                    };
                    StringContent content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
                    var client = ApiHelper.ApiClient;
                    var token = HttpContext.Session.GetString("JWToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    using var response = await client.PostAsync(hotelInfoUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        //var test = await response.Content.ReadAsStringAsync();
                        HttpContext.Session.SetString("SuccesfullCommit", transactionId);
                        return RedirectToAction("CommitTransaction", "Booking");
                    }
                }
            }
            return View();
        }

        /// <summary>
        /// Commits the reservation and return reservation confirmation number
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CommitTransaction()
        {
            const string hotelInfoUrl = "https://service.stage.paximum.com/v2/api/bookingservice/committransaction";
            string transactionId = HttpContext.Session.GetString("SuccesfullCommit");
            if (transactionId != null)
            {
                CommitTransactionQuery query = new CommitTransactionQuery
                {
                    TransactionId = transactionId
                };
                StringContent content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
                var client = ApiHelper.ApiClient;
                var token = HttpContext.Session.GetString("JWToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using var response = await client.PostAsync(hotelInfoUrl, content);
                if (response.IsSuccessStatusCode)
                {

                    CommitReservationResult info = await response.Content.ReadAsAsync<CommitReservationResult>();
                    ConfirmationResult confirmationResult = new ConfirmationResult
                    {
                        ReservationNumber = info.Body.ReservationNumber,
                        EncryptedReservationNumber = info.Body.EncryptedReservationNumber,
                        TransactionId = info.Body.TransactionId
                    };
                    _unitOfWork.ReservatioConfirmationDetails.Add(confirmationResult);
                    _unitOfWork.Save();
                    return View(info);
                }
            };
            return View();

        }
        [HttpPost, ActionName("CommitTransaction")]
        public IActionResult ClearSessions()
        {
            return RedirectToAction("LogOff", "Home");
        }

        /// <summary>
        /// it return offer id to begin transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetOfferId(int id)
        {
            string hotelsId;
            string offerId;
            string jsonQuery = HttpContext.Session.GetString("GetOfferId");
            OfferIdResult offerIdResult = JsonConvert.DeserializeObject<OfferIdResult>(jsonQuery);
            foreach (var hotel in offerIdResult.Body.Hotels.Where(x => x.Id == id.ToString()))
            {
                hotelsId = hotel.Id;
                foreach (var offer in hotel.Offers)
                {
                    offerId = offer.OfferId;
                    return offerId;
                }
            }
            return "Something went wrong";

        }


    }
}
