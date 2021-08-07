using BankSystem.Models;

namespace BankSystem.Services
{
    public class Exchange : IExchange
    {
        public double ExchangeCurrency<T, U> (T currencyIn, U currencyOut, double ammount) where T: Currency where U:Currency
        {
            return ammount * currencyIn.ValueInDollars / currencyOut.ValueInDollars;
        }
    }
}
