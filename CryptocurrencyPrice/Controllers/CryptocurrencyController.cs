using CryptocurrencyPrice.Interface;
using CryptocurrencyPrice.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptocurrencyPrice.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CryptocurrencyController : ControllerBase
    {
        private readonly ICryptocurrencyService _cryptocurrencyService;
        private readonly IConfiguration _config;

        public CryptocurrencyController(ICryptocurrencyService cryptocurrencyService, IConfiguration config)
        {
            _cryptocurrencyService = cryptocurrencyService;
            _config = config;
        }

        [HttpGet]
        [ActionName("GetLastQuotes")]
        public async Task<IEnumerable<Cryptocurrency>> GetLastQuotes()
        {
            var start = _config.GetValue<string>("QueryStringStart");
            var limit = _config.GetValue<string>("QueryStringlimit");
            var methodUrl = $"cryptocurrency/listings/latest?start={start}&limit={limit}";

            return await _cryptocurrencyService.GetLastQuotes(methodUrl);
        }

        [HttpGet]
        [ActionName("PriceConversion")]
        public async Task<IEnumerable<Cryptocurrency>> PriceConversion(string id, string amount, string symbols)
        {
            List<Cryptocurrency> cryptocurrencies = new List<Cryptocurrency>();
            string[] _symbols = symbols.Split(',');
            var methodUrl = "";
            foreach (var symbol in _symbols)
            {
                methodUrl = $"tools/price-conversion?symbol={symbol}&amount={amount}&convert=usd";
                cryptocurrencies.Add(await _cryptocurrencyService.PriceConversion(methodUrl));
            }

            return cryptocurrencies;
        }
    }
}
