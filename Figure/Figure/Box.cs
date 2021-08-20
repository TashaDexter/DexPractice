using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Figure
{
    [Serializable]
    public class Box
    {
        private List<Figure> _figures;
        public string Owner { get; set; }
        public Box()
        {
            _figures = new List<Figure>();
            Owner = "NoName";
            Name = Owner + " Box";
        }

        public void AddFigure(Figure figure)
        {
            if (!_figures.Contains(figure))
                _figures.Add(figure);
        }

        public List<Figure> Figures
        {
            get { return _figures; }
            set { _figures = value; }
        }

        private string Name { get; set; }
        public void DisplayFigure(string message)
        {
            Console.WriteLine(message);
        }
    }
}
