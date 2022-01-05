namespace CryptocurrencyPrice.Models
{
    public class Cryptocurrency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public Quote Quote { get; set; }
    }
}