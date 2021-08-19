using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;
using Bogus;

namespace BankSystem.Services
{
    public class FakeDataService
    {
        public Client GenerateClient()
        {
            var client = new Faker<Client>()
               .StrictMode(true)
                .RuleFor(x => x.PassportID, f => f.Random.Int(1000, 9999))
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Age, f => f.Random.Int(19, 50));
            return client.Generate();
        }
        public Employee GenerateEmployee()
        {
            var employee = new Faker<Employee>()
               .StrictMode(true)
                .RuleFor(x => x.PassportID, f => f.Random.Int(1000, 9999))
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Age, f => f.Random.Int(19, 50));
            return employee.Generate();
        }
    }
}
