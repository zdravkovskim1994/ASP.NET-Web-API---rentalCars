using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebCarRental.Core.Models;
using WebCarRental.Core.Repository;

namespace WebCarRental.Api.Controllers
{
    [Authorize(Roles = "admin")]
    public class RentalOrdersController : ApiController
    {
        //Admin Controler
        private readonly IDatabaseRepo repo;
        public RentalOrdersController(IDatabaseRepo repo)
        {
            this.repo = repo;
        }

        [Route("api/v1.0/rentalOrders")]
        // GET: api/RentalOrders
        public IEnumerable<RentalOrder> Get()
        {
            return repo.GetRentalOrders();
        }

        [Route("api/v1.0/rentalOrders/rented/{id}")]
        public IHttpActionResult GetRentalCar(int id)
        {
            var car = repo.CarRented(id);

            if (car == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(car);
            }
        }
        


    }
}
