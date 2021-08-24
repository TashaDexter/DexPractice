using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BankSystem.Models;
using BankSystem.Services;

namespace FindClients.Tests
{
    public class FindClientsTests
    {
        [Fact]
        public void FindClient_null()
        {
            var exception = typeof(NullReferenceException);
            Type result = null;

            BankService bankService = new BankService();
            Client client = null;

            try
            {
                bankService.Find<Client>(client.PassportID);
            }
            catch (Exception e)
            {
                result = e.GetType();
            }
            Assert.Equal(exception, result);
        }

        [Fact]
        public void FindClient_ID_VP1588_FN_Nick_LN_Pane_Age_45()
        {    
            Client client = new Client() { PassportID= "VP1588", FirstName="Nick", LastName="Pane", Age=45 };
            BankService bankService = new BankService();

            bankService.Add<Client>(client);
            IPerson result=bankService.Find<Client>(client.PassportID);

            Assert.Equal(client, result);
        }
    }
}
