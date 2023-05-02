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

        public Task<int> AddUser(User user)
        {
            return _dbConnection.InsertAsync(user);
        }

        public Task<int> DeleteUser(User user)
        {
            return _dbConnection.DeleteAsync(user);
        }

        public Task<User> GetUser()
        {
            return _dbConnection.Table<User>().FirstOrDefaultAsync();
        }

        public Task<int> UpdateUser(User user)
        {
            return _dbConnection.UpdateAsync(user);
        }
    }
}
