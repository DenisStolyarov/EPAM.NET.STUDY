//using Northwind.DataAccess.Employees;
using NorthwindApiApp.Items.Products;

namespace NorthwindApiApp.Items
{
    /// <summary>
    /// Represents an abstract factory for creating Northwind DAO.
    /// </summary>
    public interface NorthwindDataAccessFactory
    {
        /// <summary>
        /// Gets a DAO for Northwind products.
        /// </summary>
        /// <returns>A <see cref="IProductDataAccessObject"/>.</returns>
        IProductDataAccessObject GetProductDataAccessObject();

        /// <summary>
        /// Gets a DAO for Northwind product categories.
        /// </summary>
        /// <returns>A <see cref="IProductCategoryDataAccessObject"/>.</returns>
        IProductCategoryDataAccessObject GetProductCategoryDataAccessObject();

        /// <summary>
        /// Gets a DAO for Northwind employees.
        /// </summary>
        /// <returns>A <see cref="IEmployeeDataAccessObject"/>.</returns>
        //IEmployeeDataAccessObject GetEmployeeDataAccessObject();
    }
}