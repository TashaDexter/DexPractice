using BankSystem.Models;
using BankSystem.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BankService bankService1 = new BankService();
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

            //проверка поиска
            /*
            Console.WriteLine("\nEnter PassportID to search:");
            string passportID = Console.ReadLine();

            IPerson person=bankService1.Find<IPerson>(passportID);
            if (person != null)
            {
                Console.WriteLine($"\nPerson with passportID={passportID}:\n" +
                      $"PassportID={person.PassportID}, " +
                      $"FirstName={person.FirstName}, " +
                      $"LastName={person.LastName}, " +
                      $"Age={person.Age}, " +
                      $"PersonType={ person.GetType()}");
            }
            else
            {
                Console.WriteLine($"Person with passportID={passportID} was not found.");
            }*/

            //проверка добавления в словарь
            Ruble ruble = new Ruble() { Type="RUB",ValueInDollars = 0.014 };
            Euro euro = new Euro() { Type="EUR", ValueInDollars = 1.19 };
            Dollar dollar = new Dollar() { Type="USD", ValueInDollars = 1 };

            Exchange exchange1 = new Exchange();

            Client client1 = generator.GenerateClient();
            Account acc1 = new Account() { Currency = ruble, Ammount = 278.5 };
            Account acc2 = new Account() { Currency = dollar, Ammount = 29.8 };

            Client client2 = generator.GenerateClient();
            Account acc3 = new Account() { Currency = ruble, Ammount = 1578 };
            Account acc4 = new Account() { Currency = euro, Ammount = 4824 };

            /*
            bankService1.AddClientAccount(client1, acc1);
            bankService1.AddClientAccount(client1, acc2);
            bankService1.AddClientAccount(client2, acc3);
            bankService1.AddClientAccount(client2, acc1);
            */

            Assembly asm = Assembly.LoadFrom("D:\\Dex\\DexPractice\\DexPractice\\Figure\\Figure\\bin\\Debug\\Figure.exe");
            Console.WriteLine(asm.FullName);

            Type myType = asm.GetType("Figure.Box", false, true);
            DataExportService dataExportService1 = new DataExportService();
            object obj = Activator.CreateInstance(myType);
            MethodInfo method = myType.GetMethod("Display");
            // вызываем метод, передаем ему значения для параметров и получаем результат
            method.Invoke(obj, new object[] { "тест"});
            //var obj=GenerateNewObject("Figure.Box, Figure");



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

        private static object GenerateNewObject(string type)
        {
            var myType = Type.GetType(type, false, true);
            DataExportService dataExportService1 = new DataExportService();
            if (myType != null)
            {
                //получаем конструктор
                System.Reflection.ConstructorInfo ctor= myType.GetConstructor(new Type[] { });

                //вызываем конструтор
                object Obj = ctor.Invoke(new object[] { });
                dataExportService1.Display(Obj);
                return Obj;
            }
            else
            {
                Console.WriteLine("Класс не найден");
                return null;
            }
        }
    }
}
