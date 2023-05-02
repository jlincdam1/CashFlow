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
        Task<User> GetUser();

        Task<int> AddUser(User user);

        Task<int> DeleteUser();
        Task<int> UpdateUser(User user);
    }
}
