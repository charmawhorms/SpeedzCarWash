using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpeedzCarWashClient.Models;
using System.Text;

namespace SpeedzCarWashClient.Controllers
{
    public class WasherController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5268/washer");
        private readonly HttpClient _client;

        public WasherController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }



        [HttpGet]
        public IActionResult Index()
        {
            List<Washer> washerList = new List<Washer>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                washerList = JsonConvert.DeserializeObject<List<Washer>>(data)!;
            }
            return View(washerList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Washer model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Washer Created";
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
                Washer washer = new Washer();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    washer = JsonConvert.DeserializeObject<Washer>(data);
                }
                return View(washer);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }


        }

        [HttpPost]
        public IActionResult Edit(Washer model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Washer details updated";
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
                Washer washer = new Washer();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    washer = JsonConvert.DeserializeObject<Washer>(data);
                }
                return View(washer);
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
                    TempData["successMessage"] = "Washer details deleted";
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
