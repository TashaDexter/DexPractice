using BankSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem.Services
{
    public class BankService
    {
        public BankService()
        {
            Clients=new List<Client>();
            Employees = new List<Employee>();
        }
        public List<Client> Clients { get; set; }

        public List<Employee> Employees { get; set; }

        private IPerson FindPerson<U>(List<U> list, int passportID) where U: IPerson
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
    }
}
