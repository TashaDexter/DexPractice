using BankSystem.Models;

namespace BankSystem.Services
{
    public class Exchange : IExchange
    {
        public double ExchangeCurrency<T, U> (double ammount, T currencyIn, U currencyOut) where T: Currency where U:Currency
        {
            return ammount * currencyIn.ValueInDollars / currencyOut.ValueInDollars;
        }
    }
}
