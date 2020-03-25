using Microsoft.EntityFrameworkCore;

namespace Northwind.Services.Products
{
    /// <inheritdoc/>
    public class NorthwindContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NorthwindContext"/> class.
        /// </summary>
        /// <param name="options">The database option.</param>
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets get or sets product categories.
        /// </summary>
        public DbSet<ProductCategory> Categories { get; set; }

        /// <summary>
        /// Gets or sets get or sets products.
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}
