using Northwind.DataAccess.Products;
using Northwind.Services.Products;

namespace Northwind.DataAccess.Produts
{
    public static class Mapping
    {
        public static ProductCategory ToCategory(this ProductCategoryTransferObject productCategory)
        {
            var category = new ProductCategory
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                Picture = productCategory.Picture,
            };

            return category;
        }

        public static ProductCategoryTransferObject ToCategoryTDO(this ProductCategory productCategory)
        {
            var category = new ProductCategoryTransferObject
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                Picture = productCategory.Picture,
            };

            return category;
        }
    }
}
