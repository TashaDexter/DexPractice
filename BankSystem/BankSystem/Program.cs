using BankSystem.Models;
using BankSystem.Services;
using System;
using System.Collections.Generic;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> firstNames = new Dictionary<int, string>() { { 0, "James" }, { 1, "Jack"}, { 2,"Liza"}, { 3, "Kent" }, { 4, "Leo"} };
            Dictionary<int, string> lastNames = new Dictionary<int, string>() { { 0, "Harrison" }, { 1, "Smith" }, { 2, "Kane" }, { 3, "Pillow" }, { 4, "Holly" } };

            BankService bankService1 = new BankService();
            Console.WriteLine("---------------BankService---------------");

            Console.WriteLine("\nHow many clients do you want to add?");
            int clientNumber = Convert.ToInt32(Console.ReadLine());
            int number = 0;
            Random rnd = new Random();
            while (number < clientNumber)
            {
                Client client = new Client() { PassportID= rnd.Next(1000, 9999), Age=rnd.Next(18,50), FirstName=firstNames[rnd.Next(0,4)], LastName=lastNames[rnd.Next(0,4)] };
                bankService1.Clients.Add(client);
                number++;
            }

            foreach (var client in bankService1.Clients)
            {
                Console.WriteLine($"PassportID={client.PassportID}, " +
                    $"FirstName={client.FirstName}, " +
                    $"LastName={client.LastName}, " +
                    $"Age={client.Age}");
            }

            Console.WriteLine("\nHow many employees do you want to add?");
            int employeeNumber = Convert.ToInt32(Console.ReadLine());
            number = 0;
            while (number < employeeNumber)
            {
                Employee employee = new Employee() { PassportID = rnd.Next(1000, 9999), Age = rnd.Next(18, 50), FirstName = firstNames[rnd.Next(0, 4)], LastName = lastNames[rnd.Next(0, 4)] };
                bankService1.Employees.Add(employee);
                number++;
            }

            foreach (var employee in bankService1.Employees)
            {
                Console.WriteLine($"PassportID={employee.PassportID}, " +
                    $"FirstName={employee.FirstName}, " +
                    $"LastName={employee.LastName}, " +
                    $"Age={employee.Age}");
            }

            Console.WriteLine("\nEnter PassportID to search:");
            int passportID = Convert.ToInt32(Console.ReadLine());

            IPerson person=bankService1.Find<IPerson>(passportID);
            if (person != null)
            {
                Console.WriteLine($"PassportID={person.PassportID}, " +
                      $"FirstName={person.FirstName}, " +
                      $"LastName={person.LastName}, " +
                      $"Age={person.Age}, " +
                      $"PersonType={ person.GetType()}");
            }

            Ruble ruble = new Ruble() { ValueInDollars = 0.014 };
            Euro euro = new Euro() { ValueInDollars = 1.19 };
            Dollar dollar = new Dollar() { ValueInDollars = 1 };

            Exchange exchange1 = new Exchange();

            Console.WriteLine("\nEnter currency to exchange:\n(ruble/dollar/euro)");
            string currencyIn = Console.ReadLine();
            Console.WriteLine("\nEnter currency to receive:\n(ruble/dollar/euro)");
            string currencyOut = Console.ReadLine();
            Console.WriteLine("\nEnter ammount to exchange:");
            double ammount = Convert.ToDouble(Console.ReadLine());

            Currency curIn=null;
            switch (currencyIn)
            {
                case "ruble":
                    {
                        curIn = ruble;
                        break;
                    }
                case "dollar":
                    {
                        curIn = dollar;
                        break;
                    }
                case "euro":
                    {
                        curIn = euro;
                        break;
                    }
                default:
                    break;
            }

            Currency curOut=null;
            switch (currencyOut)
            {
                case "ruble":
                    {
                        curOut = ruble;
                        break;
                    }
                case "dollar":
                    {
                        curOut = dollar;
                        break;
                    }
                case "euro":
                    {
                        curOut = euro;
                        break;
                    }
                default:
                    break;
            }
            if (curIn == null || curOut == null)
                Console.WriteLine("Error! The entered data is incorrect.");
            else
                Console.WriteLine($"\n{ammount} {currencyIn} = {exchange1.ExchangeCurrency(curIn, curOut, ammount)} {currencyOut}");
            
            Console.ReadKey();
        }
    }
}
