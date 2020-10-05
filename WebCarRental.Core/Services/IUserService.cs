using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCarRental.Core.Models;

namespace WebCarRental.Core.Services
{
    public interface IUserService
    {
        User ValidateLogin(string username, string password);
    }
}
