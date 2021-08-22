using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services
{
    public class DataExportService
    {
        public void ExportDataToFile<T>(T obj, string file)
        {
            var myType = obj.GetType();
            var properties = myType.GetProperties();

            string path = Directory.GetCurrentDirectory() + "\\Export";
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            string data = "";

            foreach (var property in properties)
            {
               data+=property.Name+" = "+property.GetValue(obj)+"\n";
            }

            using (FileStream fsExport = new FileStream($"{path}\\{file}", FileMode.Append))
            {
                byte[] dataArray = System.Text.Encoding.Default.GetBytes(data+"\n");
                fsExport.Write(dataArray, 0, dataArray.Length);
                fsExport.Close();
            }

            Console.WriteLine($"Data exported to {file}");
            
        }
    }
}
