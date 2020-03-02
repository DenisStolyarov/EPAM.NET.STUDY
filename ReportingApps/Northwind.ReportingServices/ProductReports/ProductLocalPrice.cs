using System.Globalization;
using System.Text;

namespace Northwind.ReportingServices.ProductReports
{
    /// <summary>
    /// Represents a product report line.
    /// </summary>
    public class ProductLocalPrice
    {
        private const string Format = "{0:0.##}";

        /// <summary>
        /// Gets or sets a product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets a product country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets a local price.
        /// </summary>
        public decimal LocalPrice { get; set; }

        /// <summary>
        /// Gets or sets a currency symbol.
        /// </summary>
        public string CurrencySymbol { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"{this.Name}, ")
                   .Append($"{string.Format(CultureInfo.InvariantCulture, Format, this.Price)}$, ")
                   .Append($"{this.Country}, ")
                   .Append($"{string.Format(CultureInfo.InvariantCulture, Format, this.LocalPrice)}{this.CurrencySymbol}");
            return builder.ToString();
        }
    }
}
