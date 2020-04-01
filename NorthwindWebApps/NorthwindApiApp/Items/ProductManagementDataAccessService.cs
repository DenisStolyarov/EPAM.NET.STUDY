using System;
using System.Collections.Generic;
using NorthwindApiApp.Items.Products;
using Northwind.Services.Products;

namespace NorthwindApiApp.Items
{
    public class ProductManagementDataAccessService : IProductManagementService
    {
        private readonly NorthwindDataAccessFactory factory;

        public ProductManagementDataAccessService(NorthwindDataAccessFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public int CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool DestroyProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public IList<Product> LookupProductsByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        public IList<Product> ShowProducts(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public IList<Product> ShowProductsForCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public bool TryShowProduct(int productId, out Product product)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(int productId, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
