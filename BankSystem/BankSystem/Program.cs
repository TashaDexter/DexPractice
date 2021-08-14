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

            //тестовое добавление клиентов
            Console.WriteLine("\nHow many clients do you want to add?");
            int clientNumber = Convert.ToInt32(Console.ReadLine());
            int number = 0;
            Random rnd = new Random();
            while (number < clientNumber)
            {
                Client client = new Client() { PassportID= rnd.Next(1000, 9999), Age=rnd.Next(18,50), FirstName=firstNames[rnd.Next(0,4)], LastName=lastNames[rnd.Next(0,4)] };
                bankService1.Add(client);
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
                bankService1.Add(employee);
                number++;
            }

            foreach (var employee in bankService1.Employees)
            {
                Console.WriteLine($"PassportID={employee.PassportID}, " +
                    $"FirstName={employee.FirstName}, " +
                    $"LastName={employee.LastName}, " +
                    $"Age={employee.Age}");
            }

            //проверка поиска            
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
            else
            {
                Console.WriteLine($"Person with passportID={passportID} was not found.");
            }
            

            Console.WriteLine("----------------------------------------------");

            Ruble ruble = new Ruble() { Type="RUB",ValueInDollars = 0.014 };
            Euro euro = new Euro() { Type="EUR", ValueInDollars = 1.19 };
            Dollar dollar = new Dollar() { Type="USD", ValueInDollars = 1 };

            Exchange exchange1 = new Exchange();

            Client client1 = new Client() { PassportID = 2612, Age = 28, FirstName = "Kane", LastName = "Bounty"};
            Account acc1 = new Account() { Currency = ruble, Ammount = 278.5 };
            Account acc2 = new Account() { Currency = dollar, Ammount = 29.8 };

            Client client2 = new Client() { PassportID = 7859, Age = 32, FirstName = "Lamar", LastName = "Kiss" };
            Account acc3 = new Account() { Currency = ruble, Ammount = 1578 };
            Account acc4 = new Account() { Currency = euro, Ammount = 4824 };

            Employee employee1 = new Employee() { PassportID = 2485, Age = 45, FirstName = "Kate", LastName = "Gamby" };

            bankService1.AddClientAccount(client1, acc1);
            bankService1.AddClientAccount(client1, acc2);
            bankService1.AddClientAccount(client2, acc3);
            bankService1.AddClientAccount(client2, acc4);

            //проверка money transfer
            /*
            Console.WriteLine("\n-------------Before money transfer------------\n");
            Console.WriteLine($"Account1 ammount={acc1.Ammount}");
            Console.WriteLine($"Account2 ammount={acc2.Ammount}");

            var exchHandler = new Func<double, Currency, Currency, double>(exchange1.ExchangeCurrency);

            Console.WriteLine("\n---------------Money transfer #1---------------\n");
            bankService1.MoneyTransfer(18, acc1, acc2, exchHandler);
            Console.WriteLine($"Account1 ammount={acc1.Ammount}");
            Console.WriteLine($"Account2 ammount={acc2.Ammount}");

            Console.WriteLine("\n---------------Money transfer #2---------------\n");
            bankService1.MoneyTransfer(270, acc1, acc2, exchHandler);
            Console.WriteLine($"Account1 ammount={acc1.Ammount}");
            Console.WriteLine($"Account2 ammount={acc2.Ammount}");*/

            Console.ReadKey();
        }
    }
}
