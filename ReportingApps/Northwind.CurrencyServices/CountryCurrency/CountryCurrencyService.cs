using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Northwind.CurrencyServices.CountryCurrency
{
    /// <inheritdoc/>
    public class CountryCurrencyService : ICountryCurrencyService
    {
        private readonly string currencySource = "https://restcountries.eu/rest/v2";

        /// <inheritdoc/>
        public async Task<LocalCurrency> GetLocalCurrencyByCountry(string countryName)
        {
            if (countryName is null)
            {
                throw new ArgumentNullException(nameof(countryName));
            }

            using (var client = new HttpClient())
            {
                var responses = await client.GetStringAsync($"{this.currencySource}/name/{countryName}?fields=currencies;name;");

                var localCurrencyInfo = JsonSerializer.Deserialize<CurrencyCountryInfo[]>(responses).Last();

                var localCurrency = new LocalCurrency()
                {
                    CountryName = localCurrencyInfo.FullName,
                    CurrencySymbol = localCurrencyInfo.Currencies.First()["symbol"],
                    CurrencyCode = localCurrencyInfo.Currencies.First()["code"],
                };

                return localCurrency;
            }
        }
    }
}
