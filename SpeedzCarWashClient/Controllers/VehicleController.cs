using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpeedzCarWashClient.Models;
using System.Text;

namespace SpeedzCarWashClient.Controllers
{
    public class VehicleController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5268/vehicle");
        private readonly HttpClient _client;

        public VehicleController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }



        [HttpGet]
        public IActionResult Index()
        {
            List<Vehicle> vehicleList = new List<Vehicle>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                vehicleList = JsonConvert.DeserializeObject<List<Vehicle>>(data);
            }
            return View(vehicleList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Vehicle model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Vehicle Created";
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
                Vehicle vehicle = new Vehicle();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    vehicle = JsonConvert.DeserializeObject<Vehicle>(data);
                }
                return View(vehicle);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }


        }

        [HttpPost]
        public IActionResult Edit(Vehicle model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Vehicle details updated";
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
                Vehicle vehicle = new Vehicle();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    vehicle = JsonConvert.DeserializeObject<Vehicle>(data);
                }
                return View(vehicle);
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
                    TempData["successMessage"] = "Vehicle details deleted";
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
