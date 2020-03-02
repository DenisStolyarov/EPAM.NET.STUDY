using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Threading.Tasks;
using Northwind.CurrencyServices.CountryCurrency;
using Northwind.CurrencyServices.CurrencyExchange;
using Northwind.ReportingServices.ProductReports;
using NorthwindProduct = NorthwindModel.Product;

namespace Northwind.ReportingServices.OData.ProductReports
{
    /// <summary>
    /// Represents a service that produces product-related reports.
    /// </summary>
    public class ProductReportService : IProductReportService
    {
        private readonly NorthwindModel.NorthwindEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductReportService"/> class.
        /// </summary>
        /// <param name="northwindServiceUri">An URL to Northwind OData service.</param>
        public ProductReportService(Uri northwindServiceUri)
        {
            this.entities = new NorthwindModel.NorthwindEntities(northwindServiceUri ?? throw new ArgumentNullException(nameof(northwindServiceUri)));
        }

        /// <summary>
        /// Gets a product report with all current products.
        /// </summary>
        /// <returns>Returns <see cref="ProductReport{T}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetCurrentProducts()
        {
            DataServiceQueryContinuation<NorthwindProduct> token = null;

            var query = (DataServiceQuery<NorthwindProduct>)this.entities.Products;
            var products = new List<NorthwindProduct>();

            var result = await Task<IEnumerable<NorthwindProduct>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            }) as QueryOperationResponse<NorthwindProduct>;

            products.AddRange(result);

            token = result.GetContinuation();

            while (token != null)
            {
                result = await Task<IEnumerable<NorthwindProduct>>.Factory.FromAsync(this.entities.BeginExecute<NorthwindProduct>(token.NextLinkUri, null, null), (ar) =>
                {
                    return this.entities.EndExecute<NorthwindProduct>(ar);
                }) as QueryOperationResponse<NorthwindProduct>;

                products.AddRange(result);

                token = result.GetContinuation();
            }

            var prices = from p in products
                    where !p.Discontinued
                    orderby p.ProductName
                    select new ProductPrice
                    {
                        Name = p.ProductName,
                        Price = p.UnitPrice ?? 0,
                    };

            return new ProductReport<ProductPrice>(prices);
        }

        /// <summary>
        /// Gets a product report with most expensive products.
        /// </summary>
        /// <param name="count">Items count.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetMostExpensiveProductsReport(int count)
        {
            var query = (DataServiceQuery<ProductPrice>)this.entities.Products.
                Where(p => p.UnitPrice != null).
                OrderByDescending(p => p.UnitPrice.Value).
                Take(count).
                Select(p => new ProductPrice
                {
                    Name = p.ProductName,
                    Price = p.UnitPrice ?? 0,
                });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with products where price less then max price.
        /// </summary>
        /// <param name="maxPrice">Max price.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetPriceLessThenProductsReport(decimal maxPrice)
        {
            var query = (DataServiceQuery<ProductPrice>)this.entities.Products.
                Where(p => p.UnitPrice != null && p.UnitPrice < maxPrice).
                OrderBy(p => p.UnitPrice.Value).
                Select(p => new ProductPrice
                {
                    Name = p.ProductName,
                    Price = p.UnitPrice ?? 0,
                });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with products where price between min ands max prices.
        /// </summary>
        /// <param name="minPrice">Min price.</param>
        /// <param name="maxPrice">Max price.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetPriceBetweenProducts(decimal minPrice, decimal maxPrice)
        {
            var query = (DataServiceQuery<ProductPrice>)this.entities.Products.
                Where(p => p.UnitPrice != null
                    && p.UnitPrice > minPrice
                    && p.UnitPrice < maxPrice).
                OrderBy(p => p.UnitPrice.Value).
                Select(p => new ProductPrice
                {
                    Name = p.ProductName,
                    Price = p.UnitPrice ?? 0,
                });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with products where price above average.
        /// </summary>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetPriceAboveAverageProducts()
        {
            var products = await this.GetCurrentProducts();
            var average = products.Products.Average(i => i.Price);
            var result = products.Products
                .Where(i => i.Price > average)
                .OrderBy(i => i.Price);
            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with products where price above average.
        /// </summary>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetUnitsInStockDeficitProducts()
        {
            var query = (DataServiceQuery<ProductPrice>)this.entities.Products.
                Where(p => p.UnitsInStock < p.UnitsOnOrder).
                OrderBy(p => p.UnitPrice.Value).
                Select(p => new ProductPrice
                {
                    Name = p.ProductName,
                    Price = p.UnitPrice ?? 0,
                });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with local country information.
        /// </summary>
        /// <param name="countryCurrencyService">Service to get country info.</param>
        /// <param name="currencyExchangeService">Service to get currency info.</param>
        /// <returns>Returns <see cref="ProductReport{ProductLocalPrice}"/>.</returns>
        public async Task<ProductReport<ProductLocalPrice>> GetCurrentProductsWithLocalCurrencyReport(ICountryCurrencyService countryCurrencyService, ICurrencyExchangeService currencyExchangeService)
        {
            if (countryCurrencyService is null)
            {
                throw new ArgumentNullException(nameof(countryCurrencyService));
            }

            if (currencyExchangeService is null)
            {
                throw new ArgumentNullException(nameof(currencyExchangeService));
            }

            var query = (DataServiceQuery<ProductLocalPrice>)this.entities.Products.
                Where(p => p.UnitPrice != null).
                OrderBy(p => p.ProductName).
                Select(p => new ProductLocalPrice
                {
                    Name = p.ProductName,
                    Price = p.UnitPrice ?? 0,
                    Country = p.Supplier.Country,
                });

            var localPrices = await Task<ProductLocalPrice[]>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar).ToArray();
            });

            for (int i = 0; i < localPrices.Length; i++)
            {
                var countryInfo = await countryCurrencyService.GetLocalCurrencyByCountry(localPrices[i].Country);
                var currencyExchange = await currencyExchangeService.GetCurrencyExchangeRate("USD", countryInfo.CurrencyCode);
                localPrices[i].Country = countryInfo.CountryName;
                localPrices[i].LocalPrice = localPrices[i].Price * currencyExchange;
                localPrices[i].CurrencySymbol = countryInfo.CurrencySymbol;
            }

            return new ProductReport<ProductLocalPrice>(localPrices);
        }
    }
}
