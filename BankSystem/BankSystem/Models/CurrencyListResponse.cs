using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class CurrencyListResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<string> Data { get; set; }
    }
}
