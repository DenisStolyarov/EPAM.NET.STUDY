using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Services.Products
{
    /// <inheritdoc/>
    public class ProductCategoryManagementService : IProductCategoryManagementService
    {
        private readonly NorthwindContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryManagementService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductCategoryManagementService(NorthwindContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public int CreateCategory(ProductCategory productCategory)
        {
            if (productCategory is null)
            {
                throw new ArgumentNullException(nameof(productCategory));
            }

            this.context.Categories.Add(productCategory);
            this.context.SaveChanges();
            return productCategory.Id;
        }

        /// <inheritdoc/>
        public bool DestroyCategory(int categoryId)
        {
            var category = this.context.Categories.Find(categoryId);

            if (category is null)
            {
                return false;
            }

            this.context.Remove(category);
            this.context.SaveChanges();
            return true;
        }

        /// <inheritdoc/>
        public IList<ProductCategory> LookupCategoriesByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<ProductCategory> ShowCategories(int offset, int limit)
        {
            return this.context.Categories.Skip(offset).Take(limit).ToArray();
        }

        /// <inheritdoc/>
        public bool TryShowCategory(int categoryId, out ProductCategory productCategory)
        {
            productCategory = default(ProductCategory);

            var category = this.context.Categories.Find(categoryId);

            if (category is null)
            {
                return false;
            }

            productCategory = category;
            return true;
        }

        /// <inheritdoc/>
        public bool UpdateCategories(int categoryId, ProductCategory productCategory)
        {
            var result = this.context.Categories.AsNoTracking().FirstOrDefault(i => i.Id.Equals(categoryId));

            if (result is null)
            {
                return false;
            }

            this.context.Categories.Update(productCategory);
            this.context.SaveChanges();
            return true;
        }
    }
}
