using BankSystem.Models;
using BankSystem.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Threading;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var locker = new object();
            BankService bankService1 = new BankService(locker);
            Console.WriteLine("---------------BankService---------------");

            var generator = new FakeDataService();

            //тестовое добавление клиентов
            /*
            Console.WriteLine("\nHow many clients do you want to add?");
            int clientNumber = Convert.ToInt32(Console.ReadLine());
            int number = 0;
            Random rnd = new Random();
            while (number < clientNumber)
            {
                Client client =generator.GenerateClient();
                bankService1.Add(client);
                number++;
            }

            foreach (var client in bankService1.Clients)
            {
                Console.WriteLine($"PassportID={client.PassportID}, " +
                    $"FirstName={client.FirstName}, " +
                    $"LastName={client.LastName}, " +
                    $"Age={client.Age}");
            }*/

            //тестовое добавление сотрудников
            /*
            Console.WriteLine("\nHow many employees do you want to add?");
            int employeeNumber = Convert.ToInt32(Console.ReadLine());
            number = 0;
            while (number < employeeNumber)
            {
                Employee employee = generator.GenerateEmployee();
                bankService1.Add(employee);
                number++;
            }

            foreach (var employee in bankService1.Employees)
            {
                Console.WriteLine($"PassportID={employee.PassportID}, " +
                    $"FirstName={employee.FirstName}, " +
                    $"LastName={employee.LastName}, " +
                    $"Age={employee.Age}");
            }*/

            //Get from API - GetActualCurrencyService тест
            //GetActualCurrencyService getActualCurrencyService = new GetActualCurrencyService("ebdb8321776f5ae7487a39d9c368c544");

            //List<string> currencyList = getFromApiService.GetCurrencyList().Result.Data;

            //foreach (var currency in currencyList)
            //{
            //    var result = getFromApiService.GetCurrency(currency).Result.Data;
            //    foreach (var res in result)
            //    {
            //        Console.WriteLine($"{res.Key} : {res.Value}");
            //    }
            //}

            bankService1.PrintClients();

            ThreadPool.QueueUserWorkItem(_ =>
            {
                for (int i = 0; i < 100; i++)
                {
                    lock (locker)
                    {
                        Client client = generator.GenerateClient();
                        bankService1.Add(client);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Added client:{client.FirstName} {client.LastName}");
                        Console.ResetColor();
                    }
                    Thread.Sleep(2000);
                }
            });

            Console.ReadKey();
        }
    }
}
