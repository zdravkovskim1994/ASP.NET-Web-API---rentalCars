using System;
using System.Collections.Generic;
using WebCarRental.Core.Models;

namespace WebCarRental.Core.Services
{
    public interface ICustomerService
    {
        bool DeleteCustomer(int customerId);
        bool AddCustomer(Customer customer);
        Customer GetCustomer(int customerId);
        IEnumerable<Customer> GetCustomers();
        bool UpdateCustomerAddress(int id, string address);
        //Rent
        bool RentStart(int customerId, int carId);
        bool RentEnd(int customerId, int carId);
    }
}
