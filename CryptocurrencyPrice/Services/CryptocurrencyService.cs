using CryptocurrencyPrice.Interface;
using CryptocurrencyPrice.Models;

namespace CryptocurrencyPrice.Service
{
    public class CryptocurrencyService : ICryptocurrencyService
    {
        private readonly HttpClient _httpClient;

        public CryptocurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Cryptocurrency>> GetLastQuotes(string url)
        {
            IEnumerable<Cryptocurrency> cryptocurrencies = new List<Cryptocurrency>();
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var dataResponse = await response.Content.ReadFromJsonAsync<GetLastCurrencyResponse>();
                    cryptocurrencies = dataResponse.Data;
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return cryptocurrencies;
        }

        public async Task<Cryptocurrency> PriceConversion(string url)
        {
            Cryptocurrency cryptocurrency = new Cryptocurrency();
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var conversionResponse = await response.Content.ReadFromJsonAsync<PriceConversionResponse>();
                    cryptocurrency = conversionResponse.Data;
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return cryptocurrency;
        }
    }
}
