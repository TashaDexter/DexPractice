using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services
{
    public class DataExportService
    {
        public void Display<T>(T obj)
        {
            var myType = obj.GetType();
            var properties = myType.GetProperties();
            foreach (var property in properties)
            {
                Console.WriteLine($"{property.Name} = {property.GetValue(obj)}");
            }
        }
    }
}
