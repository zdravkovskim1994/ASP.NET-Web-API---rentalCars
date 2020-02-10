using System;
using System.Collections.Generic;
using WebCarRental.Core.Models;
using WebCarRental.Core.Repository;

namespace WebCarRental.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IDatabaseRepo repo;
        public CarService(IDatabaseRepo repo)
        {
            this.repo = repo;
        }

        public bool AddCar(Car car)
        {
            if(car == null)
            {
                return false;
            }
            else
            {
                return repo.AddCar(car);
            }
        }

        public bool DeleteCar(int id)
        {
            return repo.DeleteCar(id);   
        }

        public Car GetCar(int carId)
        {
            return repo.GetCar(carId);
        }

        public IEnumerable<Car> GetCars()
        {
            return repo.GetCars();
        }

        public bool UpdateCar(int id, Car car)
        {
            if (car == null) return false;

            var foundCar = repo.GetCar(id);

            if (foundCar == null)
                throw new ApplicationException("Car doesn't exist");

            return repo.UpdateCar(id, car);
        }

        public List<Car> AvailableCars()
        {
            return repo.AvailableCars();
        }

        public List<Car> SearchCar(string searchTerm)
        {
            return repo.SearchCar(searchTerm);
        }
    }
}
