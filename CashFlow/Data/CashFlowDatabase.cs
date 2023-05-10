using CashFlow.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Data
{
    public class CashFlowDatabase
    {
        SQLiteAsyncConnection Database;
        public CashFlowDatabase() { }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await Database.CreateTableAsync<User>();
            await Database.CreateTableAsync<Activities>();
        }
        public async Task<User> GetUserAsync()
        {
            await Init();
            return await Database.Table<User>().FirstOrDefaultAsync();
        }

        public async Task<int> SaveUserAsync(User user)
        {
            await Init();
            return await Database.InsertAsync(user);
        }

        public async Task<int> DeleteUserAsync()
        {
            await Init();
            return await Database.DeleteAllAsync<User>();
        }
        public async Task<int> UpdateUserAsync(User user)
        {
            await Init();
            return await Database.UpdateAsync(user);
        }

        // Métodos para los movimientos
        public async Task<List<Activities>> GetActivitiesAsync()
        {
            await Init();
            return await Database.Table<Activities>().ToListAsync();
        }
        public async Task<Activities> GetActivityAsync(int id)
        {
            await Init();
            return await Database.Table<Activities>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> AddActivityAsync(Activities act)
        {
            await Init();
            return await Database.InsertAsync(act);
        }
        public async Task<int> DeleteActivityAsync(Activities act)
        {
            await Init();
            return await Database.DeleteAsync(act);
        }
        public async Task<int> DeleteAllActivities()
        {
            await Init();
            return await Database.DeleteAllAsync<Activities>();
        }

        public async Task<int> UpdateActivity(Activities act)
        {
            await Init();
            return await Database.UpdateAsync(act);
        }
    }
}
