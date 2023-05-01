using CashFlow.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Data
{
    public class UserRepository
    {
        string _dbPath;
        private SQLiteConnection _connection;

        public UserRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void Init()
        {
            _connection = new SQLiteConnection(_dbPath);
            _connection.CreateTable<User>();
        }

        public User Get(int id)
        {
            Init();
            return _connection.Table<User>().Where(o => o.Id == id).FirstOrDefault();
        }

        public void Add(User user) 
        { 
            _connection = new SQLiteConnection( _dbPath);
            _connection.Insert(user);
        }

        public void Delete(int id)
        {
            _connection = new SQLiteConnection(_dbPath);
            _connection.Delete(new User { Id = id});
        }

        public void Update(int id, User user)
        {
            _connection.Update(new User { Id = id, });
        }
    }
}
