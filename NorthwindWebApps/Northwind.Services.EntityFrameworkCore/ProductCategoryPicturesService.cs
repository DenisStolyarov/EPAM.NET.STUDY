using System;
using System.IO;
using Northwind.Services.Products;

namespace Northwind.Services.EntityFrameworkCore
{
    /// <inheritdoc/>
    public class ProductCategoryPicturesService : IProductCategoryPicturesService
    {
        private readonly NorthwindContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryPicturesService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductCategoryPicturesService(NorthwindContext context)
        {
            this.context = context;
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
    }
}
