using System;
using System.Threading.Tasks;
using BankSystem.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BankSystem.Services
{
    public class GetActualCurrencyService
    {
        private readonly string _apiKey;
        public GetActualCurrencyService(string apiKey)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        public async Task<CurrencyListResponse> GetCurrencyList()
        {
            HttpResponseMessage responseMessage;
            CurrencyListResponse currencyResponse;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    AuthenticationHeaderValue.Parse(_apiKey);

                responseMessage = await client.GetAsync($"https://currate.ru/api/?get=currency_list&key={_apiKey}");

                responseMessage.EnsureSuccessStatusCode();

                string serializedMessage = await responseMessage.Content.ReadAsStringAsync();
                currencyResponse = JsonConvert.DeserializeObject<CurrencyListResponse> (serializedMessage);
            }

            return currencyResponse;
        }

        public async Task<CurrencyResponse> GetCurrency(string currency)
        {
            HttpResponseMessage responseMessage;
            CurrencyResponse currencyResponse;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    AuthenticationHeaderValue.Parse(_apiKey);

                responseMessage = await client.GetAsync($"https://currate.ru/api/?get=rates&pairs={currency}&key={_apiKey}");

                responseMessage.EnsureSuccessStatusCode();

                string serializedMessage = await responseMessage.Content.ReadAsStringAsync();
                currencyResponse = JsonConvert.DeserializeObject<CurrencyResponse>(serializedMessage);
            }

            return currencyResponse;
        }
    }
}
