using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace Figure
{
    public class BogusGenerator
    {
        public Figure GenerateFigure()
        {
            var figure = new Faker<Figure>()
                .StrictMode(true)
                .RuleFor(f => f.SideCount, f => f.Random.Number(1, 10))
                .RuleFor(f => f.SideLength, f => f.Random.Number(1, 100));
            return figure;
        }
        public Box GenerateBox()
        {
            var figure = new Faker<Figure>()
                .StrictMode(true)
                .RuleFor(f => f.SideCount, f => f.Random.Number(1, 10))
                .RuleFor(f => f.SideLength, f => f.Random.Number(1, 100));

            var box = new Faker<Box>()
                .StrictMode(true)
                .RuleFor(b => b.Owner, b => b.Person.FirstName)
                .RuleFor(b => b.Figures, b => figure.Generate(5).ToList());
            return box;
        }
    }
}
