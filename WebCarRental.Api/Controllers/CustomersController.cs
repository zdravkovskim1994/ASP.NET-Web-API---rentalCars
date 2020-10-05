using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebCarRental.Core.Models;
using WebCarRental.Core.Services;
using WebCarRental.Core.Validation;

namespace WebCarRental.Api.Controllers
{
    [Authorize]
    public class CustomersController : ApiController
    {
        private readonly ICustomerService service;

        public CustomersController(ICustomerService service)
        {
            this.service = service;
        }

        [Authorize(Roles = "admin")]
        [Route("api/v1.0/customer")]
        // GET: api/Customer
        public IEnumerable<Customer> GetCustomers()
        {
            return service.GetCustomers();
        }

        [Authorize(Roles = "admin")]
        [Route("api/v1.0/customer/{id}")]
        // GET: api/Customer/5
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = service.GetCustomer(id);

            if (customer == null)
                return NotFound();
            else
                return Ok(customer);
        }

        [AllowAnonymous]
        // POST: api/Customer
        [HttpPost]
        public HttpResponseMessage AddCustomers(Customer customer)
        {
            CustomerValidation validations = new CustomerValidation();
            var result = validations.Validate(customer);

            if (!result.IsValid) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                service.AddCustomer(customer);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize(Roles = "user")]
        //PUT: api/Customer/5
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, string address)
        {
            if (service.UpdateCustomerAddress(id, address))
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        [Authorize(Roles = "user")]
        [Route("api/customers/{customerId}/rent/{carId}")]
        [HttpPost]
        public HttpResponseMessage RentStart(int customerId, int carId)
        {
            try
            {
                var rentStart = service.RentStart(customerId, carId);

                if (rentStart)
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize(Roles = "admin")]
        [Route("api/v1.0/customers/{customerId}/return/{carId}")]
        [HttpPut]
        public HttpResponseMessage RentEnd(int customerId, int carId)
        {
            try
            {
                var rentEnd = service.RentEnd(customerId, carId);

                if (rentEnd)
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }


        //[Authorize(Roles = "admin")]
        //// DELETE: api/Customer/5
        //public void Delete(int id)
        //{
        //}
    }
}
