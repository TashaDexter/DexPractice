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
        public int SideCount { get; set; }
        public int SideLength { get; set; }
    }
}
