using BankSystem.Models;

namespace BankSystem.Services
{
    public interface IExchange
    {
        double ExchangeCurrency<T, U>(T currencyIn, U currencyOut, double ammount) where T : Currency where U : Currency;
    }
}
