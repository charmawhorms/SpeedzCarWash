using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SpeedzCarWashClient.Models;
using System.Net.Http;
using System.Text;
using System.Net.Http.Json;

namespace SpeedzCarWashClient.Controllers
{
    public class BookingController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5268/booking");
        private readonly HttpClient _client;

        public BookingController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }



        [HttpGet]
        public IActionResult Index()
        {
            List<Booking> bookingList = new List<Booking>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                bookingList = JsonConvert.DeserializeObject<List<Booking>>(data)!;
            }
            return View(bookingList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            BookingVM bookingVM = new BookingVM();
            var response = _client.GetAsync($"{_client.BaseAddress}/Upsert").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content?.ReadAsStringAsync().Result;
                bookingVM = JsonConvert.DeserializeObject<BookingVM>(data);
            }
            return View(bookingVM);
        }

        [HttpPost]
        public IActionResult Create(Booking model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Booking Created";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Booking booking = new Booking();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    booking = JsonConvert.DeserializeObject<Booking>(data);
                }
                return View(booking);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }


        }

        [HttpPost]
        public IActionResult Edit(Booking model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Booking details updated";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Booking booking = new Booking();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    booking = JsonConvert.DeserializeObject<Booking>(data);
                }
                return View(booking);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Booking details deleted";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }


    }
}
