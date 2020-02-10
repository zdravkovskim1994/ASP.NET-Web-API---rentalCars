using System;
using System.Collections.Generic;
using WebCarRental.Core.Models;

namespace WebCarRental.Core.Services
{
    public interface ICarService
    {
        bool DeleteCar(int id);
        bool AddCar(Car car);
        bool UpdateCar(int id, Car car);
        IEnumerable<Car> GetCars();
        Car GetCar(int carId);
        List<Car> AvailableCars();
        List<Car> SearchCar(string searchTerm);

    }
}
