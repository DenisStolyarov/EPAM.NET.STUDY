using System;
using System.Globalization;
using System.Threading.Tasks;
using Northwind.CurrencyServices.CountryCurrency;
using Northwind.CurrencyServices.CurrencyExchange;
using Northwind.ReportingServices.OData.ProductReports;
using Northwind.ReportingServices.ProductReports;

namespace ReportingApp
{
    /// <summary>
    /// Program class.
    /// </summary>
    public sealed class Program
    {
        private const string NorthwindServiceUrl = "https://services.odata.org/V3/Northwind/Northwind.svc";
        private const string CurrentProductsReport = "current-products";
        private const string MostExpensiveProductsReport = "most-expensive-products";
        private const string PriceLessThenProductsReport = "price-less-then-products";
        private const string PriceBetweenProducts = "price-between-products";
        private const string PriceAboveAverageProducts = "price-above-average-products";
        private const string UnitsInStockDeficit = "units-in-stock-deficit";
        private const string CurrentProductsLocalPrices = "current-products-local-prices";
        private const string AccessKey = "33e22dfaf746fee120ba7801a8589a08";

        /// <summary>
        /// A program entry point.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                ShowHelp();
                return;
            }

            var reportName = args[0];

            if (string.Equals(reportName, CurrentProductsReport, StringComparison.InvariantCultureIgnoreCase))
            {
                await ShowCurrentProducts();
                return;
            }
            else if (string.Equals(reportName, MostExpensiveProductsReport, StringComparison.InvariantCultureIgnoreCase))
            {
                if (args.Length > 1 && int.TryParse(args[1], out int count))
                {
                    await ShowMostExpensiveProducts(count);
                    return;
                }
            }
            else if (string.Equals(reportName, PriceLessThenProductsReport, StringComparison.InvariantCultureIgnoreCase))
            {
                if (args.Length > 1 && decimal.TryParse(args[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal price))
                {
                    await ShowPriceLessThenProducts(price);
                    return;
                }
            }
            else if (string.Equals(reportName, PriceLessThenProductsReport, StringComparison.InvariantCultureIgnoreCase))
            {
                if (args.Length > 1 && decimal.TryParse(args[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal price))
                {
                    await ShowPriceLessThenProducts(price);
                    return;
                }
            }
            else if (string.Equals(reportName, PriceBetweenProducts, StringComparison.InvariantCultureIgnoreCase))
            {
                if (args.Length > 2
                    && decimal.TryParse(args[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal minPrice)
                    && decimal.TryParse(args[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal maxPrice))
                {
                    await ShowPriceBetweenProducts(minPrice, maxPrice);
                    return;
                }
            }
            else if (string.Equals(reportName, PriceAboveAverageProducts, StringComparison.InvariantCultureIgnoreCase))
            {
                await ShowPriceAboveAverageProducts();
                return;
            }
            else if (string.Equals(reportName, UnitsInStockDeficit, StringComparison.InvariantCultureIgnoreCase))
            {
                await ShowUnitsInStockDeficitProducts();
                return;
            }
            else if (string.Equals(reportName, CurrentProductsLocalPrices, StringComparison.InvariantCultureIgnoreCase))
            {
                await ShowCurrentProductsLocalPrices();
                return;
            }
            else
            {
                ShowHelp();
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("\tReportingApp.exe <report> <report-argument1> <report-argument2> ...");
            Console.WriteLine();
            Console.WriteLine("Reports:");
            Console.WriteLine($"\t{CurrentProductsReport}\t\tShows current products.");
            Console.WriteLine($"\t{MostExpensiveProductsReport}\t\tShows specified number of the most expensive products.");
        }

        private static async Task ShowCurrentProducts()
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetCurrentProducts();
            PrintProductReport("current products:", report);
        }

        private static async Task ShowMostExpensiveProducts(int count)
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetMostExpensiveProductsReport(count);
            PrintProductReport($"{count} most expensive products:", report);
        }

        private static async Task ShowPriceLessThenProducts(decimal price)
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetPriceLessThenProductsReport(price);
            PrintProductReport($"products with price less then {price}:", report);
        }

        private static async Task ShowPriceBetweenProducts(decimal minPrice, decimal maxPrice)
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetPriceBetweenProducts(minPrice, maxPrice);
            PrintProductReport($"products with price between {minPrice} and {maxPrice}", report);
        }

        private static async Task ShowPriceAboveAverageProducts()
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetPriceAboveAverageProducts();
            PrintProductReport($"products with price above average:", report);
        }

        private static async Task ShowUnitsInStockDeficitProducts()
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = await service.GetUnitsInStockDeficitProducts();
            PrintProductReport($"stock products are in deficit:", report);
        }

        private static async Task ShowCurrentProductsLocalPrices()
        {
            var service = new ProductReportService(new Uri(NorthwindServiceUrl));
            var report = new CurrentProductLocalPriceReport(service, new CurrencyExchangeService(AccessKey), new CountryCurrencyService());
            await report.PrintReport();
        }

        private static void PrintProductReport(string header, ProductReport<ProductPrice> productReport)
        {
            Console.WriteLine($"Report - {header}");
            foreach (var reportLine in productReport.Products)
            {
                Console.WriteLine("{0}, {1}", reportLine.Name, reportLine.Price);
            }
        }
    }
}
