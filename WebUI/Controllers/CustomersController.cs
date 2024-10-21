using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _localHostUrl= "https://localhost:44395/api/Customers";

        public CustomersController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/");
        }

        #region Index

        // GET: Customers
        public ActionResult Index()
        {
            var response = _httpClient.GetAsync(_localHostUrl).Result;
            var customers = JsonConvert.DeserializeObject<List<Customer>>(response.Content.ReadAsStringAsync().Result);
            return View(customers);
        }

        #endregion


        #region Create

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(customer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_localHostUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }

        #endregion

        #region Delete

        // DELETE: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _httpClient.GetAsync(_localHostUrl + "/" + id).Result;
            var customer = JsonConvert.DeserializeObject<Customer>(response.Content.ReadAsStringAsync().Result);
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var response = _httpClient.DeleteAsync(_localHostUrl + "/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Record deleted successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        #endregion

        #region Edit

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _httpClient.GetAsync(_localHostUrl + "/" + id).Result;
            var customer = JsonConvert.DeserializeObject<Customer>(response.Content.ReadAsStringAsync().Result);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(customer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(_localHostUrl + "/" + customer.Id, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }

        #endregion





    }
}