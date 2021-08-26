using System;
using BankSystem.Models;
using BankSystem.Services;
using Xunit;

namespace BankSystem.Tests
{
    public class ExchangeTests
    {
        //positive test
        [Fact]
        public void ExchangeCurrency_ammount_125_ruble_0_014_dollar_1()
        {

            Exchange exchange = new Exchange();
            Ruble ruble = new Ruble() { Type = "RUB", ValueInDollars = 0.014 };
            Dollar dollar = new Dollar() { Type = "USD", ValueInDollars = 1 };

            var result = exchange.ExchangeCurrency(125, ruble, dollar);

            Assert.Equal(1.75, result);
        }
        //negative test
        [Fact]
        public void ExchangeCurrency_ammount_100_euro_1_19_ruble_0_014()
        {

            Exchange exchange = new Exchange();
            Ruble ruble = new Ruble() { Type = "RUB", ValueInDollars = 0.014 };
            Euro euro = new Euro() { Type = "EUR", ValueInDollars = 1.19 };

            var result = exchange.ExchangeCurrency(125, euro, ruble);

            Assert.Equal(10500, result);
        }
    }
}
