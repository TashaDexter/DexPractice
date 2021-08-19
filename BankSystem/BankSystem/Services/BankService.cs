using BankSystem.Exceptions;
using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace BankSystem.Services
{
    public class BankService
    {
        private List<Client> _clients;
        private List<Employee> _employees;
        private Dictionary<int, List<Account>> _clientAccounts;
        public Func<Currency, Currency, double, double> ExchangeHandler;
        public BankService()
        {
            Clients=new List<Client>();
            Employees = new List<Employee>();
            _clientAccounts = new Dictionary<int, List<Account>>();
        }
        public List<Client> Clients
        {
            get
            {
                return _clients;
            }
            set
            {
                _clients = value;
            }
        }

        public List<Employee> Employees
        {
            get
            {
                return _employees ;
            }
            set
            {
                _employees = value;
            }
        }

        public void Add<T>(T person) where T : IPerson
        {
            if (person is Client)
            {
                var client = person as Client;
                AddClient(client);
            }

            if (person is Employee)
            {
                var employee = person as Employee;
                AddEmployee(employee);
            }
        }
        private void AddClient(Client client)
        {
            try
            {
                if (client.Age < 18)
                    throw new MinorAgeException("Error! Attempting to add a minor age person.");
                else

                {
                    _clients.Add(client);

                    //создание пути в bin/Debug/Clients/
                    string pathClients = Directory.GetCurrentDirectory() + "\\Clients";
                    DirectoryInfo directoryInfo = new DirectoryInfo(pathClients);

                    if (!directoryInfo.Exists)
                    {
                        directoryInfo.Create();
                    }

                    var serClients = JsonConvert.SerializeObject(_clients);

                    using (FileStream fsClients = new FileStream($"{ pathClients}\\Clients.txt", FileMode.Create))
                    {
                        byte[] arrayClients = System.Text.Encoding.Default.GetBytes(serClients);
                        fsClients.Write(arrayClients, 0, arrayClients.Length);
                        fsClients.Close();
                    }
                }
            }
            catch (MinorAgeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void AddEmployee(Employee employee)
        {
            try
            {
                if (employee.Age < 18)
                    throw new MinorAgeException("Error! Attempting to add a minor age person.");
                else
                {
                    _employees.Add(employee);

                    //создание пути в bin/Debug/Employees
                    string pathEmployees = Directory.GetCurrentDirectory() + "\\Employees";
                    DirectoryInfo directoryInfo = new DirectoryInfo(pathEmployees);

                    if (!directoryInfo.Exists)
                    {
                        directoryInfo.Create();
                    }

                    var serEmployees= JsonConvert.SerializeObject(_employees);

                   using (FileStream fsClients = new FileStream($"{ pathEmployees}\\Employees.txt", FileMode.Create))
                    {
                        byte[] arrayEmployees = System.Text.Encoding.Default.GetBytes(serEmployees);
                        fsClients.Write(arrayEmployees, 0, arrayEmployees.Length);
                        fsClients.Close();
                    }
                }
            }
            catch (MinorAgeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public IPerson Find<T>(int passportID) where T : IPerson
        {
            //поиск из файла
            return FindFromFile<T>(passportID);
            
            //поиск через списки
            var client=FindPerson(Clients, passportID);
            var employee = FindPerson(Employees, passportID);
            if (client != null)
                return client;
            if (employee != null)
                return employee;
            return null;
        }

        private IPerson FindPerson<U>(List<U> list, int passportID) where U : IPerson
        {
            IEnumerable<IPerson> query = (IEnumerable<IPerson>)(from l in list
                                                                where l.PassportID == passportID
                                                                select l);
            if (query.Any())
            {
                return query.First();
            }

            return null;
        }

        private IPerson FindFromFile<T>(int passportID) where T : IPerson
        {
            var client = FindClientFromFile(passportID);
            var employee = FindEmployeeFromFile(passportID);
            if (!(client is null))
                return client;
            if (!(employee is null))
                return employee;
            return null;
        }

        private Client FindClientFromFile(int passportID)
        {
            string pathClients = Directory.GetCurrentDirectory() + "\\Clients";
            DirectoryInfo directoryInfo = new DirectoryInfo(pathClients);

            if (!directoryInfo.Exists)
            {
                Console.WriteLine("Directory doesn't exist!");
            }
            else
                using (FileStream fsClients = new FileStream($"{ pathClients}\\Clients.txt", FileMode.OpenOrCreate))
                {
                    byte[] arrayReadClients = new byte[fsClients.Length];
                    fsClients.Read(arrayReadClients, 0, arrayReadClients.Length);
                    string clientsText = System.Text.Encoding.Default.GetString(arrayReadClients);

                    List<Client> clients=JsonConvert.DeserializeObject<List<Client>>(clientsText);

                    foreach (var client in clients)
                    {
                        if (client.PassportID == passportID)
                            return client;
                    }
                }
            return null;
        }

        private Employee FindEmployeeFromFile(int passportID)
        {
            string pathEmployees = Directory.GetCurrentDirectory() + "\\Employees";
            DirectoryInfo directoryInfo = new DirectoryInfo(pathEmployees);

            if (!directoryInfo.Exists)
            {
                Console.WriteLine("Directory doesn't exist!");
            }
            else
            {
                using (FileStream fsEmployees = new FileStream($"{ pathEmployees}\\Employees.txt", FileMode.OpenOrCreate))
                {
                    byte[] arrayReadEmployees = new byte[fsEmployees.Length];
                    fsEmployees.Read(arrayReadEmployees, 0, arrayReadEmployees.Length);
                    string employeesText = System.Text.Encoding.Default.GetString(arrayReadEmployees);

                    List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(employeesText);

                    foreach (var employee in employees)
                    {
                        if (employee.PassportID == passportID)
                            return employee;
                    }
                }
            }
            return null;
        }

        public void AddClientAccount(Client client, Account account)
        {
            //создание директории в bin/Debug/ClientAccounts/ для Dictionary<Client, List<Account>> _clientAccounts;
            string pathDictionary = Directory.GetCurrentDirectory() + "\\ClientAccounts";
            DirectoryInfo directoryInfo = new DirectoryInfo(pathDictionary);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            if (!Clients.Contains(client))
                AddClient(client);

            if (!_clientAccounts.ContainsKey(client.PassportID))
            {
                _clientAccounts.Add(client.PassportID, new List<Account> { account });
            }
            else
            {
                _clientAccounts[client.PassportID].Add(account);
            }

            RewriteDictionary(pathDictionary);
        }

        private void RewriteDictionary(string pathDictionary)
        {
            var textClientAccount = JsonConvert.SerializeObject(_clientAccounts);
            using (FileStream fsClientAccounts = new FileStream($"{pathDictionary}\\ClientAccounts.txt", FileMode.Create))
            {
                byte[] arrayText = System.Text.Encoding.Default.GetBytes(textClientAccount);
                fsClientAccounts.Write(arrayText, 0, arrayText.Length);
            }
        }

        public void MoneyTransfer(double ammount, Account donorAccount, Account recipientAccount, Func<double, Currency, Currency, double> exchangeHandler)
        {
            try
            {
                var conversionResult = exchangeHandler.Invoke(ammount, donorAccount.Currency, recipientAccount.Currency);
                if (donorAccount.Ammount - ammount < 0)
                {
                    throw new NotEnoughFundsException("Error! Not enough funds for transfer!");
                }
                else
                {
                    Console.WriteLine($"Money transfer is in progress." +
                        $"\nAmmount: {ammount}" +
                        $"\nDonorAccount currency: {donorAccount.Currency}" +
                        $"\nRecipientAccount: {recipientAccount.Currency}" +
                        $"\nConversion result:{conversionResult}");
                    donorAccount.Ammount -= ammount;
                    recipientAccount.Ammount += conversionResult;
                }
            }
            catch (NotEnoughFundsException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
