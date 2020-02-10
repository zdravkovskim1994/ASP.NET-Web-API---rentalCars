using System;
using System.Collections.Generic;
using WebCarRental.Core.Models;
using WebCarRental.Core.Repository;

namespace WebCarRental.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDatabaseRepo repo;
        
        public CustomerService(IDatabaseRepo repo)
        {
            this.repo = repo;
        }

        public bool DeleteCustomer(int customerId)
        {
            return repo.DeleteCustomer(customerId);
        }

        public bool AddCustomer(Customer customer)
        {
            if (customer == null)
                return false;
            return repo.AddCustomer(customer);
        }

        public Customer GetCustomer(int customerId)
        {
            return repo.GetCustomer(customerId);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return repo.GetCustomers();
        }

        public bool UpdateCustomerAddress(int id, string address)
        {
            return repo.UpdateCustomerAddress(id, address);
        }

        //Rent
        public bool RentStart(int customerId, int carId)
        {

            var customer = repo.GetCustomer(customerId);
            if (customer == null)
                throw new ApplicationException("Customer doesn't exist!");

            var car = repo.GetCar(carId);
            if (car == null)
                throw new ApplicationException("Car doesn't exist!");

            return repo.RentStart(customerId, carId);
        }

        public bool RentEnd(int customerId, int carId)
        {
            var customer = repo.GetCustomer(customerId);
            if (customer == null)
                throw new ApplicationException("Customer doesn't exist!");

            var car = repo.GetCar(carId);
            if (car == null)
                throw new ApplicationException("Car doesn't exist!");

            return repo.RentEnd(customerId, carId);
        }
    }
}
