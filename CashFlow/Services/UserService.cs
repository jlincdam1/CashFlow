using CashFlow.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Services
{
    public class UserService : IUserService
    {
        public SQLiteAsyncConnection _dbConnection;

        public UserService()
        {
            SetupDatabase();
        }

        private async void SetupDatabase()
        {
            if(_dbConnection == null)
            {
                string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "cashFlow.db");
                _dbConnection = new SQLiteAsyncConnection(dbpath);
                await _dbConnection.CreateTableAsync<User>();
            }
        }

        public Task<bool> AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUser()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
