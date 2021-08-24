using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BankSystem.Models;
using BankSystem.Services;
using BankSystem.Exceptions;


namespace AddClients.Tests
{
    public class AddClientTests
    {
        [Fact]
        public void AddClient_ID_VS1548_FN_Jack_LN_Smith_Age_21()
        {
            BankService bankService = new BankService();

            Client client = new Client() { PassportID = "VS1548", FirstName = "Jack", LastName = "Smith", Age = 21 };
            bankService.Add<Client>(client);

            var result = bankService.Find<Client>(client.PassportID);

            Assert.Equal(client, result);
        }

        [Fact]
        public void AddClient_ID_VS1453_FN_Jenny_LN_Collin_Age_9_()
        {
            BankService bankService = new BankService();
            Client client = new Client() { PassportID = "VS1453", FirstName = "Jenny", LastName = "Collin", Age = 9 };

            bankService.Add<Client>(client);
            var result =bankService.Find<Client>(client.PassportID);            

            Assert.Null(result);
        }
    }
}
