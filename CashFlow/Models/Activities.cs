using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Models
{
    public class Activities
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public float quantity { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}
