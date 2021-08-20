using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Figure
{
    [Serializable]
    public class Figure
    {
        public Figure() { }
        public int SideCount { get; set; }
        public int SideLength { get; set; }

        private string Parameters
        {
            get
            {
                return $"SideCount={SideCount} ; SideLength={SideLength}";
                
            }
        }
        public void CalculateSquare(int sideCount, int sideLength)
        {
            double Square = (sideCount * Math.Pow(sideLength, 2)) / (4 * Math.Tan((Math.PI / sideCount)));
            Console.WriteLine($"Square={Square}");
        }
    }
}
