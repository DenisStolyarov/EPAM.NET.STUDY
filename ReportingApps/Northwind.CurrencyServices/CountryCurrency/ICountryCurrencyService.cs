using System.Threading.Tasks;

namespace Northwind.CurrencyServices.CountryCurrency
{
    /// <summary>
    /// Represents a service that gets information about country.
    /// </summary>
    public interface ICountryCurrencyService
    {
        /// <summary>
        /// Gets local country currency information.
        /// </summary>
        /// <param name="countryName">The country name.</param>
        /// <returns>The country currency information.</returns>
        Task<LocalCurrency> GetLocalCurrencyByCountry(string countryName);
    }
}
