using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public float InitCapital { get; set; }
        public float Capital { get; set; }
        public float MensualEarning { get; set; }
    }
}
