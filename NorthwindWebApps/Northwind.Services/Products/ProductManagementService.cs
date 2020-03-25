﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Services.Products
{
    /// <summary>
    /// Represents a stub for a product management service.
    /// </summary>
    public sealed class ProductManagementService : IProductManagementService
    {
        private readonly NorthwindContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductManagementService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductManagementService(NorthwindContext context)
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
        public int CreateProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            this.context.Products.Add(product);
            this.context.SaveChanges();
            return product.Id;
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
        public bool DestroyPicture(int categoryId)
        {
            var category = this.context.Categories.Find(categoryId);

            if (category is null)
            {
                return false;
            }

            category.Picture = null;
            this.context.SaveChanges();
            return true;
        }

        /// <inheritdoc/>
        public bool DestroyProduct(int productId)
        {
            var product = this.context.Categories.Find(productId);

            if (product is null)
            {
                return false;
            }

            this.context.Remove(product);
            this.context.SaveChanges();
            return true;
        }

        /// <inheritdoc/>
        public IList<ProductCategory> LookupCategoriesByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<Product> LookupProductsByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<ProductCategory> ShowCategories(int offset, int limit)
        {
            return this.context.Categories.Skip(offset).Take(limit).ToArray();
        }

        /// <inheritdoc/>
        public IList<Product> ShowProducts(int offset, int limit)
        {
            return this.context.Products.Skip(offset).Take(limit).ToArray();
        }

        /// <inheritdoc/>
        public IList<Product> ShowProductsForCategory(int categoryId)
        {
            throw new NotImplementedException();
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
        public bool TryShowPicture(int categoryId, out byte[] bytes)
        {
            var category = this.context.Categories.Find(categoryId);

            bytes = default(byte[]);

            if (category is null)
            {
                return false;
            }

            if (category.Picture is null)
            {
                return false;
            }

            bytes = new byte[category.Picture.Length];
            Array.Copy(category.Picture, bytes, category.Picture.Length);
            return true;
        }

        /// <inheritdoc/>
        public bool TryShowProduct(int productId, out Product product)
        {
            product = default(Product);

            var result = this.context.Products.Find(productId);

            if (result is null)
            {
                return false;
            }

            product = result;
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

        /// <inheritdoc/>
        public bool UpdatePicture(int categoryId, Stream stream)
        {
            var category = this.context.Categories.Find(categoryId);

            if (category is null)
            {
                return false;
            }

            if (stream is null)
            {
                return false;
            }

            category.Picture = ((MemoryStream)stream).ToArray();
            this.context.SaveChanges();

            return true;
        }

        /// <inheritdoc/>
        public bool UpdateProduct(int productId, Product product)
        {
            var result = this.context.Products.AsNoTracking().FirstOrDefault(i => i.Id.Equals(productId));

            if (result is null)
            {
                return false;
            }

            this.context.Products.Update(product);
            this.context.SaveChanges();
            return true;
        }
    }
}
