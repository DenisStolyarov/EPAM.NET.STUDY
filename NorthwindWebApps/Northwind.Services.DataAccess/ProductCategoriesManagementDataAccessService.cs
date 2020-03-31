using System;
using System.Collections.Generic;
using System.Linq;
using Northwind.DataAccess;
using Northwind.DataAccess.Produts;
using Northwind.Services.Products;

namespace Northwind.Services.DataAccess
{
    public class ProductCategoriesManagementDataAccessService : IProductCategoryManagementService
    {
        private readonly NorthwindDataAccessFactory factory;

        public ProductCategoriesManagementDataAccessService(NorthwindDataAccessFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public int CreateCategory(ProductCategory productCategory)
        {
            return this.factory.GetProductCategoryDataAccessObject()
                .InsertProductCategory(productCategory.ToCategoryTDO());
        }

        public bool DestroyCategory(int categoryId)
        {
            return this.factory.GetProductCategoryDataAccessObject()
                .DeleteProductCategory(categoryId);
        }

        public IList<ProductCategory> LookupCategoriesByName(IList<string> names)
        {
            return this.factory.GetProductCategoryDataAccessObject()
                .SelectProductCategoriesByName(names)
                .Select(i => i.ToCategory())
                .ToList();
        }

        public IList<ProductCategory> ShowCategories(int offset, int limit)
        {
            return this.factory.GetProductCategoryDataAccessObject()
                .SelectProductCategories(offset, limit)
                .Select(i => i.ToCategory())
                .ToList();
        }

        public bool TryShowCategory(int categoryId, out ProductCategory productCategory)
        {
            productCategory = null;
            var searchResult = this.factory
                .GetProductCategoryDataAccessObject()
                .FindProductCategory(categoryId);
            if (searchResult is null)
            {
                return false;
            }
            else
            {
                productCategory = searchResult.ToCategory();
                return true;
            }
        }

        public bool UpdateCategories(int categoryId, ProductCategory productCategory)
        {
            var searchResult = this.factory
                .GetProductCategoryDataAccessObject()
                .FindProductCategory(categoryId);
            if (searchResult is null)
            {
                return false;
            }
            else
            {
                productCategory.Id = searchResult.Id;
                return this.factory
                .GetProductCategoryDataAccessObject()
                .UpdateProductCategory(productCategory.ToCategoryTDO());
            }
        }
    }
}
