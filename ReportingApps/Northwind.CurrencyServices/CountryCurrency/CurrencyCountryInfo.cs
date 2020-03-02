using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Northwind.CurrencyServices.CountryCurrency
{
    /// <summary>
    /// Represents info about country currency.
    /// </summary>
    public class CurrencyCountryInfo
    {
        /// <summary>
        /// Gets or sets country full name.
        /// </summary>
        [JsonPropertyName("name")]
        public string FullName { get; set; }

        [JsonPropertyName("currencies")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "<Ожидание>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Свойства не должны возвращать массивы", Justification = "<Ожидание>")]
        public Dictionary<string, string>[] Currencies { get; set; }
    }
}
