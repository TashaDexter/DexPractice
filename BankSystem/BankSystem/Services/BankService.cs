using BankSystem.Exceptions;
using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BankSystem.Services
{
    public class BankService
    {
        private List<Client> _clients;
        private List<Employee> _employees;
        private Dictionary<Client, List<Account>> _clientAccounts;
        public Func<Currency, Currency, double, double> ExchangeHandler;
        public BankService()
        {
            Clients=new List<Client>();
            Employees = new List<Employee>();
            _clientAccounts = new Dictionary<Client, List<Account>>();
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

                    //счетчик для проверки повторяющихся клиентов в файле
                    int counter = 0;
                    //проверка, существует ли клиент с заданным passportID в файле
                    using (FileStream fsClients = new FileStream($"{ pathClients}\\Clients.txt", FileMode.OpenOrCreate))
                    {
                        byte[] arrayReadClients = new byte[fsClients.Length];
                        fsClients.Read(arrayReadClients, 0, arrayReadClients.Length);
                        string clientsText = System.Text.Encoding.Default.GetString(arrayReadClients);
                        string[] lines = clientsText.Split('\n');
                        
                        foreach (var l in lines)
                        {
                            string[] words = l.Split(' ');
                            if (words[0] == client.PassportID.ToString())
                            {
                                Console.WriteLine($"Error! Client with passportID={words[0]} exists!");
                                counter++;
                            }
                        }  
                    }
                    //если клиента в файле нет (то есть счетчик равен нулю), то дописать в файл с новой строки
                    if (counter == 0)
                    {
                        string clientText = client.PassportID + " " + client.FirstName + ' ' + client.LastName + ' ' + client.Age ;

                        using (FileStream fsClients = new FileStream($"{ pathClients}\\Clients.txt", FileMode.Append))
                        {
                            byte[] arrayClients = System.Text.Encoding.Default.GetBytes(clientText + "\n");
                            fsClients.Write(arrayClients, 0, arrayClients.Length);
                            fsClients.Close();
                        }
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
                    string pathEmployee = Directory.GetCurrentDirectory() + "\\Employees";
                    DirectoryInfo directoryInfo = new DirectoryInfo(pathEmployee);

                    if (!directoryInfo.Exists)
                    {
                        directoryInfo.Create();
                    }

                    //счетчик для проверки повторяющихся сотрудников в файле
                    int counter = 0;
                    //проверка, существует ли сотрудник с заданным passportID в файле
                    using (FileStream fsEmployee = new FileStream($"{pathEmployee}\\Employees.txt", FileMode.OpenOrCreate))
                    {
                        byte[] arrayReadEmployees = new byte[fsEmployee.Length];
                        fsEmployee.Read(arrayReadEmployees, 0, arrayReadEmployees.Length);
                        string employeesText = System.Text.Encoding.Default.GetString(arrayReadEmployees);
                        string[] lines = employeesText.Split('\n');

                        foreach (var l in lines)
                        {
                            string[] words = l.Split(' ');
                            if (words[0] == employee.PassportID.ToString())
                            {
                                Console.WriteLine($"Error! Employee with passportID={words[0]} exists!");
                                counter++;
                            }
                        }
                    }
                    //если сотрудника в файле нет (то есть счетчик равен нулю), то дописать в файл с новой строки
                    if (counter == 0)
                    {
                        string employeeText = employee.PassportID + " " + employee.FirstName + " " + employee.LastName + " " + employee.Age + " " + employee.Position;

                        using (FileStream fsClients = new FileStream($"{pathEmployee}\\Employees.txt", FileMode.Append))
                        {
                            byte[] arrayEmployees = System.Text.Encoding.Default.GetBytes(employeeText + "\n");
                            fsClients.Write(arrayEmployees, 0, arrayEmployees.Length);
                            fsClients.Close();
                        }
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
            return FindFromFile<T>(passportID);
            
            //закомментировала для проверки поиска из файла
            /*var client=FindPerson(Clients, passportID);
            var employee = FindPerson(Employees, passportID);
            if (client != null)
                return client;
            if (employee != null)
                return employee;
            return null;*/
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

        private IPerson FindFromFile<T>(int passportID) where T:IPerson
        {
            string pathClients = Directory.GetCurrentDirectory() + "\\Clients";
            DirectoryInfo directoryInfo = new DirectoryInfo(pathClients);

            if (!directoryInfo.Exists)
            {
                Console.WriteLine("Directory doesn't exist!");
            }

            using (FileStream fsClients = new FileStream($"{ pathClients}\\Clients.txt", FileMode.OpenOrCreate))
            {
                byte[] arrayReadClients = new byte[fsClients.Length];
                fsClients.Read(arrayReadClients, 0, arrayReadClients.Length);
                string clientsText = System.Text.Encoding.Default.GetString(arrayReadClients);
                string[] lines = clientsText.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);

                foreach (var l in lines)
                {
                    if (l != "")
                    {
                        string[] words = l.Split(' ');
                        int passID = Convert.ToInt32(words[0]);
                        string firstName = words[1];
                        string lastName = words[2];
                        int age = Convert.ToInt32(words[3]);
                        if (passID == passportID)
                            return new Client() { PassportID = passID, FirstName = firstName, LastName = lastName, Age = age };
                    }
                }
            }

            string pathEmployees = Directory.GetCurrentDirectory() + "\\Employees";
           directoryInfo = new DirectoryInfo(pathClients);

            if (!directoryInfo.Exists)
            {
                Console.WriteLine("Directory doesn't exist!");
            }

            using (FileStream fsEmployees = new FileStream($"{ pathEmployees}\\Employees.txt", FileMode.OpenOrCreate))
            {
                byte[] arrayReadEmployees = new byte[fsEmployees.Length];
                fsEmployees.Read(arrayReadEmployees, 0, arrayReadEmployees.Length);
                string employeesText = System.Text.Encoding.Default.GetString(arrayReadEmployees);
                string[] lines =employeesText.Split('\n');

                foreach (var l in lines)
                {
                    string[] words = l.Split(' ');
                    int passID = Convert.ToInt32(words[0]);
                    string firstName = words[1];
                    string lastName = words[2];
                    int age = Convert.ToInt32(words[3]);
                    if (passID == passportID)
                        return new Employee() { PassportID = passID, FirstName = firstName, LastName = lastName, Age = age };
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

            using (FileStream fsClientAccounts = new FileStream($"{pathDictionary}\\ClientAccounts.txt", FileMode.OpenOrCreate))
            {
                //получить словарь из текста
                byte[] arrayReadClientAccounts = new byte[fsClientAccounts.Length];
                fsClientAccounts.Read(arrayReadClientAccounts, 0, arrayReadClientAccounts.Length);
                string clientAccountsText = System.Text.Encoding.Default.GetString(arrayReadClientAccounts);
                var dictionary=GetDictionaryFromText(clientAccountsText);
            }

            if (Clients.Contains(client))
            {
                if (!_clientAccounts.ContainsKey(client))
                {
                    _clientAccounts.Add(client, new List<Account> { account });
                }
                else
                {
                    _clientAccounts[client].Add(account);
                }
            }
            else
            {
                AddClient(client);
                if (!_clientAccounts.ContainsKey(client))
                {
                    _clientAccounts.Add(client, new List<Account> { account });
                }
                else
                {
                    _clientAccounts[client].Add(account);
                }
            }

            var text = GetTextFromDictionary(_clientAccounts);
            using (FileStream fsClientAccounts = new FileStream($"{pathDictionary}\\ClientAccounts.txt", FileMode.Create))
            {
                byte[] arrayText = System.Text.Encoding.Default.GetBytes(text);
                fsClientAccounts.Write(arrayText, 0, arrayText.Length);
            }
        }

        public Dictionary<Client, List<Account>> GetDictionaryFromText(string text)
        {
            Dictionary<Client, List<Account>> dictionary = new Dictionary<Client, List<Account>>();
            var separator = "\n---\n";
            string[] pairKeyValue = text.Split(separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in pairKeyValue)
            {
                string[] lines = pair.Trim().Split(':');

                string key = lines[0];
                //values - строка, которая включает в себя все аккаунты одной строкой
                string values = lines[1];
                //value - аккаунты, разбитые на строки через разделитель ';'
                string[] value = values.Trim().Split(';', (char)StringSplitOptions.RemoveEmptyEntries);

                string[] paramsClient = key.Split(' ');
                int passportID = Convert.ToInt32(paramsClient[0]);
                string firstName = paramsClient[1];
                string lastName = paramsClient[2];
                int age = Convert.ToInt32(paramsClient[3]);
                Client client = new Client() { PassportID = passportID, FirstName = firstName, LastName = lastName, Age = age };

                List<Account> accounts = new List<Account>();

                foreach (var v in value)
                {
                    if (v != "")
                    {
                        string[] paramsAccount = v.Trim(' ').Split(' ');
                        double ammount = Convert.ToDouble(paramsAccount[0]);
                        string type = paramsAccount[1];
                        double valueInDollars = Convert.ToDouble(paramsAccount[2]);
                        Currency currency = null;
                        switch (type)
                        {
                            case "RUB":
                                {
                                    currency = new Ruble() { Type = type, ValueInDollars = valueInDollars };
                                    break;
                                }
                            case "EUR":
                                {
                                    currency = new Euro() { Type = type, ValueInDollars = valueInDollars };
                                    break;
                                }
                            case "USD":
                                {
                                    currency = new Dollar() { Type = type, ValueInDollars = valueInDollars };
                                    break;
                                }
                        }
                        accounts.Add(new Account() { Ammount = ammount, Currency = currency });
                    }
                }

                if(!dictionary.ContainsKey(client))
                    dictionary.Add(client, accounts);
            }

            return dictionary;
        }

        public string GetTextFromDictionary(Dictionary<Client, List<Account>> dictionary)
        {
            string text="";
            var separator = "\n---\n";

            foreach (var dict in dictionary)
            {
                Client client = dict.Key;
                List<Account> accounts = dict.Value;
                var keyText = client.PassportID + " " + client.FirstName + " " + client.LastName + " " + client.Age;
                var valueText = "";
                foreach (var account in accounts)
                    valueText+=account.Ammount + " " + account.Currency.Type + " " + account.Currency.ValueInDollars+" ; ";
                text += keyText + " : " + valueText + separator;
            }
            return text;
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
