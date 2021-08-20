using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Figure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many figures do you want to add?");
            int figureNumber = Convert.ToInt32(Console.ReadLine());

            var generator = new BogusGenerator();
            Box box = new Box() { Owner="Sergey"};
            for (int i = 0; i < figureNumber; i++)
            {
                box.AddFigure(generator.GenerateFigure());
            }
           
            string serJsonFigure=FiguresSerJson(box.Figures);
            FiguresDeserJson(serJsonFigure);

            FiguresSerXml(box.Figures);
            FiguresDeserXml();

            FiguresSerBinary(box.Figures);
            FiguresDeserBinary();

            /*
            Console.WriteLine("---------------------------");
            Console.WriteLine("How many boxes do you want to add?");
            int boxesNumber = Convert.ToInt32(Console.ReadLine());
            List<Box> boxes = new List<Box>();
            for (int i = 0; i < boxesNumber; i++)
            {
                boxes.Add(generator.GenerateBox());
            }
            
            string serJsonBox=BoxSerJson(boxes);
            BoxDeserJson(serJsonBox);

            BoxSerXml(boxes);
            BoxDeserXml();

            BoxSerBinary(boxes);
            BoxesDeserBinary();*/


            Console.Read();

        }
        public static string FiguresSerJson(List<Figure> figures)
        {
            Console.WriteLine("\nJson Serialization | Figures\n");
            string path = Directory.GetCurrentDirectory() + "\\Serialization";
            CheckDirectory(path);

            string result = "";
            using (FileStream fs = new FileStream($"{path}\\figuresSerJson.txt", FileMode.OpenOrCreate))
            {
                result = JsonConvert.SerializeObject(figures);
                byte[] arrayClients = System.Text.Encoding.Default.GetBytes(result);
                fs.Write(arrayClients, 0, arrayClients.Length);
                fs.Close();
            }
            Console.WriteLine(result);
            return result;
        }

        public static void FiguresDeserJson(string text)
        {
            Console.WriteLine("\nJson Deserialization | Figures\n");

            string path = Directory.GetCurrentDirectory() + "\\Serialization";
            CheckDirectory(path);

            List<Figure> deserJson = new List<Figure>();

            using (FileStream fs = new FileStream($"{path}\\figuresSerJson.txt", FileMode.OpenOrCreate))
            {
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                string serJson = System.Text.Encoding.Default.GetString(array);
                deserJson = JsonConvert.DeserializeObject<List<Figure>>(serJson);
                fs.Close();
            }

            foreach (var d in deserJson)
            {
                Console.WriteLine($"SideCount={d.SideCount}, SideLength={d.SideLength}");
            }
        }

        public static void FiguresSerXml(List<Figure> figures)
        {
            Console.WriteLine("\nXml Serialization | Figures\n");
            string path= Directory.GetCurrentDirectory() + "\\Serialization";
            CheckDirectory(path);

            XmlSerializer formatter = new XmlSerializer(typeof(List<Figure>));
            using (FileStream fs = new FileStream($"{path}\\figures.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, figures);
                fs.Close();
            }
        }

        public static void FiguresDeserXml()
        {
            Console.WriteLine("\nXml Deserialization | Figures\n");
            string path = Directory.GetCurrentDirectory() + "\\Serialization";
            CheckDirectory(path);

            List<Figure> deserXml;
            XmlSerializer formatter = new XmlSerializer(typeof(List<Figure>));
            using (FileStream fs = new FileStream($"{path}\\figures.xml", FileMode.OpenOrCreate))
            {
                deserXml = (List<Figure>)formatter.Deserialize(fs);
            }

            foreach (var d in deserXml)
            {
                Console.WriteLine($"SideCount={d.SideCount}, SideLength={d.SideLength}");
            }
        }

        public static void FiguresSerBinary(List<Figure> figures)
        {
            Console.WriteLine("\nBinary Serialization | Figures\n");
            string path = Directory.GetCurrentDirectory() + "\\Serialization";
            CheckDirectory(path);

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream($"{path}\\figures.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, figures);
            }
        }

        public static void FiguresDeserBinary()
        {
            Console.WriteLine("\nBinary Deserialization | Figures\n");
            string path = Directory.GetCurrentDirectory() + "\\Serialization";
            CheckDirectory(path);

            List<Figure> deserBinary;

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream($"{path}\\figures.dat", FileMode.OpenOrCreate))
            {
                deserBinary = (List<Figure>)formatter.Deserialize(fs);
            }

            foreach (var d in deserBinary)
            {
                Console.WriteLine($"SideCount={d.SideCount}, SideLength={d.SideLength}");
            }
        }

        public static string BoxSerJson(List<Box> boxes)
        {
            Console.WriteLine("\nJson Serialization | Boxes\n");
            string path = Directory.GetCurrentDirectory() + "\\Serialization";
            CheckDirectory(path);

            string result = "";
            using (FileStream fs = new FileStream($"{path}\\boxSerJson.txt", FileMode.OpenOrCreate))
            {
                result = JsonConvert.SerializeObject(boxes);
                byte[] arrayClients = System.Text.Encoding.Default.GetBytes(result);
                fs.Write(arrayClients, 0, arrayClients.Length);
                fs.Close();
            }
            Console.WriteLine(result);
            return result;
        }

        public static void BoxDeserJson(string text)
        {
            Console.WriteLine("\nJson Deserialization | Boxes\n");

            string path = Directory.GetCurrentDirectory() + "\\Serialization";
            CheckDirectory(path);

            List<Box> deserJson = new List<Box>();

            using (FileStream fs = new FileStream($"{path}\\boxSerJson.txt", FileMode.OpenOrCreate))
            {
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                string serJson = System.Text.Encoding.Default.GetString(array);
                deserJson = JsonConvert.DeserializeObject<List<Box>>(serJson);
                fs.Close();
            }
            foreach(var box in deserJson)
            {
                Console.WriteLine($"Owner={box.Owner}");
                foreach (var b in box.Figures)
                    Console.WriteLine($"SideCount={b.SideCount}, SideLength={b.SideLength}");
            }
        }

        public static void BoxSerXml(List<Box> boxes)
        {
            Console.WriteLine("\nXml Serialization | Boxes\n");
            string path = Directory.GetCurrentDirectory() + "\\Serialization";
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            XmlSerializer formatter = new XmlSerializer(typeof(List<Box>));
            using (FileStream fs = new FileStream($"{path}\\boxes.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, boxes);
                fs.Close();
            }
        }

        public static void BoxDeserXml()
        {
            Console.WriteLine("\nXml Deserialization | Boxes\n");
            string path = Directory.GetCurrentDirectory() + "\\Serialization";
            CheckDirectory(path);

            List<Box> deserXml;
            XmlSerializer formatter = new XmlSerializer(typeof(List<Box>));
            using (FileStream fs = new FileStream($"{path}\\boxes.xml", FileMode.OpenOrCreate))
            {
                deserXml = (List<Box>)formatter.Deserialize(fs);
            }

            foreach (var box in deserXml)
            {
                Console.WriteLine($"Owner={box.Owner}");
                foreach (var b in box.Figures)
                    Console.WriteLine($"SideCount={b.SideCount}, SideLength={b.SideLength}");
            }
        }

        public static void BoxSerBinary(List<Box> boxes)
        {
            Console.WriteLine("\nBinary Serialization | Boxes\n");
            string path = Directory.GetCurrentDirectory() + "\\Serialization";            
            CheckDirectory(path);

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream($"{path}\\boxes.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, boxes);
            }
        }

        public static void BoxesDeserBinary()
        {
            Console.WriteLine("\nBinary Deserialization | Boxes\n");
            string path = Directory.GetCurrentDirectory() + "\\Serialization";
            CheckDirectory(path);

            List<Box> deserBinary;

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream($"{path}\\boxes.dat", FileMode.OpenOrCreate))
            {
                deserBinary = (List<Box>)formatter.Deserialize(fs);
            }

            foreach (var box in deserBinary)
            {
                Console.WriteLine($"Owner={box.Owner}");
                foreach (var b in box.Figures)
                    Console.WriteLine($"SideCount={b.SideCount}, SideLength={b.SideLength}");
            }
        }

        public static void CheckDirectory(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
    }
}