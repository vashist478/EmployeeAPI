using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeClientApp.Controllers
{
    public class EmployeeDetailsController : Controller
    {

        IConfiguration _config;
        HttpClient _client;
        Uri _baseAddress;
        IMemoryCache _cache;

        public EmployeeDetailsController(IConfiguration config, IMemoryCache cache)
        {

            _config = config;
            _baseAddress = new Uri("http://localhost:57387");
            _client = new HttpClient();
            _client.BaseAddress = _baseAddress;
            _cache = cache;
        }

        /// <summary>
        /// SK: Display employee records
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            List<DAL.Employee> reservationList = new List<DAL.Employee>();
            using (var httpClient = new HttpClient())
            {
                string ApiURL = _client.BaseAddress + "values";
                //httpClient.GetAsync("http://localhost:57387/values").Result
                using (var response = httpClient.GetAsync(ApiURL).Result)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<DAL.Employee>>(apiResponse);
                }
            }
            return View(reservationList);
        }


        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// SK: Create Employee record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                // string strData = JsonSerializer.Serialize(model);
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = _client.PostAsync(_client.BaseAddress + "api/values/Add", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        /// <summary>
        /// SK: Bind employee form for edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            Employee model = new Employee();
            var response = _client.GetAsync(_client.BaseAddress + "api/values/get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<Employee>(data);
            }
            return View("Create", model);
        }


        /// <summary>
        /// SK: Update Employee record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                //  string strData = JsonSerializer.Serialize(model);
                string strData = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                var response = _client.PutAsync(_client.BaseAddress + "api/values/update/" + model.Id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            
            return View("Create", model);
        }

        /// <summary>
        /// SK: Delete Employee record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                string ApiURL = _client.BaseAddress + "values/Delete/" + id;
                using (var response = httpClient.DeleteAsync(ApiURL).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
