using BankSystem.Models;

namespace BankSystem.Services
{
    public interface IExchange
    {
        double ExchangeCurrency<T, U>(double ammount, T currencyIn, U currencyOut) where T : Currency where U : Currency;
    }
}
