using System;
using System.Threading.Tasks;
using Northwind.CurrencyServices.CountryCurrency;
using Northwind.CurrencyServices.CurrencyExchange;
using Northwind.ReportingServices.ProductReports;

namespace ReportingApp
{
    /// <summary>
    /// Represent information about current product price report.
    /// </summary>
    public class CurrentProductLocalPriceReport
    {
        private readonly IProductReportService productReportService;
        private readonly ICurrencyExchangeService currencyExchangeService;
        private readonly ICountryCurrencyService countryCurrencyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentProductLocalPriceReport"/> class.
        /// </summary>
        /// <param name="productReportService">The report service.</param>
        /// <param name="currencyExchangeService">The currency exchange service.</param>
        /// <param name="countryCurrencyService">The country currency service.</param>
        public CurrentProductLocalPriceReport(IProductReportService productReportService, ICurrencyExchangeService currencyExchangeService, ICountryCurrencyService countryCurrencyService)
        {
            this.productReportService = productReportService ?? throw new ArgumentNullException(nameof(productReportService));
            this.currencyExchangeService = currencyExchangeService ?? throw new ArgumentNullException(nameof(currencyExchangeService));
            this.countryCurrencyService = countryCurrencyService ?? throw new ArgumentNullException(nameof(countryCurrencyService));
        }

        /// <summary>
        /// Prints local country product report.
        /// </summary>
        /// <returns>The local report.</returns>
        public async Task PrintReport()
        {
            var report = await this.productReportService.GetCurrentProductsWithLocalCurrencyReport(this.countryCurrencyService, this.currencyExchangeService);
            Console.WriteLine($"Report - current products with local price:");
            foreach (var reportLine in report.Products)
            {
                Console.WriteLine(reportLine);
            }
        }
    }
}
