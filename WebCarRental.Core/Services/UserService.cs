using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCarRental.Core.Models;
using WebCarRental.Core.Repository;

namespace WebCarRental.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IDatabaseRepo repo;
        public UserService(IDatabaseRepo repo)
        {
            this.repo = repo;
        }

        public User ValidateLogin(string username, string password)
        {
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            else
            {
                return repo.GetUser(username, password);
            }
        }
    }
}
