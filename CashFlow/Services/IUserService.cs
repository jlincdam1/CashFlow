using CashFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUser();

        Task<bool> AddUser(User user);

        Task<bool> DeleteUser(User user);
        Task<bool> UpdateUser(User user);
    }
}
