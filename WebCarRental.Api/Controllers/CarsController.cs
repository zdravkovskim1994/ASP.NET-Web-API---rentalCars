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
    //asdas
    [Authorize]
    public class CarsController : ApiController
    {
        private readonly ICarService service;

        public CarsController(ICarService service)
        {
            this.service = service;
        }

        [Authorize(Roles = "admin")]
        [Route("api/v1.0/cars")]
        public IEnumerable<Car> GetCars()
        {
            return service.GetCars();
        }

        //AvailableCars
        [AllowAnonymous]
        [Route("api/cars/availableCars")]
        [HttpGet]
        public IEnumerable<Car> AvailableCars()
        {
            return service.AvailableCars();
        }

        [Authorize(Roles = "user")]
        [Route("api/cars/{id}")]
        // GET: api/Cars/5
        public IHttpActionResult GetCar(int id)
        {
            var car = service.GetCar(id);

            if(car == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(car);
            }       
        }

        // POST: api/Cars
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("api/v1.0/cars")]
        public HttpResponseMessage AddCar(Car car)
        {
            CarValidation validation = new CarValidation();
            var result = validation.Validate(car);

            if (!result.IsValid) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                service.AddCar(car);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Cars/5
        [Authorize(Roles = "admin")]
        [Route("api/v1.0/cars/{id}")]
        [HttpPut]
        public HttpResponseMessage UpdateCar(int id, Car car)
        {
            CarValidation validations = new CarValidation();
            var result = validations.Validate(car);

            if (!result.IsValid) return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                if (service.UpdateCar(id, car))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Cars/5
        [Authorize(Roles = "admin")]
        [Route("api/v1.0/cars/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (service.DeleteCar(id))
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
                
        }

        [Authorize(Roles = "user")]
        //Search Cars
        [Route("api/cars/search")]
        public IEnumerable<Car> Get(string searchTerm)
        {
            return service.SearchCar(searchTerm);
        }
    }
}
