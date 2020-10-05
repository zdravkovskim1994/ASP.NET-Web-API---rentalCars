using System;
using System.Collections.Generic;
using System.Linq;
using WebCarRental.Core.Models;
using Dapper;
using log4net;
using System.Data.SqlClient;
using WebCarRental.Core.DbHelper;
using System.Data;

namespace WebCarRental.Core.Repository
{
    public class DatabaseRepo : IDatabaseRepo
    {
        ILog logger;
        public DatabaseRepo(ILog logger)
        {
            this.logger = logger;
        }

        #region Db Car
        public bool AddCar(Car car)
        {
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@tagNumber", car.TagNumber);
                    parameters.Add("@model", car.Model);
                    parameters.Add("@carYear", car.CarYear);
                    parameters.Add("@AirConditioner", car.AirConditioner);
                    parameters.Add("@daily", car.Daily);
                    parameters.Add("@monthly", car.Monthly);

                    int result = conn.Execute("InsertCar", parameters, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while adding the car! " + ex.Message, ex);
                return false;
            }
        }

        public bool DeleteCar(int id)
        {
            logger.Info("Deliting car " + id);
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@carId", id);

                    int result = conn.Execute("Delete_car", parameter, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while deleting the car! " + ex.Message, ex);
                return false;
            }
        }

        public Car GetCar(int carId)
        {
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", carId);

                    Car result = conn.Query<Car>("GetCar", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while getting the car! " + ex.Message, ex);
                return null;

            }
        }

        public List<Car> GetCars()
        {
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    List<Car> result = conn.Query<Car>("GetAllCars", commandType: CommandType.StoredProcedure).ToList();

                    return result;

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while getting the cars! " + ex.Message, ex);
                return null;
            }
        }

        public bool UpdateCar(int id, Car car)
        {
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", id);
                    parameters.Add("@daily", car.Daily);
                    parameters.Add("@monthly", car.Monthly);

                    int result = conn.Execute("UpdateCarRates", parameters, commandType: CommandType.StoredProcedure);

                    return result > 0;

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while updating the (daily,monthly) -car! " + ex.Message, ex);
                return false;
            }

        }

        public List<Car> AvailableCars()
        {
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    List<Car> result = conn.Query<Car>("AvailableCars", commandType: CommandType.StoredProcedure).ToList();

                    return result;

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while getting the available cars! " + ex.Message, ex);
                return null;
            }
        }

        public List<Car> SearchCar(string searchTerm)
        {
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@searchTerm", searchTerm);


                    List<Car> result = conn.Query<Car>("SearchCarByName", parameters, commandType: CommandType.StoredProcedure).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while searching the car! " + ex.Message, ex);
                return null;
            }
        }

        public Car CarRented(int carId)
        {
            try
            {
                using(SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@carId", carId);

                    Car result = conn.Query<Car>("IsCarRented", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while getting the car rented!" + ex.Message, ex);
                return null;
            }
        }
        #endregion

        #region Db Customer
        public bool DeleteCustomer(int customerId)
        {
            try
            {
                using(SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", customerId);

                    int result = conn.Execute("DeleteCustomer", commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while deleting the customer! " + ex.Message, ex);
                return false;
            }
        }
        public bool AddCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@drvLicNumber", customer.DrvLicNumber);
                    parameters.Add("@fullName", customer.FullName);
                    parameters.Add("@address", customer.Address);
                    parameters.Add("@country", customer.Country);
                    parameters.Add("@city", customer.City);

                    int result = conn.Execute("InsertCustomer", parameters, commandType: CommandType.StoredProcedure);

                    return result > 0;

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while adding the customer! " + ex.Message, ex);
                return false;
            }
            
        }
        public Customer GetCustomer(int customerId)
        {
            try
            {
                using(SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@id", customerId);

                    Customer result = conn.Query<Customer>("GetCustomer", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while getting the customer! " + ex.Message, ex);
                return null;
            }
        }
        public List<Customer> GetCustomers()
        {
            try
            {
                using(SqlConnection conn = DbAccess.GetConnection())
                {
                    List<Customer> result = conn.Query<Customer>("GetAllCustomers", commandType: CommandType.StoredProcedure).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while getting the customers! " + ex.Message, ex);
                return null;
            }
        }

        public bool UpdateCustomerAddress(int id, string address)
        {
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", id);
                    parameters.Add("@address", address);

                    int result = conn.Execute("UpdateCustomerAddress", parameters, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while updating the customer address! " + ex.Message, ex);
                return false;
            }
        }
        #endregion

        #region Db RentalOrder
        public bool RentStart(int customerId, int carId)
        {
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@customerId", customerId);
                    parameters.Add("@carId", carId);

                    int result = conn.Execute("RentStart",parameters, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while rent start! " + ex.Message, ex);
                return false;
            }
        }

        public bool RentEnd(int customerId, int carId)
        {
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@customerId", customerId);
                    parameters.Add("@carId", carId);

                    int result = conn.Execute("RentEnd", parameters, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while rent end! " + ex.Message, ex);
                return false;
            }
        }

        public List<RentalOrder> GetRentalOrders()
        {
            try
            {
                using (SqlConnection conn = DbAccess.GetConnection())
                {
                    List<RentalOrder> result = conn.Query<RentalOrder>("GetRentalOrders", commandType: CommandType.StoredProcedure).ToList();

                    return result;

                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while getting rental orders! " + ex.Message, ex);
                return null;
            }
        }
        #endregion

        #region Db User
        public User GetUser(string username, string password)
        {
            try
            {
                using(SqlConnection conn = DbAccess.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@username", username);
                    parameters.Add("@password", password);

                    User result = conn.Query<User>("UserLoginSp", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error happened while user logging! " + ex.Message, ex);
                return null;
            }
        }
        #endregion

        
    }
}
