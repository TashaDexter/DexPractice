using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BankSystem.Models;
using BankSystem.Services;
using System.Threading;
using System.Threading.Tasks;

namespace BankSystem.Tests
{
    public class BankServiceTests
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
            var result = bankService.Find<Client>(client.PassportID);

            Assert.Null(result);
        }

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
            Client client = new Client() { PassportID = "VP1588", FirstName = "Nick", LastName = "Pane", Age = 45 };
            BankService bankService = new BankService();

            bankService.Add<Client>(client);
            IPerson result = bankService.Find<Client>(client.PassportID);

            Assert.Equal(client, result);
        }

        //метод переписывает значение аккаунта из двух потоков одновременно
        [Fact]
        public void ChangeAmmountForClientAccount()
        {            
            var locker = new object();
            BankService bankService = new BankService(locker);

            Ruble ruble = new Ruble() { Type = "RUB", ValueInDollars = 0.014 };
            Dollar dollar = new Dollar() { Type = "USD", ValueInDollars = 1 };

            Account acc1 = new Account() { Ammount = 154, Currency = ruble };
            Account acc2 = new Account() { Ammount = 169, Currency = dollar };

            Exchange exchange = new Exchange();

            var exchHandler = new Func<double, Currency, Currency, double>(exchange.ExchangeCurrency);

            var completed = 0;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                lock (locker)
                {
                    bankService.MoneyTransfer(13, acc1, acc2, exchHandler);
                }
                Thread.Sleep(500);
                Interlocked.Increment(ref completed);
            });

            ThreadPool.QueueUserWorkItem(_ => 
            {
                lock (locker)
                {
                    bankService.MoneyTransfer(10, acc1, acc2, exchHandler);
                }
                Interlocked.Increment(ref completed);
            });

            while (completed < 2) // ожидание завершения
            {
            Thread.Sleep(25);
            }

            Assert.Equal(131, acc1.Ammount);
        }
    }
}
