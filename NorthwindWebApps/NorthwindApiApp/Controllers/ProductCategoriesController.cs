using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Products;

namespace NorthwindApiApp.Controllers
{
    /// <inheritdoc/>
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private const int PaginationLimit = 15;
        private readonly IProductCategoryManagementService productManagementService;
        private readonly IProductCategoryPicturesService categoryPicturesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoriesController"/> class.
        /// </summary>
        /// <param name="productManagementService">The product categories management service.</param>
        public ProductCategoriesController(IProductCategoryManagementService productManagementService, IProductCategoryPicturesService categoryPicturesService)
        {
            this.productManagementService = productManagementService;
            this.categoryPicturesService = categoryPicturesService;
        }

        [ApiConventionMethod(typeof(IEnumerable<ProductCategory>), nameof(DefaultApiConventions.Get))]
        [HttpGet]
        public IEnumerable<ProductCategory> Read()
        {
            return this.productManagementService.ShowCategories(0, PaginationLimit);
        }

        [ProducesResponseType(typeof(ProductCategory), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public IActionResult Read(int id)
        {
            if (!this.productManagementService.TryShowCategory(id, out ProductCategory productCategory))
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(productCategory);
            }
        }

        [ApiConventionMethod(typeof(ProductCategory), nameof(DefaultApiConventions.Post))]
        [HttpPost]
        public ActionResult<ProductCategory> Create(ProductCategory category)
        {
            int categoryId = this.productManagementService.CreateCategory(category);

            return this.CreatedAtAction(nameof(this.Read), new { id = categoryId }, category);
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductCategory category)
        {
            if (category is null)
            {
                return this.BadRequest();
            }

            if (id != category.Id)
            {
                return this.BadRequest();
            }

            if (!this.productManagementService.UpdateCategories(id, category))
            {
                return this.NotFound();
            }
            else
            {
                return this.NoContent();
            }
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!this.productManagementService.DestroyCategory(id))
            {
                return this.NotFound();
            }
            else
            {
                return this.NoContent();
            }
        }

        // GET: api/ProductCategories/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}/picture")]
        public IActionResult ReadPicture(int id)
        {
            if (!this.categoryPicturesService.TryShowPicture(id, out byte[] bytes))
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(bytes);
            }
        }

        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}/picture")]
        public async Task<IActionResult> UpdateImage(int id)
        {
            var request = this.Request.Body;
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = await request.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                if (!this.categoryPicturesService.UpdatePicture(id, ms))
                {
                    return this.NotFound();
                }
                else
                {
                    return this.NoContent();
                }
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}/picture")]
        public IActionResult DeletePicture(int id)
        {
            if (!this.categoryPicturesService.DestroyPicture(id))
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
