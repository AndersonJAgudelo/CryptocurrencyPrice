using CryptocurrencyPrice.Models;

namespace CryptocurrencyPrice.Interface
{
    public interface ICryptocurrencyService
    {
        Task<IEnumerable<Cryptocurrency>> GetLastQuotes(string url);
        Task<Cryptocurrency> PriceConversion(string url);
    }
}
