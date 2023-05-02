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

        private async Task SetupDatabase()
        {
            if(_dbConnection == null)
            {
                string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "cashFlow.db");
                _dbConnection = new SQLiteAsyncConnection(dbpath);
                await _dbConnection.CreateTableAsync<User>();
            }
        }

        public async Task<int> AddUser(User user)
        {
            await SetupDatabase();
            return await _dbConnection.InsertAsync(user);
        }

        public async Task<int> DeleteUser()
        {
            await SetupDatabase();
            return await _dbConnection.DeleteAllAsync<User>();
        }

        public async Task<User> GetUser()
        {
            await SetupDatabase();
            return await _dbConnection.Table<User>().FirstOrDefaultAsync();
        }

        public async Task<int> UpdateUser(User user)
        {
            await SetupDatabase();
            return await _dbConnection.UpdateAsync(user);
        }
    }
}
