using System.Threading.Tasks;
using Northwind.CurrencyServices.CountryCurrency;
using Northwind.CurrencyServices.CurrencyExchange;

namespace Northwind.ReportingServices.ProductReports
{
    /// <summary>
    /// Represents a service that produces product-related reports.
    /// </summary>
    public interface IProductReportService
    {
        /// <summary>
        /// Gets a product report with all current products.
        /// </summary>
        /// <returns>Returns <see cref="ProductReport{T}"/>.</returns>
        Task<ProductReport<ProductPrice>> GetCurrentProducts();

        /// <summary>
        /// Gets a product report with most expensive products.
        /// </summary>
        /// <param name="count">Items count.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        Task<ProductReport<ProductPrice>> GetMostExpensiveProductsReport(int count);

        /// <summary>
        /// Gets a product report with products where price less then max price.
        /// </summary>
        /// <param name="maxPrice">Max price.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        Task<ProductReport<ProductPrice>> GetPriceLessThenProductsReport(decimal maxPrice);

        /// <summary>
        /// Gets a product report with products where price between min ands max prices.
        /// </summary>
        /// <param name="minPrice">Min price.</param>
        /// <param name="maxPrice">Max price.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        Task<ProductReport<ProductPrice>> GetPriceBetweenProducts(decimal minPrice, decimal maxPrice);

        /// <summary>
        /// Gets a product report with products where price above average.
        /// </summary>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        Task<ProductReport<ProductPrice>> GetPriceAboveAverageProducts();

        /// <summary>
        /// Gets a product report with products where price above average.
        /// </summary>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        Task<ProductReport<ProductPrice>> GetUnitsInStockDeficitProducts();

        /// <summary>
        /// Gets a product report with local country information.
        /// </summary>
        /// <param name="countryCurrencyService">Service to get country info.</param>
        /// <param name="currencyExchangeService">Service to get currency info.</param>
        /// <returns>Returns <see cref="ProductReport{ProductLocalPrice}"/>.</returns>
        Task<ProductReport<ProductLocalPrice>> GetCurrentProductsWithLocalCurrencyReport(ICountryCurrencyService countryCurrencyService, ICurrencyExchangeService currencyExchangeService);
    }
}
