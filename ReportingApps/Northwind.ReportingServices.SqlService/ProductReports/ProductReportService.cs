using System;
using System.Data.SqlClient;
using Northwind.ReportingServices.ProductReports;
using System.Threading.Tasks;
using Northwind.CurrencyServices.CountryCurrency;
using Northwind.CurrencyServices.CurrencyExchange;

namespace Northwind.ReportingServices.SqlService.ProductReports
{
    public class ProductReportService : IProductReportService
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";

        public Task<ProductReport<ProductPrice>> GetCurrentProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductReport<ProductLocalPrice>> GetCurrentProductsWithLocalCurrencyReport(ICountryCurrencyService countryCurrencyService, ICurrencyExchangeService currencyExchangeService)
        {
            throw new NotImplementedException();
        }

        public Task<ProductReport<ProductPrice>> GetMostExpensiveProductsReport(int count)
        {
            throw new NotImplementedException();
        }

        public Task<ProductReport<ProductPrice>> GetPriceAboveAverageProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductReport<ProductPrice>> GetPriceBetweenProducts(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public Task<ProductReport<ProductPrice>> GetPriceLessThenProductsReport(decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public Task<ProductReport<ProductPrice>> GetUnitsInStockDeficitProducts()
        {
            throw new NotImplementedException();
        }
    }
}
