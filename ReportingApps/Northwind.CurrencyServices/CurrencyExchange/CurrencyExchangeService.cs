using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Northwind.CurrencyServices.CurrencyExchange
{
    /// <summary>
    /// Represents a service that exchanges currency.
    /// </summary>
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly string currencySource = "http://www.apilayer.net/api/live?access_key=";
        private readonly string accessKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyExchangeService"/> class.
        /// </summary>
        /// <param name="accesskey">Key to access the service.</param>
        public CurrencyExchangeService(string accesskey)
        {
            this.accessKey = !string.IsNullOrWhiteSpace(accesskey) ? accesskey : throw new ArgumentException("Access key is invalid.", nameof(accesskey));
        }

        /// <summary>
        /// Get a currency exchange rate.
        /// </summary>
        /// <param name="baseCurrency">The base curency.</param>
        /// <param name="exchangeCurrency">The currency exchange result.</param>
        /// <returns>Converted currency.</returns>
        public async Task<decimal> GetCurrencyExchangeRate(string baseCurrency, string exchangeCurrency)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStreamAsync(this.currencySource + this.accessKey);
                var rateInfo = await JsonSerializer.DeserializeAsync<CurrencyRateInfo>(response);
                var usd = string.Equals(rateInfo.SourceCurrency, baseCurrency, StringComparison.InvariantCulture)
                    ? 1M : (decimal)(1 / rateInfo.CurrencyQuotes[rateInfo.SourceCurrency + baseCurrency]);
                var exchangeRate = usd * rateInfo.CurrencyQuotes[rateInfo.SourceCurrency + exchangeCurrency];

                return exchangeRate;
            }
        }
    }
}
