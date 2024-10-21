using API.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace API.Controllers
{
    public class CustomersController : ApiController
    {
        private static List<Customer> customers = new List<Customer>();

        public CustomersController()
        {
            if (customers.Count<1)
            {
                // Initializing with some data
                customers.Add(new Customer { Id = 1, Name = "John Doe1", Email = "john@example.com", Phone = "123456789" });
                customers.Add(new Customer { Id = 2, Name = "Jane Doe2", Email = "jane@example.com", Phone = "987654321" });
                customers.Add(new Customer { Id = 3, Name = "Jane Doe3", Email = "jane@example.com", Phone = "987654321" });
            }
        }

        #region Get

        // GET api/customers
        public IHttpActionResult GetCustomers()
        {
            return Ok(customers.ToList());
        }

        // GET api/customers/5
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }


        #endregion

        #region Post

        // POST api/customers
        public IHttpActionResult CreateCustomer(Customer customer)
        {
            var existingCustomer = customers.FirstOrDefault(c => c.Id == customer.Id);
            if (existingCustomer != null)
            {
                return BadRequest("Customer with this ID already exists.");
            }
            customers.Add(customer);
            return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }


        #endregion


        #region Put

        [System.Web.Http.HttpPut]
        public IHttpActionResult EditCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            var existingCustomer = customers.FirstOrDefault(c => c.Id == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            return Ok(existingCustomer);
        }


        #endregion


        #region Delete
        // DELETE api/customers/5
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            customers.Remove(customer);
            return Ok();
        }
        #endregion


    }
}