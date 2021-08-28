using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class CurrencyResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public Dictionary<string, double> Data { get; set; }
    }
}
