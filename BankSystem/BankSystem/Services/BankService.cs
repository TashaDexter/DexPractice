using BankSystem.Exceptions;
using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void AddClient(Client client)
        {
            try
            {
                if (client.Age < 18)
                    throw new MinorAgeException("Error! Attempting to add a minor age person.");
                else
                    _clients.Add(client);
            }
            catch (MinorAgeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                if (employee.Age < 18)
                    throw new MinorAgeException("Error! Attempting to add a minor age person.");
                else
                    _employees.Add(employee);
            }
            catch (MinorAgeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public IPerson Find<T>(int passportID) where T : IPerson
        {
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

        public void AddClientAccount(Client client, Account account)
        {
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
