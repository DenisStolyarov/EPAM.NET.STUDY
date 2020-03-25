using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Products;

namespace NorthwindApiApp.Controllers
{
    /// <inheritdoc/>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private const int PaginationLimit = 15;
        private readonly IProductManagementService productManagementService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productManagementService">The product management service.</param>
        public ProductsController(IProductManagementService productManagementService)
        {
            this.productManagementService = productManagementService;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return this.productManagementService.ShowProducts(0, PaginationLimit);
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            if (!this.productManagementService.TryShowProduct(id, out Product product))
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(product);
            }
        }

        // POST: api/Products
        [HttpPost]
        public ActionResult<ProductCategory> Post(Product product)
        {
            int productId = this.productManagementService.CreateProduct(product);

            return this.CreatedAtAction(nameof(this.Get), new { id = productId }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Product product)
        {
            if (product is null)
            {
                return this.BadRequest();
            }

            if (id != product.Id)
            {
                return this.BadRequest();
            }

            if (!this.productManagementService.UpdateProduct(id, product))
            {
                return this.NotFound();
            }
            else
            {
                return this.NoContent();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!this.productManagementService.DestroyProduct(id))
            {
                return this.NotFound();
            }
            else
            {
                return this.NoContent();
            }
        }
    }
}
