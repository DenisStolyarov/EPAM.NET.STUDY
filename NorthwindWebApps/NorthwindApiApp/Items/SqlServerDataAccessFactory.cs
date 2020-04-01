// ReSharper disable CheckNamespace
using System;
using System.Data.SqlClient;
//using Northwind.DataAccess.Employees;
using NorthwindApiApp.Items.Products;

namespace NorthwindApiApp.Items
{
    /// <summary>
    /// Represents an abstract factory for creating Northwind DAO for SQL Server.
    /// </summary>
    public sealed class SqlServerDataAccessFactory : NorthwindDataAccessFactory
    {
         private SqlConnection sqlConnection = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDataAccessFactory"/> class.
        /// </summary>
        /// <param name="sqlConnection">A database connection to SQL Server.</param>
        public SqlServerDataAccessFactory(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;// ?? throw new ArgumentNullException(nameof(sqlConnection));
        }

        /// <inheritdoc/>
        public IProductCategoryDataAccessObject GetProductCategoryDataAccessObject()
        {
            return new ProductCategorySqlServerDataAccessObject(this.sqlConnection);
        }

        /// <inheritdoc/>
        public IProductDataAccessObject GetProductDataAccessObject()
        {
            return new ProductSqlServerDataAccessObject(this.sqlConnection);
        }

        /// <inheritdoc />
        //public IEmployeeDataAccessObject GetEmployeeDataAccessObject()
        //{
        //    return new EmployeeSqlServerDataAccessObject();
        //}
    }
}