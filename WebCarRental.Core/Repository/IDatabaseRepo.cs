using System;
using System.Collections.Generic;
using WebCarRental.Core.Models;

namespace WebCarRental.Core.Repository
{
    public interface IDatabaseRepo
    {
        //Car
        bool DeleteCar(int id);
        bool AddCar(Car car);
        bool UpdateCar(int id, Car car);
        List<Car> GetCars();
        Car GetCar(int carId);
        List<Car> AvailableCars();
        List<Car> SearchCar(string searchTerm);

        //Customer
        bool DeleteCustomer(int customerId);
        bool AddCustomer(Customer customer);
        Customer GetCustomer(int customerId);
        List<Customer> GetCustomers();
        bool UpdateCustomerAddress(int id, string address);

        //RentalOrder
        bool RentStart(int customerId, int carId);
        bool RentEnd(int customerId, int carId);

        //RentalOrders-admin
        List<RentalOrder> GetRentalOrders();
        Car CarRented(int carId);

        //UserLoggin
        User GetUser(string username, string password);


        

    }
}
