using System.Threading.Tasks;

namespace Northwind.CurrencyServices.CurrencyExchange
{
    /// <summary>
    /// Represents a service that exchanges currency.
    /// </summary>
    public interface ICurrencyExchangeService
    {
        /// <summary>
        /// Get a currency exchange rate.
        /// </summary>
        /// <param name="baseCurrency">The base curency.</param>
        /// <param name="exchangeCurrency">The currency exchange result.</param>
        /// <returns>Converted currency.</returns>
        Task<decimal> GetCurrencyExchangeRate(string baseCurrency, string exchangeCurrency);
    }
}
