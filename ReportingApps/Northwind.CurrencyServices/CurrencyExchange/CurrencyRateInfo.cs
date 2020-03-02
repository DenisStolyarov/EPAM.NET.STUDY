using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Northwind.CurrencyServices.CurrencyExchange
{
    /// <summary>
    /// Represents info about currency.
    /// </summary>
    public class CurrencyRateInfo
    {
        /// <summary>
        /// Gets or sets source currency.
        /// </summary>
        [JsonPropertyName("source")]
        public string SourceCurrency { get; set; }

        /// <summary>
        /// Gets or sets list of currencies and their prices.
        /// </summary>
        [JsonPropertyName("quotes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Свойства коллекций должны быть доступны только для чтения", Justification = "<Ожидание>")]
        public Dictionary<string, decimal> CurrencyQuotes { get; set; }
    }
}
